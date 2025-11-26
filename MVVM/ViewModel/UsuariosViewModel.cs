using patrimonio_digital.MVVM.Model;
using patrimonio_digital.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Usuario> usuarios;

        public ObservableCollection<Usuario> Usuarios
        {
            get => usuarios;
            set
            {
                usuarios = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Usuarios)));
            }
        }

        private readonly AuthService auth = new AuthService();

        public UsuariosViewModel()
        {
            CarregarUsuarios();
        }

        public void CarregarUsuarios()
        {
            Usuarios = new ObservableCollection<Usuario>(auth.GetAllUsers());
        }

        public void ExcluirUsuario(Usuario usuario)
        {
            if (auth.RemoveUser(usuario))
            {
                CarregarUsuarios();
            }
            else
            {
                // Pode mostrar mensagem de erro se preferir
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
