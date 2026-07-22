using System.Windows;
using ControleCaixa.UI.ViewModels;

namespace ControleCaixa.UI.Views;

public partial class CaixaView : Window
{
    public CaixaView(CaixaViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}