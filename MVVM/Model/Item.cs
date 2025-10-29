using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patrimonio_digital.MVVM.Model
{

    // Aqui fica a modelagem dos dados dos itens, assim como a criação da Lista de objetos

    public class Item
    {
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Data { get; set; }
        public string Origem { get; set; }
        public string Tipo { get; set; }
        public string EstadoCons { get; set; }
        public string SetorFisico { get; set; }
        public string PastaDocumentos { get; set; }


    }
}
