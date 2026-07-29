using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Enums;

namespace ControleCaixa.UI.ViewModels;

public partial class CadastroMovimentacaoViewModel : ObservableObject
{
    private readonly IMovimentacaoService _service;

    private int _caixaId;
    private TipoMovimentacao _tipo;

    public Action<bool>? FecharJanela { get; set; }

    public string Titulo =>
        _tipo == TipoMovimentacao.Entrada
            ? "Nova entrada"
            : "Nova saída";

    public string TextoBotao =>
        _tipo == TipoMovimentacao.Entrada
            ? "Registrar entrada"
            : "Registrar saída";

    [ObservableProperty]
    private string descricao = string.Empty;

    [ObservableProperty]
    private decimal valor;
    
    [ObservableProperty]
    private Categoria? categoria;
    
    private int _movimentacaoId;

    public bool Editando => _movimentacaoId > 0;

    public IEnumerable<Categoria> Categorias =>
        Enum.GetValues<Categoria>();

    [ObservableProperty]
    private string mensagem = string.Empty;

    public CadastroMovimentacaoViewModel(
        IMovimentacaoService service)
    {
        _service = service;
    }

    public void Preparar(
        int caixaId,
        TipoMovimentacao tipo,
        Categoria? categoria)
    {
        _caixaId = caixaId;
        _tipo = tipo;

        Descricao = string.Empty;
        Valor = 0;
        Categoria = null;
        Mensagem = string.Empty;

        OnPropertyChanged(nameof(Titulo));
        OnPropertyChanged(nameof(TextoBotao));
    }
    
    public void PrepararEdicao(
        int caixaId,
        MovimentacaoCompletaDTO movimentacao)
    {
        _movimentacaoId = movimentacao.Id;

        _caixaId = caixaId;
        _tipo = movimentacao.Tipo;

        Descricao = movimentacao.Descricao;
        Categoria = movimentacao.Categoria;
        Valor = movimentacao.Valor;
        Mensagem = string.Empty;

        OnPropertyChanged(nameof(Editando));
        OnPropertyChanged(nameof(Titulo));
        OnPropertyChanged(nameof(TextoBotao));
    }

    [RelayCommand]
    private async Task Salvar()
    {
        Mensagem = string.Empty;

        if (_caixaId <= 0)
        {
            Mensagem = "Caixa inválido.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Descricao))
        {
            Mensagem = "Informe a descrição.";
            return;
        }

        if (Categoria is null)
        {
            Mensagem = "Selecione uma categoria.";
            return;
        }

        if (Valor <= 0)
        {
            Mensagem = "O valor deve ser maior que zero.";
            return;
        }

        var dto = new MovimentacaoDTO()
        {
            CaixaId = _caixaId,
            Descricao = Descricao.Trim(),
            Categoria = Categoria.Value,
            Valor = Valor,
            Tipo = _tipo
        };

        var resultado = Editando
            ? await _service.Atualizar(dto, _movimentacaoId)
            : await _service.Adicionar(dto);
        
        if (!resultado.Success)
        {
            Mensagem = string.Join(
                Environment.NewLine,
                resultado.Errors);

            return;
        }

        FecharJanela?.Invoke(true);
    }

    [RelayCommand]
    private void Cancelar()
    {
        FecharJanela?.Invoke(false);
    }
}