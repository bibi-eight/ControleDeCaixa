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
    private decimal saldoMinimo;

    [ObservableProperty]
    private string mensagem = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Caixa> caixas = [];

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

    [RelayCommand]
    private async Task Carregar()
    {
        await CarregarCaixas();

        Mensagem = Caixas.Count == 0
            ? "Não existem caixas cadastrados."
            : string.Empty;
    }

    private async Task CarregarCaixas()
    {
        var resultado = await _service.ObterCaixas();

        Caixas = new ObservableCollection<Caixa>(
            resultado ?? []);
    }
}