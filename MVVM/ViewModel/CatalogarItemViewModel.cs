using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    internal class CatalogarItemViewModel : ObservableObject
    {

        // variável intermediária que recebe o valor da textbox
        private string _nomeNovoItem;
        public string NomeNovoItem
        {
            get => _nomeNovoItem;
            set
            {
                if (_nomeNovoItem == value) return;
                _nomeNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }

        public ObservableCollection<Item> Itens { get; } // lista de itens (por enquanto só tem o campo nome)

        public ICommand RegistrarCommand { get; }

        public CatalogarItemViewModel(ObservableCollection<Item> itensCompartilhados) // firula pra mandar essa lista pra main window
        {
            Itens = itensCompartilhados;
            RegistrarCommand = new RelayCommand(_ => RegistrarItem());
        }


        //função de registrar item na lista
        private void RegistrarItem()
        {
            if (string.IsNullOrWhiteSpace(NomeNovoItem)) return;
            Itens.Add(new Item { Nome = NomeNovoItem });
            NomeNovoItem = string.Empty;
        }
    }
}
