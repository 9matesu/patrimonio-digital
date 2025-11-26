using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patrimonio_digital.MVVM.Model
{
    public class AuditoriaModel
    {
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }
        public string Acao { get; set; }      
        public string Item { get; set; }  
    }
}
