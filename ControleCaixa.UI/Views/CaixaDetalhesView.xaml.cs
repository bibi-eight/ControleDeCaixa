using System.Windows;
using ControleCaixa.UI.ViewModels;

namespace ControleCaixa.UI.Views;

public partial class CaixaDetalhesView : Window
{
    public CaixaDetalhesView(CaixaDetalhesViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
    
    private void Voltar_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}