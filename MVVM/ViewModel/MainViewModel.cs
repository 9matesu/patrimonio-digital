using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View;
using patrimonio_digital.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private Usuario _usuarioLogado;
        public Usuario UsuarioLogado
        {
            get => _usuarioLogado;
            set
            {
                _usuarioLogado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PodeCatalogar));
                OnPropertyChanged(nameof(PodeExcluir));
                OnPropertyChanged(nameof(PodeGerenciarUsuarios));
                OnPropertyChanged(nameof(PodeAuditoria));
            }
        }

        public bool PodeCatalogar => UsuarioLogado != null &&
            (UsuarioLogado.Tipo == TipoUsuario.Administrador || UsuarioLogado.Tipo == TipoUsuario.Funcionario);

        public bool PodeExcluir => UsuarioLogado != null &&
            UsuarioLogado.Tipo == TipoUsuario.Administrador;

        public bool PodeGerenciarUsuarios => UsuarioLogado != null &&
            UsuarioLogado.Tipo == TipoUsuario.Administrador;

        public bool PodeAuditoria => UsuarioLogado != null &&
            UsuarioLogado.Tipo == TipoUsuario.Administrador;

        public bool PodeEditar => UsuarioLogado != null &&
            UsuarioLogado.Tipo != TipoUsuario.Visitante;

        public ICommand AbrirJanelaCommand { get; }
        public ICommand FecharJanelaCommand { get; }
        public ICommand ExcluirItemCommand { get; }
        public ICommand AbrirEditorCommand { get; }

        private string textoPesquisa;
        public string TextoPesquisa
        {
            get => textoPesquisa;
            set
            {
                if (textoPesquisa != value)
                {
                    textoPesquisa = value;
                    OnPropertyChanged();
                    ItensView.Refresh();
                }
            }
        }

        public ObservableCollection<Item> Itens { get; }
        public ICollectionView ItensView { get; }

        public MainViewModel()
        {
            AbrirJanelaCommand = new RelayCommand(AbrirJanela);
            FecharJanelaCommand = new RelayCommand(FecharJanela);
            ExcluirItemCommand = new RelayCommand(ExcluirItem);
            AbrirEditorCommand = new RelayCommand(AbrirEditor);

            Itens = ItemStorage.Carregar();
            ItensView = CollectionViewSource.GetDefaultView(Itens);
            ItensView.Filter = FiltrarItens;
        }

        private bool FiltrarItens(object obj)
        {
            if (obj is not Item item) return false;

            if (string.IsNullOrWhiteSpace(TextoPesquisa))
                return true;

            string termo = TextoPesquisa.Trim().ToLower();

            return (item.Nome?.ToLower().Contains(termo) == true) ||
                   (item.Autor?.ToLower().Contains(termo) == true) ||
                   (item.Origem?.ToLower().Contains(termo) == true) ||
                   (item.Tipo?.ToLower().Contains(termo) == true) ||
                   (item.Data?.ToLower().Contains(termo) == true) ||
                   (item.EstadoCons?.ToLower().Contains(termo) == true) ||
                   (item.SetorFisico?.ToLower().Contains(termo) == true);
        }

        private void AbrirJanela(object parameter)
        {
            if (parameter is string tipoJanela)
            {
                if (tipoJanela == "Usuarios" && !PodeGerenciarUsuarios)
                {
                    MessageBox.Show("Acesso negado: você não tem permissão para gerenciar usuários.");
                    return;
                }

                if (tipoJanela == "Auditoria" && !PodeAuditoria)
                {
                    MessageBox.Show("Acesso negado: você não tem permissão para acessar Auditoria.");
                    return;
                }

                Window janela = tipoJanela switch
                {
                    "Catalogar" when PodeCatalogar => new CatalogarItemWindow { DataContext = new CatalogarItemViewModel(Itens) },
                    "Auditoria" when PodeAuditoria => new Auditoria(),
                    "Usuarios" when PodeGerenciarUsuarios => new CadastroUsuarioWindow(),
                    "Login" => new Login(),
                    _ => null
                };

                if (janela == null)
                {
                    MessageBox.Show("Acesso negado para essa funcionalidade.");
                    return;
                }

                janela.Show();
            }
        }

        private void FecharJanela(object parameter)
        {
            if (parameter is Window janela)
                janela.Close();
        }

        private void ExcluirItem(object parameter)
        {
            if (!PodeExcluir)
            {
                MessageBox.Show("Acesso negado: você não tem permissão para excluir itens.");
                return;
            }

            if (parameter is Item item && Itens.Contains(item))
            {
                var resultado = MessageBox.Show(
                    $"Tem certeza que deseja excluir o item \"{item.Nome}\"?",
                    "Confirmar exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    Itens.Remove(item);
                    ItemStorage.Salvar(Itens);
                }
            }
        }

        private void AbrirEditor(object parameter)
        {
            if (parameter is Item item)
            {
                if (PodeEditar)
                {
                    // Abre o editor para usuarios com permissão
                    var editor = new EditorDocumentoView(item);
                    editor.ShowDialog();
                }
                else
                {
                    // Abre o visualizador somente leitura para visitantes
                    var visualizador = new VisualizadorDocumentoView(item);
                    visualizador.DataContext = new VisualizadorDocumentoViewModel(item);
                    visualizador.ShowDialog();
                }
            }
        }
    }
}
