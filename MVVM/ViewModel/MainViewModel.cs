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
                    ItensView.Refresh(); // atualiza a filtragem ao digitar
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
                Window janela = tipoJanela switch
                {
                    "Catalogar" => new CatalogarItemWindow { DataContext = new CatalogarItemViewModel(Itens) },
                    "Auditoria" => new Auditoria(),
                    "Usuarios" => new Usuarios(),
                    "Login" => new Login(),
                    _ => null
                };

                janela?.Show();
            }
        }

        private void FecharJanela(object parameter)
        {
            if (parameter is Window janela)
                janela.Close();
        }

        private void ExcluirItem(object parameter)
        {
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
                var editor = new EditorDocumentoView(item);
                editor.ShowDialog();
            }
        }
    }
}
