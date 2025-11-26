using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using patrimonio_digital.MVVM.Model;
using System.IO;

namespace patrimonio_digital.Services
{

    public static class AuditoriaService
    {

        // todas as entradas na auditoria serão armazenadas numa lista _registros, que carrega objetos da classe AuditoriaModel
        private static readonly List<AuditoriaModel> _registros = new(); 
        private static readonly string caminho = "auditoria.json";

        public static void RegistrarAuditoria(AuditoriaModel registro)
        {
            // adiciona um objeto registro à lista _registros. esse objeto "registro" é da classe AuditoriaModel, que contém os campos necessários
            _registros.Add(registro);
        }

        // método que permite retornar a lista _registros de forma read-only
        public static IReadOnlyList<AuditoriaModel> ObterRegistros()
        {
            return _registros.AsReadOnly();
        }

        // método que salvará a lista em formato json ao fechar
        public static void SalvarAuditoria()
        {
            var json = JsonSerializer.Serialize(_registros);
            File.WriteAllText(caminho, json);
        }

        // método que carregará a lista ao iniciar
        public static void CarregarAuditoria()
        {
            if (!File.Exists(caminho))
                return;

            var json = File.ReadAllText(caminho);
            var lista = JsonSerializer.Deserialize<List<AuditoriaModel>>(json);

            if (lista != null)
            {
                _registros.Clear();
                _registros.AddRange(lista);
            }
        }
    }
}
