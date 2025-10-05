using patrimonio_digital.Core;
using patrimonio_digital.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace patrimonio_digital.MVVM.ViewModel
{
    public class MainViewModel
    {
        public ICommand AbrirJanelaCommand { get; }
        public ICommand FecharJanelaCommand { get; }

        public MainViewModel()
        {
            AbrirJanelaCommand = new RelayCommand(AbrirJanela);
            FecharJanelaCommand = new RelayCommand(FecharJanela);
        }

        private void AbrirJanela(object parameter)
        {
            if (parameter is string tipoJanela)
            {
                Window janela = tipoJanela switch
                {
                    "Catalogar" => new CatalogarItemWindow(),
                    "Auditoria" => new Auditoria(),
                    "Usuarios" => new Usuarios(),
                    _ => null
                };

                janela?.Show();
            }
        }

        private void FecharJanela(object parameter)
        {
            var janela = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this);

            janela?.Close();
        }
           
    }

}

