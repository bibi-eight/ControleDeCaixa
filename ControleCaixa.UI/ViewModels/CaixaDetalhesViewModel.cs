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
    private readonly IServiceProvider _serviceProvider;

    private int _caixaId;

    public CaixaDetalhesViewModel(
        ICaixaService service,
        IServiceProvider serviceProvider)
    {
        _service = service;
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private string nome = string.Empty;

    [ObservableProperty]
    private decimal saldo;

    [ObservableProperty]
    private decimal saldoMinimo;

    [ObservableProperty]
    private ObservableCollection<MovimentacaoCompletaDTO> movimentacoes = [];

    public async Task Carregar(int caixaId)
    {
        var caixa = await _service.ObterCaixaPorId(caixaId);

        if (caixa is null)
            return;

        _caixaId = caixa.Id;

        Nome = caixa.Nome;
        Saldo = caixa.Saldo;
        SaldoMinimo = caixa.SaldoMinimo;

        Movimentacoes = new ObservableCollection<MovimentacaoCompletaDTO>(
            caixa.Movimentacoes.Select(m => new MovimentacaoCompletaDTO
            {
                Id = m.Id,
                Descricao = m.Descricao,
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

        vm.Preparar(_caixaId, tipo);

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
}