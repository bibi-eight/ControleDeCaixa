using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Entities;
using ControleCaixa.UI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ControleCaixa.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICaixaService _caixaService;
    private readonly IServiceProvider _serviceProvider;

    public MainViewModel(ICaixaService caixaService, IServiceProvider serviceProvider)
    {
        _caixaService = caixaService;
        _serviceProvider = serviceProvider;
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
    private async Task EditarCaixa(Caixa? caixa)
    {
        if (caixa is null)
            return;

        var viewModel =
            _serviceProvider.GetRequiredService<CadastroCaixaViewModel>();

        viewModel.PrepararEdicao(caixa);

        var janela = new CadastroCaixaView(viewModel)
        {
            Owner = Application.Current.MainWindow
        };

        var resultado = janela.ShowDialog();

        if (resultado == true)
            await CarregarCaixas();
    }

    [RelayCommand]
    private async Task RemoverCaixa(Caixa caixa)
    {
        var confirmacao = MessageBox.Show(
            $"Essa ação irá deletar o caixa e suas movimentações, deseja deletar o \"{caixa.Nome}\"?",
            "Confirmar remoção",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (confirmacao == MessageBoxResult.No)
            return;

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