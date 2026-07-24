using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Entities;

namespace ControleCaixa.UI.ViewModels;

public partial class CadastroCaixaViewModel : ObservableObject
{
    private readonly ICaixaService _caixaService;

    private int? _caixaId;

    public Action<bool>? FecharJanela { get; set; }
    public bool Editando => _caixaId.HasValue;

    public string Titulo => Editando
        ? "Editar caixa"
        : "Novo caixa";

    public string TextoBotao => Editando
        ? "Salvar alterações"
        : "Cadastrar";

    [ObservableProperty]
    private string nome = string.Empty;

    [ObservableProperty]
    private decimal saldoMinimo;

    [ObservableProperty]
    private string mensagem = string.Empty;

    public CadastroCaixaViewModel(ICaixaService caixaService)
    {
        _caixaService = caixaService;
    }

    public void PrepararCadastro()
    {
        _caixaId = null;

        Nome = string.Empty;
        SaldoMinimo = 0;
        Mensagem = string.Empty;

        OnPropertyChanged(nameof(Editando));
        OnPropertyChanged(nameof(Titulo));
        OnPropertyChanged(nameof(TextoBotao));
    }

    public void PrepararEdicao(CaixaDetalhesDTO caixa)
    {
        _caixaId = caixa.Id;

        Nome = caixa.Nome;
        SaldoMinimo = caixa.SaldoMinimo;
        Mensagem = string.Empty;

        OnPropertyChanged(nameof(Editando));
        OnPropertyChanged(nameof(Titulo));
        OnPropertyChanged(nameof(TextoBotao));
    }

    [RelayCommand]
    private async Task Salvar()
    {
        var dto = new CaixaDTO
        {
            Nome = Nome,
            SaldoMinimo = SaldoMinimo
        };

        var resultado = Editando
            ? await _caixaService.Atualizar(dto, _caixaId!.Value)
            : await _caixaService.Adicionar(dto);

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