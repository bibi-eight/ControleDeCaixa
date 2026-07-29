using System.Windows;
using ControleCaixa.UI.ViewModels;

namespace ControleCaixa.UI.Views;

public partial class MainView : Window
{
    private readonly MainViewModel _viewModel;

    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = viewModel;

        Loaded += MainView_Loaded;
        Closed += MainView_Closed;
    }

    private async void MainView_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        await _viewModel.IniciarAtualizacao();
    }

    private void MainView_Closed(
        object? sender,
        EventArgs e)
    {
        _viewModel.PararAtualizacao();
    }
}