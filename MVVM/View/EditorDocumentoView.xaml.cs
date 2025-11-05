using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.ViewModel;
using System.Windows;

namespace patrimonio_digital.MVVM.View
{
    public partial class EditorDocumentoView : Window
    {
        public EditorDocumentoView(Item item)
        {
            InitializeComponent();
            DataContext = new DocumentoViewModel(item);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
