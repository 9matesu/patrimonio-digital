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
            Usuarios = new ObservableCollection<Usuario>();
            CarregarUsuarios();
        }

        public void CarregarUsuarios()
        {
            Usuarios.Clear();
            foreach (var u in auth.GetAllUsers())
                Usuarios.Add(u);
        }

        public void ExcluirUsuario(Usuario usuario)
        {
            if (auth.RemoveUser(usuario))
            {
                CarregarUsuarios();
            }
            else
            {
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
