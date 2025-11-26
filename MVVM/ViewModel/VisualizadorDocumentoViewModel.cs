using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class VisualizadorDocumentoViewModel : INotifyPropertyChanged
    {
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

        private readonly Documento documento;

        public VisualizadorDocumentoViewModel(Item item)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            string pastaBase = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
                "Patrimonio Digital",
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
            FecharCommand = new RelayCommand(_ => Fechar());
        }

        private void CarregarPaginas()
        {
            var arquivos = Directory.GetFiles(documento.Pasta, "*.txt");

            int numero = 1;

            foreach (var caminho in arquivos)
            {
                var pagina = new PaginaViewModel(new Pagina
                {
                    Numero = numero++,
                    Conteudo = File.ReadAllText(caminho)
                })
                {
                    // pode adicionar propriedade para só leitura, se quiser
                };

                Paginas.Add(pagina);
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

            if (idx < Paginas.Count - 1)
                PaginaAtual = Paginas[idx + 1];
        }

        private void Anterior()
        {
            int idx = Paginas.IndexOf(PaginaAtual);
            if (idx > 0)
                PaginaAtual = Paginas[idx - 1];
        }

        private void Fechar()
        {
            // Pode implementar lógica para fechar a janela, se necessário
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
