using patrimonio_digital.Core;

namespace patrimonio_digital.MVVM.Model
{
    public class Item : ObservableObject
    {
        private string _nome;
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                OnPropertyChanged();
            }
        }

        private string _autor;
        public string Autor
        {
            get => _autor;
            set
            {
                _autor = value;
                OnPropertyChanged();
            }
        }

        private string _data;
        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private string _origem;
        public string Origem
        {
            get => _origem;
            set
            {
                _origem = value;
                OnPropertyChanged();
            }
        }

        private string _tipo;
        public string Tipo
        {
            get => _tipo;
            set
            {
                _tipo = value;
                OnPropertyChanged();
            }
        }

        private string _estadoCons;
        public string EstadoCons
        {
            get => _estadoCons;
            set
            {
                _estadoCons = value;
                OnPropertyChanged();
            }
        }

        private string _setorFisico;
        public string SetorFisico
        {
            get => _setorFisico;
            set
            {
                _setorFisico = value;
                OnPropertyChanged();
            }
        }

        private string _pastaDocumentos;
        public string PastaDocumentos
        {
            get => _pastaDocumentos;
            set
            {
                _pastaDocumentos = value;
                OnPropertyChanged();
            }
        }
    }
}
