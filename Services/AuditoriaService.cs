using patrimonio_digital.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace patrimonio_digital.Services
{

    public static class AuditoriaService
    {
        public static ObservableCollection<AuditoriaModel> Registros { get; } = new();

        private static readonly string pastaDesktop = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "digiPat");

        private static readonly string caminho = Path.Combine(pastaDesktop, "auditoria.json");


        public static void RegistrarAuditoria(AuditoriaModel registro)
        {
            Registros.Add(registro);
        }

        public static void SalvarAuditoria()
        {
            var lista = Registros.ToList(); 
            var json = JsonSerializer.Serialize(lista);
            File.WriteAllText(caminho, json);
        }

        public static void CarregarAuditoria()
        {
            if (!File.Exists(caminho))
                return;

            var json = File.ReadAllText(caminho);
            var lista = JsonSerializer.Deserialize<List<AuditoriaModel>>(json);

            Registros.Clear();
            foreach (var item in lista)
                Registros.Add(item);
        }
    }

}
