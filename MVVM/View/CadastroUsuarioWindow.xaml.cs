using patrimonio_digital.MVVM.Model;
using System;
using System.Linq;
using System.Windows;

namespace patrimonio_digital.MVVM.View
{
    public partial class CadastroUsuarioWindow : Window
    {
        private Usuario usuarioEditando;
        private readonly AuthService authService = new();

        public CadastroUsuarioWindow()
        {
            InitializeComponent();
            cmbTipo.ItemsSource = Enum.GetValues(typeof(TipoUsuario)).Cast<TipoUsuario>();
            cmbTipo.SelectedIndex = 0;
        }

        public CadastroUsuarioWindow(Usuario usuario) : this()
        {
            usuarioEditando = usuario;

            if (usuarioEditando != null)
            {
                txtNome.Text = usuarioEditando.Nome;
                cmbTipo.SelectedItem = usuarioEditando.Tipo;
                txtSenha.Password = ""; // Senha não mostrada para edição
            }
        }

        private void BtnSalvarUsuario_Click(object sender, RoutedEventArgs e)
        {
            var nome = txtNome.Text.Trim();
            var senha = txtSenha.Password.Trim();
            var tipoSelecionado = (TipoUsuario)cmbTipo.SelectedItem;

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Por favor, informe o nome do usuário.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool sucesso;

            if (usuarioEditando != null)
            {
                sucesso = authService.AtualizarUsuario(usuarioEditando, nome, senha, tipoSelecionado);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(senha))
                {
                    MessageBox.Show("Senha é obrigatória para novo usuário.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                sucesso = authService.Register(nome, senha, tipoSelecionado);
            }

            if (sucesso)
            {
                MessageBox.Show("Operação realizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Erro ao salvar usuário. Verifique se o nome já existe.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
