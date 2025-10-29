using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View;
using patrimonio_digital.Utils;
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
        public ICommand ExcluirItemCommand { get; } // interface para apagar item da lista (provisorio?)

        //criação da lista primitiva p exibição de teste
        public ObservableCollection<Item> Itens { get; } = new ObservableCollection<Item>();


        public MainViewModel()
        {
            AbrirJanelaCommand = new RelayCommand(AbrirJanela);
            FecharJanelaCommand = new RelayCommand(FecharJanela);
            Itens = ItemStorage.Carregar(); // carrega do armazenamento permanente
            ExcluirItemCommand = new RelayCommand(ExcluirItem); // apagar item
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
                    "Login" => new Login(),
                    _ => null
                };

                janela?.Show();
            }
        }

        //função fechar janela -- sem uso
        private void FecharJanela(object parameter)
        {
            if (parameter is Window janela)
            {
                janela.Close();
            }
        }

        private void ExcluirItem(object parametro)
        {
            if (parametro is Item item && Itens.Contains(item))
            {
                var resultado = MessageBox.Show(
                    $"Tem certeza que deseja excluir o item\"{item.Nome}\"?",
                    "Confirmar exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    Itens.Remove(item);
                    ItemStorage.Salvar(Itens);
                }
            }
        }

    }

}

