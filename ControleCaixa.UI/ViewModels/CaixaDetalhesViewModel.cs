using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Enums;
using ControleCaixa.UI.ViewModels;
using ControleCaixa.UI.Views;
using Microsoft.Extensions.DependencyInjection;

public partial class CaixaDetalhesViewModel : ObservableObject
{
    private readonly ICaixaService _service;
    private readonly IMovimentacaoService _movimentacaoService;
    private readonly IServiceProvider _serviceProvider;

    private int _caixaId;

    public CaixaDetalhesViewModel(
        ICaixaService service,
        IServiceProvider serviceProvider, IMovimentacaoService movimentacaoService)
    {
        _service = service;
        _serviceProvider = serviceProvider;
        _movimentacaoService = movimentacaoService;
    }

    [ObservableProperty]
    private string nome = string.Empty;

    [ObservableProperty]
    private decimal saldo;

    [ObservableProperty]
    private decimal saldoMinimo;

    [ObservableProperty]
    private ObservableCollection<MovimentacaoCompletaDTO> movimentacoes = [];
    
    [ObservableProperty]
    private string mensagem = string.Empty;
    
    [ObservableProperty]
    private int quantidadeMovimentacoes;

    public async Task Carregar(int caixaId)
    {
        var caixa = await _service.ObterCaixaPorId(caixaId);

        if (caixa is null)
            return;
        
        QuantidadeMovimentacoes =
            await _movimentacaoService.ObterQuantidadeMovimentacoes(_caixaId);

        _caixaId = caixa.Id;

        Nome = caixa.Nome;
        Saldo = caixa.Saldo;
        SaldoMinimo = caixa.SaldoMinimo;

        Movimentacoes = new ObservableCollection<MovimentacaoCompletaDTO>(
            caixa.Movimentacoes.Select(m => new MovimentacaoCompletaDTO
            {
                Id = m.Id,
                CaixaId = caixa.Id,
                Descricao = m.Descricao,
                Categoria = m.Categoria,
                Valor = m.Valor,
                Tipo = m.Tipo,
                Data = m.Data
            }));
    }
    
    [RelayCommand]
    private async Task NovaEntrada()
    {
        await AbrirMovimentacao(TipoMovimentacao.Entrada);
    }

    [RelayCommand]
    private async Task NovaSaida()
    {
        await AbrirMovimentacao(TipoMovimentacao.Saida);
    }

    private async Task AbrirMovimentacao(TipoMovimentacao tipo)
    {
        var vm = _serviceProvider
            .GetRequiredService<CadastroMovimentacaoViewModel>();

        vm.Preparar(_caixaId, tipo, null);

        var janela = new CadastroMovimentacaoView(vm)
        {
            Owner = Application.Current.Windows
                .OfType<CaixaDetalhesView>()
                .FirstOrDefault()
        };
        var resultado = janela.ShowDialog();

        if (resultado == true)
            await Carregar(_caixaId);
    }
    
    [RelayCommand]
    private async Task EditarMovimentacao(MovimentacaoCompletaDTO? movimentacao)
    {
        if (movimentacao is null)
            return;

        var vm = _serviceProvider
            .GetRequiredService<CadastroMovimentacaoViewModel>();

        vm.PrepararEdicao(movimentacao.CaixaId, movimentacao);

        var janela = new CadastroMovimentacaoView(vm)
        {
            Owner = Application.Current.MainWindow
        };

        var resultado = janela.ShowDialog();

        if (resultado == true)
            await Carregar(_caixaId);
    }
    
    [RelayCommand]
    private async Task RemoverMovimentacao(
        MovimentacaoCompletaDTO? movimentacao)
    {
        if (movimentacao is null)
            return;

        var resultado =
            await _movimentacaoService.Apagar(movimentacao.Id);

        if (!resultado.Success)
        {
            MessageBox.Show(
                string.Join(Environment.NewLine, resultado.Errors));

            return;
        }

        var item = Movimentacoes
            .FirstOrDefault(x => x.Id == movimentacao.Id);

        if (item is not null)
            Movimentacoes.Remove(item);

        MessageBox.Show(
            $"Item encontrado: {item is not null}\n" +
            $"Itens restantes: {Movimentacoes.Count}");
        
        QuantidadeMovimentacoes--;
            await Carregar(_caixaId);
        
    }
}