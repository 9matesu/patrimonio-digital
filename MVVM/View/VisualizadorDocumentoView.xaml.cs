using patrimonio_digital.MVVM.Model;
using System.Windows;

namespace patrimonio_digital.MVVM.View
{
    public partial class VisualizadorDocumentoView : Window
    {
        public Item ItemDetalhes { get; set; }

        public VisualizadorDocumentoView(Item item)
        {
            InitializeComponent();
            ItemDetalhes = item;
            DataContext = this;
        }
    }
}