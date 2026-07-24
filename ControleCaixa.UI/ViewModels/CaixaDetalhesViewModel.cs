using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;

public partial class CaixaDetalhesViewModel : ObservableObject
{
    private readonly ICaixaService _service;

    [ObservableProperty]
    private string nome = string.Empty;

    [ObservableProperty]
    private decimal saldo;

    [ObservableProperty]
    private decimal saldoMinimo;

    [ObservableProperty]
    private ObservableCollection<MovimentacaoDTO> movimentacoes = [];

    public CaixaDetalhesViewModel(ICaixaService service)
    {
        _service = service;
    }

    public async Task Carregar(int caixaId)
    {
        var caixa = await _service.ObterCaixaPorId(caixaId);

        if (caixa is null)
            return;

        Nome = caixa.Nome;
        Saldo = caixa.Saldo;
        SaldoMinimo = caixa.SaldoMinimo;
    }
}