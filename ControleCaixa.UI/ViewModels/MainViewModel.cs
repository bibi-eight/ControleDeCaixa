using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Model.Entities;
using ControleCaixa.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Threading;

namespace ControleCaixa.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ICaixaService _caixaService;
    private readonly IServiceProvider _serviceProvider;

    private readonly DispatcherTimer _timer;
    private bool _carregando;

    public MainViewModel(
        ICaixaService caixaService,
        IServiceProvider serviceProvider)
    {
        _caixaService = caixaService;
        _serviceProvider = serviceProvider;

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(5)
        };

        _timer.Tick += Timer_Tick;
    }

    [ObservableProperty]
    private ObservableCollection<CaixaDetalhesDTO> caixas = [];

    [ObservableProperty]
    private Caixa? caixa;

    [ObservableProperty]
    private string mensagem = string.Empty;

    private async void Timer_Tick(object? sender, EventArgs e)
    {
        Mensagem = $"Atualizado em {DateTime.Now:HH:mm:ss}";

        await CarregarCaixas();
    }

    public async Task IniciarAtualizacao()
    {
        await CarregarCaixas();

        if (!_timer.IsEnabled)
            _timer.Start();
    }

    public void PararAtualizacao()
    {
        _timer.Stop();
    }

    [RelayCommand]
    private async Task CarregarCaixas()
    {
        if (_carregando)
            return;

        try
        {
            _carregando = true;

            var resultado = await _caixaService.ObterCaixas();

            Caixas = new ObservableCollection<CaixaDetalhesDTO>(
                resultado.Select(caixa => new CaixaDetalhesDTO
                {
                    Id = caixa.Id,
                    Nome = caixa.Nome,
                    SaldoMinimo = caixa.SaldoMinimo,
                    Saldo = caixa.Saldo
                }));
        }
        catch (Exception ex)
        {
            Mensagem = $"Erro ao atualizar caixas: {ex.Message}";
        }
        finally
        {
            _carregando = false;
        }
    }
    
    [RelayCommand]
    private async Task AbrirCaixa(CaixaDetalhesDTO? caixa)
    {
        if (caixa is null)
            return;

        var vm = _serviceProvider
            .GetRequiredService<CaixaDetalhesViewModel>();

        await vm.Carregar(caixa.Id);

        var janela = new CaixaDetalhesView(vm)
        {
            Owner = Application.Current.MainWindow
        };

        janela.ShowDialog();

        await CarregarCaixas();
    }
    
    [RelayCommand]
    private async Task AbrirCadastro()
    {
        var viewModel =
            _serviceProvider.GetRequiredService<CadastroCaixaViewModel>();

        viewModel.PrepararCadastro();

        var janela = new CadastroCaixaView(viewModel)
        {
            Owner = Application.Current.MainWindow
        };

        var resultado = janela.ShowDialog();

        if (resultado == true)
            await CarregarCaixas();    
    }
    
    [RelayCommand]
    private async Task EditarCaixa(CaixaDetalhesDTO? caixa)
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
    private async Task RemoverCaixa(CaixaDetalhesDTO caixa)
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