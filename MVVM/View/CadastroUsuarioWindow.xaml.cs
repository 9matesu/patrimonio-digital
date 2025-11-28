using System.Windows;
using System.Windows.Controls;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.ViewModel;

namespace patrimonio_digital.MVVM.View
{
    public partial class CadastroUsuarioWindow : Window
    {
        public CadastroUsuarioWindow()
        {
            InitializeComponent();
            DataContext = new CadastroUsuarioViewModel(this);
        }

        public CadastroUsuarioWindow(Usuario usuario)
        {
            InitializeComponent();
            DataContext = new CadastroUsuarioViewModel(this, usuario);
        }

        private void TxtSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CadastroUsuarioViewModel vm && sender is PasswordBox pb)
            {
                vm.Senha = pb.Password;
            }
        }
    }
}
