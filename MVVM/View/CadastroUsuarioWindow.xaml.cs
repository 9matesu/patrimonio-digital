using System;
using System.Linq;
using System.Windows;
using patrimonio_digital.MVVM.Model;

namespace patrimonio_digital.MVVM.View
{
    public partial class CadastroUsuarioWindow : Window
    {
        public CadastroUsuarioWindow()
        {
            InitializeComponent();
            cmbTipo.ItemsSource = Enum.GetValues(typeof(TipoUsuario)).Cast<TipoUsuario>();
            cmbTipo.SelectedIndex = 0;
        }

        private void BtnCriarUsuario_Click(object sender, RoutedEventArgs e)
        {
            var nome = txtNome.Text.Trim();
            var senha = txtSenha.Password.Trim();
            var tipoSelecionado = (TipoUsuario)cmbTipo.SelectedItem;

            var auth = new AuthService();
            bool sucesso = auth.Register(nome, senha, tipoSelecionado);

            if (sucesso)
            {
                MessageBox.Show("Usuário criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNome.Text = "";
                txtSenha.Password = "";
                cmbTipo.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Erro ao criar usuário. Verifique se o nome já existe ou os campos não estão vazios.",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
