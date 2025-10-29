using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class CatalogarItemViewModel : ObservableObject
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

        private string _autorNovoItem;
        public string AutorNovoItem
        {
            get => _autorNovoItem;
            set
            {
                if (_autorNovoItem == value) return;
                _autorNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }

        private string _dataNovoItem;
        public string DataNovoItem
        {
            get => _dataNovoItem;
            set
            {
                if (_dataNovoItem == value) return;
                _dataNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }

        private string _origemNovoItem;
        public string OrigemNovoItem
        {
            get => _origemNovoItem;
            set
            {
                if (_origemNovoItem == value) return;
                _origemNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }

        private string _tipoNovoItem;
        public string TipoNovoItem
        {
            get => _tipoNovoItem;
            set
            {
                if (_tipoNovoItem == value) return;
                _tipoNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }

        private string _estadoConsNovoItem;
        public string EstadoConsNovoItem
        {
            get => _estadoConsNovoItem;
            set
            {
                if (_estadoConsNovoItem == value) return;
                _estadoConsNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }

      

        private string _setorFisicoNovoItem;
        public string SetorFisicoNovoItem
        {
            get => _setorFisicoNovoItem;
            set
            {
                if (_setorFisicoNovoItem == value) return;
                _setorFisicoNovoItem = value;
                OnPropertyChanged(); // atualiza o evento
            }
        }



        public ObservableCollection<Item> Itens { get; }

        public ICommand RegistrarCommand { get; }

        public CatalogarItemViewModel(ObservableCollection<Item> itensCompartilhados) // firula pra mandar essa lista pra main window
        {
            Itens = itensCompartilhados;
            RegistrarCommand = new RelayCommand(_ => RegistrarItem());
        }

        // lista de estados de conservação
        public ObservableCollection<string> EstadoCons { get; set; } = new ObservableCollection<string>
        {
            "Novo",
            "Bom",
            "Regular",
            "Ruim"
        };

        public ObservableCollection<string> Tipo { get; set; } = new ObservableCollection<string>
        {
            "Fotografia",
            "Declaração",
            "Boletim de Ocorrência",
            "Escritura"
        };


        //função de registrar item na lista
        private void RegistrarItem()
        {
            if (string.IsNullOrWhiteSpace(NomeNovoItem)) return;
            Itens.Add(new Item { Nome = NomeNovoItem, Autor = AutorNovoItem, Data = DataNovoItem, Origem = OrigemNovoItem, Tipo = TipoNovoItem, EstadoCons = EstadoConsNovoItem, SetorFisico = SetorFisicoNovoItem });
            NomeNovoItem = string.Empty;
            AutorNovoItem = string.Empty;
            DataNovoItem = string.Empty;
            OrigemNovoItem = string.Empty;
            TipoNovoItem = string.Empty;
            EstadoConsNovoItem = string.Empty;
            SetorFisicoNovoItem = string.Empty;

            ItemStorage.Salvar(Itens);
        }
    }
}
