using System.Windows;
using System.Windows.Controls;
using patrimonio_digital.MVVM.ViewModel;

namespace patrimonio_digital.MVVM.View
{
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox pb)
            {
                vm.Senha = pb.Password;
            }
        }
    }
}
