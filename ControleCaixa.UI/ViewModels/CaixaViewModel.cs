using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Entities;

namespace ControleCaixa.UI.ViewModels;

public partial class CaixaViewModel : ObservableObject
{
    private readonly ICaixaService _service;

    public CaixaViewModel(ICaixaService service)
    {
        _service = service;
    }

    [ObservableProperty]
    private string nome = string.Empty;
    
    [ObservableProperty]
    private decimal saldo;

    [ObservableProperty]
    private decimal saldoMinimo;

    [ObservableProperty]
    private string mensagem = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Caixa> caixas = [];
    
    [ObservableProperty]
    private ObservableCollection<MovimentacaoCompletaDTO> movimentacoes = [];
    
    

    [RelayCommand]
    private async Task Cadastrar()
    {
        var dto = new CaixaDTO
        {
            Nome = Nome,
            SaldoMinimo = SaldoMinimo
        };

        var resultado = await _service.Adicionar(dto);

        if (!resultado.Success)
        {
            Mensagem = string.Join(
                Environment.NewLine,
                resultado.Errors);

            return;
        }

        Nome = string.Empty;
        SaldoMinimo = 0;

        await CarregarCaixas();

        Mensagem = "Caixa cadastrado com sucesso!";
    }

    public async Task Carregar(int caixaId)
    {
        var caixa = await _service.ObterCaixaPorId(caixaId);

        if (caixa is null)
            return;

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

    private async Task CarregarCaixas()
    {
        var resultado = await _service.ObterCaixas();

        Caixas = new ObservableCollection<Caixa>(
            resultado ?? []);
    }
}