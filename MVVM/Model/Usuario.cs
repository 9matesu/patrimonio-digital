using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace patrimonio_digital.MVVM.Model
{

    public enum TipoUsuario
    {
        Administrador,
        Funcionario,
        Visitante
    }


    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }

    }

}