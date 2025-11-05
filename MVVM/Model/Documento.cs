using System.IO;

namespace patrimonio_digital.MVVM.Model
{
    public class Documento
    {
        public Item Item { get; set; }
        public string Pasta { get; set; }
        public int PaginaAtual { get; set; } = 1;

        private string NomeArquivo(int pagina)
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
}
