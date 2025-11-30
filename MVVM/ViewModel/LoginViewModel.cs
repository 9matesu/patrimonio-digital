using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View;
using patrimonio_digital.Services;
using System.Windows;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class LoginViewModel : ObservableObject
    {

        private readonly Window window;
        private readonly AuthService authService = new();

        private string nome = string.Empty;
        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        public ICommand EntrarCommand { get; }

        public LoginViewModel(Window window)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));
            EntrarCommand = new RelayCommand(_ => Entrar(), _ => !string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(Senha));
        }

        public void Entrar()
        {
            var usuario = authService.Login(Nome, Senha);

            if (usuario != null)
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = new MainViewModel(usuario, usuario.Nome);
                mainWindow.Show();
                window.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
