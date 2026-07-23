using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Entities;

namespace ControleCaixa.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICaixaService _caixaService;

    public MainViewModel(ICaixaService caixaService)
    {
        _caixaService = caixaService;
    }

    [ObservableProperty]
    private ObservableCollection<Caixa> caixas = [];

    [ObservableProperty]
    private string mensagem = string.Empty;

    [RelayCommand]
    private async Task CarregarCaixas()
    {
        var resultado = await _caixaService.ObterCaixas();

        Caixas = new ObservableCollection<Caixa>(resultado);

        Mensagem = Caixas.Count == 0
            ? "Nenhum caixa cadastrado."
            : string.Empty;
    }

    [RelayCommand]
    private void AbrirCadastro()
    {
        Mensagem = "Cadastro ainda não implementado.";
    }

    [RelayCommand]
    private void AbrirCaixa(Caixa caixa)
    {
        Mensagem = $"Caixa selecionado: {caixa.Nome}";
    }
    
    [RelayCommand]
    private void EditarCaixa(Caixa caixa)
    {
        Mensagem = $"Editar caixa: {caixa.Nome}";
    }

    [RelayCommand]
    private async Task RemoverCaixa(Caixa caixa)
    {
        var resultado = await _caixaService.Apagar(caixa.Id);

        if (!resultado.Success)
        {
            Mensagem = string.Join(
                Environment.NewLine,
                resultado.Errors);

            return;
        }

        Caixas.Remove(caixa);

        Mensagem = "Caixa removido com sucesso.";
    }
}