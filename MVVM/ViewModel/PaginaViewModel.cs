using patrimonio_digital.MVVM.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class PaginaViewModel : INotifyPropertyChanged
    {
        public Pagina Model { get; }

        public int Numero
        {
            get => Model.Numero;
            set { Model.Numero = value; OnPropertyChanged(); }
        }

        public string Conteudo
        {
            get => Model.Conteudo;
            set { Model.Conteudo = value; OnPropertyChanged(); }
        }

        public PaginaViewModel(Pagina pagina)
        {
            Model = pagina;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string nome = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
