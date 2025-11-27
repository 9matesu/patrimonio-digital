using patrimonio_digital.MVVM.ViewModel;
using patrimonio_digital.MVVM.Model;
using System.Windows;

namespace patrimonio_digital.MVVM.View
{
    public partial class Usuarios : Window
    {

        private readonly UsuariosViewModel ViewModel;

        public Usuarios()
        {
            InitializeComponent();
            ViewModel = new UsuariosViewModel();
            DataContext = ViewModel;
        }

        private void BtnAbrirCadastro_Click(object sender, RoutedEventArgs e)
        {
            var cadastro = new CadastroUsuarioWindow();
            cadastro.ShowDialog();

            ViewModel.CarregarUsuarios();
        }

        private void BtnExcluirUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuario)
            {
                var resultado = MessageBox.Show($"Deseja realmente excluir o usuário \"{usuario.Nome}\"?",
                    "Confirmar exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    ViewModel.ExcluirUsuario(usuario);
                }
            }
        }
        private void BtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem is Usuario usuario)
            {
                var cadastro = new CadastroUsuarioWindow(usuario);
                cadastro.ShowDialog();

                ViewModel.CarregarUsuarios();
            }
        }

    }
}
