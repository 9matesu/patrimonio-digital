using patrimonio_digital.MVVM.Model;
using patrimonio_digital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class AuditoriaViewModel
    {
        public ObservableCollection<AuditoriaModel> Registros => AuditoriaService.Registros;
    }
}