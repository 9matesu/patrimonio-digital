using patrimonio_digital.MVVM.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
    {
        private Documento documento;

        public string Pagina => $"Página {documento.PaginaAtual}";

        public ICommand SalvarCommand { get; }
        public ICommand ProximaCommand { get; }
        public ICommand AnteriorCommand { get; }

        public DocumentoViewModel(Item item)
        {
            documento = new Documento
            {
                Item = item,
                Pasta = item.PastaDocumentos,
                PaginaAtual = 1
            };

            if (!Directory.Exists(documento.Pasta))
                Directory.CreateDirectory(documento.Pasta);

            SalvarCommand = new RelayCommand(_ => Salvar());
            ProximaCommand = new RelayCommand(_ => Proxima());
            AnteriorCommand = new RelayCommand(_ => Anterior());
        }

        public string CarregarPagina() => documento.CarregarPagina(documento.PaginaAtual);
        public void SalvarPagina(string rtf) => documento.SalvarPagina(rtf);

        private void Salvar() { /* Será chamado pelo código-behind */ }

        public void Proxima()
        {
            documento.ProximaPagina();
            OnPropertyChanged(nameof(Pagina));
        }

        public void Anterior()
        {
            documento.PaginaAnterior();
            OnPropertyChanged(nameof(Pagina));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }

    public class RelayCommand : ICommand
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

    public class Documento
    {
        public Item Item { get; set; }
        public string Pasta { get; set; }
        public int PaginaAtual { get; set; } = 1;

        private string NomeArquivo(int pagina)
        {
            string nomeBase = Item.Nome.Replace(" ", "_");
            return Path.Combine(Pasta, $"{nomeBase}_Pagina{pagina}.rtf");
        }

        public void SalvarPagina(string rtf) => File.WriteAllText(NomeArquivo(PaginaAtual), rtf);

        public string CarregarPagina(int pagina) =>
            File.Exists(NomeArquivo(pagina)) ? File.ReadAllText(NomeArquivo(pagina)) : string.Empty;

        public void ProximaPagina() => PaginaAtual++;
        public void PaginaAnterior() { if (PaginaAtual > 1) PaginaAtual--; }
    }
}
