using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.ViewModel;
using System.Windows;

namespace patrimonio_digital.MVVM.View
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            var auth = new AuthService();
            var usuario = auth.Login(txtNome.Text, txtSenha.Password);

            if (usuario != null)
            {

                // após autorizado, inicia MainWindow passando usuario.Nome como parâmetro do MainViewModel

                var mainWindow = new MainWindow();
                mainWindow.DataContext = new MainViewModel(usuario, usuario.Nome);
                mainWindow.Show();

                this.Close(); 
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.");
            }
        }
    }
}