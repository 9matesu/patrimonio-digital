using patrimonio_digital.MVVM.Model;
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
                var mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close(); // fecha a janela de login
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.");
            }
        }
    }
}