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

        public Usuario UsuarioLogado { get; private set; }

        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            var auth = new AuthService();
            var usuario = auth.Login(txtNome.Text, txtSenha.Password);

            if (usuario != null)
            {
                UsuarioLogado = usuario;

                var mainVM = new MainViewModel();
                mainVM.UsuarioLogado = UsuarioLogado;

                var mainWindow = new MainWindow
                {
                    DataContext = mainVM
                };
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