using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
    {
   
        private readonly Documento documento;

        public ObservableCollection<PaginaViewModel> Paginas { get; } = new();

        private PaginaViewModel paginaAtual;
        public PaginaViewModel PaginaAtual
        {
            get => paginaAtual;
            set
            {
                paginaAtual = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ProximaCommand { get; }
        public RelayCommand AnteriorCommand { get; }
        public RelayCommand FecharCommand { get; }

        public DocumentoViewModel(Item item)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));


            string pastaBase = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                "digiPat",
                "Documentos",
                item.Nome ?? "ItemSemNome"
            );

            documento = new Documento
            {
                Item = item,
                Pasta = pastaBase
            };

            if (!Directory.Exists(documento.Pasta))
                Directory.CreateDirectory(documento.Pasta);

            CarregarPaginas();

            ProximaCommand = new RelayCommand(_ => Proxima());
            AnteriorCommand = new RelayCommand(_ => Anterior());
            FecharCommand = new RelayCommand(_ => SalvarTodas());
        }

        private void CarregarPaginas()
        {
            var arquivos = Directory.GetFiles(documento.Pasta, "*.txt");

            int numero = 1;

            foreach (var caminho in arquivos)
            {
                Paginas.Add(new PaginaViewModel(new Pagina
                {
                    Numero = numero++,
                    Conteudo = File.ReadAllText(caminho)
                }));
            }

            if (Paginas.Count == 0)
            {
                Paginas.Add(new PaginaViewModel(new Pagina
                {
                    Numero = 1,
                    Conteudo = ""
                }));
            }

            PaginaAtual = Paginas[0];
        }

        private void Proxima()
        {
            int idx = Paginas.IndexOf(PaginaAtual);

            if (idx == Paginas.Count - 1)
            {
                var nova = new PaginaViewModel(new Pagina
                {
                    Numero = Paginas.Count + 1
                });

                Paginas.Add(nova);
                PaginaAtual = nova;
            }
            else
            {
                PaginaAtual = Paginas[idx + 1];
            }
        }

        private void Anterior()
        {
            int idx = Paginas.IndexOf(PaginaAtual);
            if (idx > 0)
                PaginaAtual = Paginas[idx - 1];
        }

        public void SalvarTodas()
        {
            foreach (var pagina in Paginas)
            {
                string caminho = documento.ObterCaminhoPagina(pagina.Numero);
                File.WriteAllText(caminho, pagina.Conteudo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
