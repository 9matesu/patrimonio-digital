using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class CadastroUsuarioViewModel : ObservableObject
    {
        private readonly AuthService authService = new();
        private readonly Window window;

        private Usuario? usuarioEditando;

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

        private TipoUsuario tipoSelecionado;
        public TipoUsuario TipoSelecionado
        {
            get => tipoSelecionado;
            set
            {
                tipoSelecionado = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TipoUsuario> TiposUsuario { get; }

        public ICommand SalvarCommand { get; }

        public CadastroUsuarioViewModel(Window window)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));

            TiposUsuario = new ObservableCollection<TipoUsuario>(
                Enum.GetValues(typeof(TipoUsuario)).Cast<TipoUsuario>());

            TipoSelecionado = TiposUsuario.FirstOrDefault();
            SalvarCommand = new RelayCommand(_ => Salvar());
        }

        public CadastroUsuarioViewModel(Window window, Usuario usuario) : this(window)
        {
            usuarioEditando = usuario;

            if (usuarioEditando != null)
            {
                Nome = usuarioEditando.Nome;
                TipoSelecionado = usuarioEditando.Tipo;
                Senha = string.Empty;
            }
        }

        private void Salvar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                MessageBox.Show("Por favor, informe o nome do usuário.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool sucesso;

            if (usuarioEditando != null)
            {
                sucesso = authService.AtualizarUsuario(usuarioEditando, Nome, Senha, TipoSelecionado);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Senha))
                {
                    MessageBox.Show("Senha é obrigatória para novo usuário.", "Erro",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                sucesso = authService.Register(Nome, Senha, TipoSelecionado);
            }

            if (sucesso)
            {
                MessageBox.Show("Operação realizada com sucesso!", "Sucesso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                window.Close();
            }
            else
            {
                MessageBox.Show("Erro ao salvar usuário. Verifique se o nome já existe.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
