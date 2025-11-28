using System.Windows;
using patrimonio_digital.MVVM.ViewModel;

namespace patrimonio_digital.MVVM.View
{
    public partial class Usuarios : Window
    {
        public Usuarios()
        {
            InitializeComponent();
            DataContext = new ListaUsuariosViewModel();
        }
    }
}
