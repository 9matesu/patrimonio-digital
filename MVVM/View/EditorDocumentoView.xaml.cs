using patrimonio_digital.MVVM.ViewModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using patrimonio_digital.MVVM.Model;

namespace patrimonio_digital.MVVM.View
{
    public partial class EditorDocumentoView : Window
    {
        private DocumentoViewModel vm;

        public EditorDocumentoView(Item item)
        {
            InitializeComponent();
            vm = new DocumentoViewModel(item);
            DataContext = vm;

            Loaded += EditorDocumentoView_Loaded;

            SalvarButton.Click += Salvar_Click;
            ProximaButton.Click += Proxima_Click;
            AnteriorButton.Click += Anterior_Click;
        }

        private void EditorDocumentoView_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarPagina();
        }

        private void CarregarPagina()
        {
            string rtf = vm.CarregarPagina();
            TextRange range = new TextRange(EditorRTF.Document.ContentStart, EditorRTF.Document.ContentEnd);

            if (!string.IsNullOrEmpty(rtf))
            {
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(rtf));
                range.Load(stream, DataFormats.Rtf);
            }
            else
            {
                EditorRTF.Document.Blocks.Clear();
            }
        }

        private void SalvarPagina()
        {
            TextRange range = new TextRange(EditorRTF.Document.ContentStart, EditorRTF.Document.ContentEnd);
            using var stream = new MemoryStream();
            range.Save(stream, DataFormats.Rtf);
            string rtf = Encoding.UTF8.GetString(stream.ToArray());
            vm.SalvarPagina(rtf);
        }

        private void Salvar_Click(object sender, RoutedEventArgs e) => SalvarPagina();

        private void Proxima_Click(object sender, RoutedEventArgs e)
        {
            SalvarPagina();
            vm.Proxima();
            CarregarPagina();
        }

        private void Anterior_Click(object sender, RoutedEventArgs e)
        {
            SalvarPagina();
            vm.Anterior();
            CarregarPagina();
        }
    }
}
