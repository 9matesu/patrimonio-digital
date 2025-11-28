using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View;
using patrimonio_digital.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class ListaUsuariosViewModel : ObservableObject
    {
        private readonly AuthService authService = new();
        private ObservableCollection<Usuario> usuarios = new();
        private Usuario? usuarioSelecionado;

        public ObservableCollection<Usuario> Usuarios
        {
            get => usuarios;
            set
            {
                usuarios = value;
                OnPropertyChanged();
            }
        }

        public Usuario? UsuarioSelecionado
        {
            get => usuarioSelecionado;
            set
            {
                usuarioSelecionado = value;
                OnPropertyChanged();
            }
        }

        public ICommand AbrirCadastroCommand { get; }
        public ICommand EditarUsuarioCommand { get; }
        public ICommand ExcluirUsuarioCommand { get; }

        public ListaUsuariosViewModel()
        {
            CarregarUsuarios();

            AbrirCadastroCommand = new RelayCommand(_ => AbrirCadastro());
            EditarUsuarioCommand = new RelayCommand(_ => EditarUsuario(), _ => UsuarioSelecionado != null);
            ExcluirUsuarioCommand = new RelayCommand(_ => ExcluirUsuario(), _ => UsuarioSelecionado != null);
        }

        private void CarregarUsuarios()
        {
            Usuarios = new ObservableCollection<Usuario>(authService.GetAllUsers());
        }

        private void AbrirCadastro()
        {
            var cadastro = new CadastroUsuarioWindow();
            cadastro.ShowDialog();
            CarregarUsuarios();
        }

        private void EditarUsuario()
        {
            if (UsuarioSelecionado != null)
            {
                var cadastro = new CadastroUsuarioWindow(UsuarioSelecionado);
                cadastro.ShowDialog();
                CarregarUsuarios();
            }
        }

        private void ExcluirUsuario()
        {
            if (UsuarioSelecionado != null)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir o usuário \"{UsuarioSelecionado.Nome}\"?",
                    "Confirmar exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    if (authService.RemoveUser(UsuarioSelecionado))
                    {
                        CarregarUsuarios();
                    }
                }
            }
        }
    }
}
