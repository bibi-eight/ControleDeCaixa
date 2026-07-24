using System.Windows;
using ControleCaixa.UI.ViewModels;

namespace ControleCaixa.UI.Views;

public partial class CadastroCaixaView : Window
{
    public CadastroCaixaView(
        CadastroCaixaViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

        viewModel.FecharJanela = resultado =>
        {
            DialogResult = resultado;
            Close();
        };  
    }
}