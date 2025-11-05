using patrimonio_digital.MVVM.Model;
using patrimonio_digital.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
    {
        private Documento documento;
        private string conteudo;

        public string Conteudo
        {
            get => conteudo;
            set { conteudo = value; OnPropertyChanged(); }
        }

        public string Pagina => $"Página {documento.PaginaAtual}";

        public RelayCommand SalvarCommand { get; }
        public RelayCommand ProximaCommand { get; }
        public RelayCommand AnteriorCommand { get; }

        public DocumentoViewModel(Item item)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item), "O item não pode ser nulo.");

            string pastaBase = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                "Patrimonio Digital",
                "Documentos",
                item.Nome ?? "ItemSemNome"
            );

            documento = new Documento
            {
                Item = item,
                Pasta = pastaBase,
                PaginaAtual = 1
            };

            if (!Directory.Exists(documento.Pasta))
                Directory.CreateDirectory(documento.Pasta);

            Conteudo = documento.CarregarPagina(documento.PaginaAtual);

            SalvarCommand = new RelayCommand(_ => Salvar());
            ProximaCommand = new RelayCommand(_ => Proxima());
            AnteriorCommand = new RelayCommand(_ => Anterior());
        }

        private void Salvar() => documento.SalvarPagina(Conteudo);

        private void Proxima()
        {
            documento.SalvarPagina(Conteudo);
            documento.ProximaPagina();
            Conteudo = documento.CarregarPagina(documento.PaginaAtual);
            OnPropertyChanged(nameof(Pagina));

        }

        private void Anterior()
        {
            documento.SalvarPagina(Conteudo);
            documento.PaginaAnterior();
            Conteudo = documento.CarregarPagina(documento.PaginaAtual);
            OnPropertyChanged(nameof(Pagina));

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
