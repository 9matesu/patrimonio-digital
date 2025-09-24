using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace patrimonio_digital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        // GotFocus e LostFocus: funções para tirar o texto da Box quando o usuário clica
        private void txtBusca_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusca.Text == "Procure por itens, palavras-chave, data...")
            {
                txtBusca.Text = "";
                txtBusca.Foreground = Brushes.Black;
            }
        }

        private void txtBusca_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusca.Text))
            {
                txtBusca.Text = "Procure por itens, palavras-chave, data...";
                txtBusca.Foreground = Brushes.Gray;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}