using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace patrimonio_digital.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ICommand AbrirJanelaCommand { get; }
        public ICommand FecharJanelaCommand { get; }

        //criação da lista primitiva p exibição de teste
        public ObservableCollection<Item> Itens { get; } = new ObservableCollection<Item>();


        public MainViewModel()
        {
            AbrirJanelaCommand = new RelayCommand(AbrirJanela);
            FecharJanelaCommand = new RelayCommand(FecharJanela);

        }

        // funções para abrir janelas baseado nas tags de cada botão
        // usa-se: "tag" = new ArquivoJanela
        private void AbrirJanela(object parameter)
        {
            if (parameter is string tipoJanela)
            {
                Window janela = tipoJanela switch
                {
                    "Catalogar" => new CatalogarItemWindow() { DataContext = new CatalogarItemViewModel(Itens) },
                    "Auditoria" => new Auditoria(),
                    "Usuarios" => new Usuarios(),
                    _ => null
                };

                janela?.Show();
            }
        }

        //função fechar janela
        private void FecharJanela(object parameter)
        {
            if (parameter is Window janela)
            {
                janela.Close();
            }
        }

    }

}

