using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;

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
            Mensagem = string.Join(Environment.NewLine, resultado.Errors);
            return;
        }

        Mensagem = "Caixa cadastrado com sucesso!";

        Nome = string.Empty;
        SaldoMinimo = 0;
    }
}