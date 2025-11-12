using System.Windows;

namespace patrimonio_digital.MVVM.View
{
    public partial class LoginView : Window
    {
        private readonly AuthService _authService;

        public LoginView()
        {
            InitializeComponent();

            _authService = new AuthService();

            // Opcional: criar um admin inicial se não existir
            var adminExistente = _authService.Login("admin", "admin123");
            if (adminExistente == null)
            {
                _authService.Register("admin", "admin123", UserRole.Administrador);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string senha = txtSenha.Password.Trim();

            var usuario = _authService.Login(nome, senha);

            if (usuario != null)
            {
                // Abre a MainWindow passando o usuário logado
                var mainWindow = new MainWindow(_authService, usuario);
                mainWindow.Show();

                // Fecha o login
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos!", 
                                "Erro", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Warning);
            }
        }
    }
}
