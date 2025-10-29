using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace patrimonio_digital.Utils
{
    public class EditorDoc
    {
        private Documento documento;

        public EditorDoc(string nomeDocumento, string pasta)
        {
            documento = new Documento
            {
                Item = new Item { Nome = nomeDocumento },
                Pasta = pasta,
                PaginaAtual = 1
            };

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);
        }

        private void AbrirEditor()
        {
            // Criando a janela
            Window janela = new Window
            {
                Title = $"Editor - {documento.Item.Nome}",
                Width = 800,
                Height = 500
            };

            // ViewModel
            var vm = new DocumentoViewModel(documento);

            // Layout
            DockPanel dock = new DockPanel();

            // Barra de botões
            StackPanel botoes = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
            Button btnAnterior = new Button { Content = "Anterior", Margin = new Thickness(2), Command = vm.AnteriorCommand };
            Button btnSalvar = new Button { Content = "Salvar", Margin = new Thickness(2), Command = vm.SalvarCommand };
            Button btnProxima = new Button { Content = "Próxima", Margin = new Thickness(2), Command = vm.ProximaCommand };
            TextBlock lblPagina = new TextBlock { Margin = new Thickness(10, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            lblPagina.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Pagina"));

            botoes.Children.Add(btnAnterior);
            botoes.Children.Add(btnSalvar);
            botoes.Children.Add(btnProxima);
            botoes.Children.Add(lblPagina);
            DockPanel.SetDock(botoes, Dock.Top);

            // RichTextBox para conteúdo
            TextBox txtEditor = new TextBox
            {
                AcceptsReturn = true,
                AcceptsTab = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                FontSize = 14
            };
            txtEditor.SetBinding(TextBox.TextProperty, new System.Windows.Data.Binding("Conteudo") { UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged });

            dock.Children.Add(botoes);
            dock.Children.Add(txtEditor);

            janela.Content = dock;
            janela.DataContext = vm;

            janela.ShowDialog();
        }

        // Classes internas para MVVM
        private class DocumentoViewModel : INotifyPropertyChanged
        {
            private Documento doc;
            private string conteudo;

            public string Conteudo
            {
                get => conteudo;
                set { conteudo = value; OnPropertyChanged(); }
            }

            public string Pagina => $"Página {doc.PaginaAtual}";

            public ICommand SalvarCommand { get; }
            public ICommand ProximaCommand { get; }
            public ICommand AnteriorCommand { get; }

            public DocumentoViewModel(Documento documento)
            {
                doc = documento;
                Conteudo = doc.CarregarPagina(doc.PaginaAtual);

                SalvarCommand = new RelayCommand(_ => Salvar());
                ProximaCommand = new RelayCommand(_ => Proxima());
                AnteriorCommand = new RelayCommand(_ => Anterior());
            }

            private void Salvar() => doc.SalvarPagina(Conteudo);

            private void Proxima()
            {
                doc.ProximaPagina();
                Conteudo = doc.CarregarPagina(doc.PaginaAtual);
                OnPropertyChanged(nameof(Pagina));
            }

            private void Anterior()
            {
                doc.PaginaAnterior();
                Conteudo = doc.CarregarPagina(doc.PaginaAtual);
                OnPropertyChanged(nameof(Pagina));
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged([CallerMemberName] string nome = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
            }
        }

        private class RelayCommand : ICommand
        {
            private readonly Action<object> execute;
            private readonly Func<object, bool> canExecute;

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);
            public void Execute(object parameter) => execute(parameter);
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }

        // Classes modelo
        private class Documento
        {
            public Item Item { get; set; }
            public string Pasta { get; set; }
            public int PaginaAtual { get; set; } = 1;

            public string NomeArquivo(int pagina)
            {
                string nomeBase = Item.Nome.Replace(" ", "_");
                return Path.Combine(Pasta, $"{nomeBase}_Pagina{pagina}.txt");
            }

            public void SalvarPagina(string conteudo)
            {
                File.WriteAllText(NomeArquivo(PaginaAtual), conteudo);
            }

            public string CarregarPagina(int pagina)
            {
                string caminho = NomeArquivo(pagina);
                return File.Exists(caminho) ? File.ReadAllText(caminho) : "";
            }

            public void ProximaPagina() => PaginaAtual++;
            public void PaginaAnterior() { if (PaginaAtual > 1) PaginaAtual--; }
        }

        private class Item
        {
            public string Nome { get; set; }
        }
    }
}
