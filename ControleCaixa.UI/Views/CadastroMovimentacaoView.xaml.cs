using System.Windows;
using ControleCaixa.UI.ViewModels;

namespace ControleCaixa.UI.Views;

public partial class CadastroMovimentacaoView : Window
{
    public CadastroMovimentacaoView(
        CadastroMovimentacaoViewModel viewModel)
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