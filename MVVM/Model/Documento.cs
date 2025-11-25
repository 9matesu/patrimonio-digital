// MVVM/Model/Documento.cs
using System.IO;

namespace patrimonio_digital.MVVM.Model
{
    public class Documento
    {
        public Item Item { get; set; }
        public string Pasta { get; set; }

        public string ObterCaminhoPagina(int pagina)
        {
            string nomeBase = Item.Nome.Replace(" ", "_");
            return Path.Combine(Pasta, $"{nomeBase}_Pagina{pagina}.txt");
        }
    }
}
