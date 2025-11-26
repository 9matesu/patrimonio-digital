using patrimonio_digital.Core;
using patrimonio_digital.MVVM.Model;
using patrimonio_digital.MVVM.View;
using patrimonio_digital.Services;
using patrimonio_digital.Utils;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace patrimonio_digital.MVVM.ViewModel
{
    public class CatalogarItemViewModel : ObservableObject
    {

        // interfaces para fechar janela atual (catalogaritemwindow)
        public Window JanelaAtual { get; set; }
        public ICommand FecharCommand { get; }

        // 
        public string UsuarioLogado { get; set; }

        // campos de binding
        private string _nomeNovoItem;
        public string NomeNovoItem
        {
            get => _nomeNovoItem;
            set { if (_nomeNovoItem == value) return; _nomeNovoItem = value; OnPropertyChanged(); }
        }
        private string _autorNovoItem;
        public string AutorNovoItem
        {
            get => _autorNovoItem;
            set { if (_autorNovoItem == value) return; _autorNovoItem = value; OnPropertyChanged(); }
        }
        private string _dataNovoItem;
        public string DataNovoItem
        {
            get => _dataNovoItem;
            set { if (_dataNovoItem == value) return; _dataNovoItem = value; OnPropertyChanged(); }
        }
        private string _origemNovoItem;
        public string OrigemNovoItem
        {
            get => _origemNovoItem;
            set { if (_origemNovoItem == value) return; _origemNovoItem = value; OnPropertyChanged(); }
        }
        private string _tipoNovoItem;
        public string TipoNovoItem
        {
            get => _tipoNovoItem;
            set { if (_tipoNovoItem == value) return; _tipoNovoItem = value; OnPropertyChanged(); }
        }
        private string _estadoConsNovoItem;
        public string EstadoConsNovoItem
        {
            get => _estadoConsNovoItem;
            set { if (_estadoConsNovoItem == value) return; _estadoConsNovoItem = value; OnPropertyChanged(); }
        }
        private string _setorFisicoNovoItem;
        public string SetorFisicoNovoItem
        {
            get => _setorFisicoNovoItem;
            set { if (_setorFisicoNovoItem == value) return; _setorFisicoNovoItem = value; OnPropertyChanged(); }
        }

        // coleção compartilhada e comando
        public ObservableCollection<Item> Itens { get; }
        public ICommand RegistrarCommand { get; }

        // item em edição (null => novo)
        private Item ItemEditando { get; }

        // lista: estado de conservação
        public ObservableCollection<string> EstadoCons { get; set; } = new ObservableCollection<string>
        {
            "Novo", "Bom", "Regular", "Ruim"
        };

        // lista: tipo de documento
        public ObservableCollection<string> Tipo { get; set; } = new ObservableCollection<string>
        {
            "Fotografia", "Declaração", "Boletim de Ocorrência", "Escritura"
        };

        // construtor antigo (compatibilidade)
        public CatalogarItemViewModel(ObservableCollection<Item> itensCompartilhados, string usuarioLogado)
            : this(itensCompartilhados, usuarioLogado, null)
        {
        }

        // construtor principal
        public CatalogarItemViewModel(ObservableCollection<Item> itensCompartilhados, string usuarioLogado, Item itemParaEditar)
        {
            Itens = itensCompartilhados ?? throw new ArgumentNullException(nameof(itensCompartilhados));
            UsuarioLogado = usuarioLogado;
            ItemEditando = itemParaEditar;

            // construtor para fechar janela
            FecharCommand = new RelayCommand(janela =>
            {
                if (janela is Window w)
                    w.Close();
            });

            RegistrarCommand = new RelayCommand(_ => RegistrarItem());

            if (ItemEditando != null)
            {
                NomeNovoItem = ItemEditando.Nome;
                AutorNovoItem = ItemEditando.Autor;
                DataNovoItem = ItemEditando.Data;
                OrigemNovoItem = ItemEditando.Origem;
                TipoNovoItem = ItemEditando.Tipo;
                EstadoConsNovoItem = ItemEditando.EstadoCons;
                SetorFisicoNovoItem = ItemEditando.SetorFisico;
            }
        }

        // registra novo item ou atualiza o existente
        private void RegistrarItem()
        {
            if (string.IsNullOrWhiteSpace(NomeNovoItem)) return;

            if (ItemEditando == null)
            {
                // cadastro
                var novo = new Item
                {
                    Nome = NomeNovoItem,
                    Autor = AutorNovoItem,
                    Data = DataNovoItem,
                    Origem = OrigemNovoItem,
                    Tipo = TipoNovoItem,
                    EstadoCons = EstadoConsNovoItem,
                    SetorFisico = SetorFisicoNovoItem
                };

                Itens.Add(novo);

                AuditoriaService.RegistrarAuditoria(new AuditoriaModel
                {
                    DataHora = DateTime.Now,
                    Usuario = UsuarioLogado,
                    Acao = "Cadastro de Item",
                    Item = NomeNovoItem
                });
            }
            else
            {
                // edição
                ItemEditando.Nome = NomeNovoItem;
                ItemEditando.Autor = AutorNovoItem;
                ItemEditando.Data = DataNovoItem;
                ItemEditando.Origem = OrigemNovoItem;
                ItemEditando.Tipo = TipoNovoItem;
                ItemEditando.EstadoCons = EstadoConsNovoItem;
                ItemEditando.SetorFisico = SetorFisicoNovoItem;


                AuditoriaService.RegistrarAuditoria(new AuditoriaModel
                {
                    DataHora = DateTime.Now,
                    Usuario = UsuarioLogado,
                    Acao = "Edição de Item",
                    Item = NomeNovoItem
                });
                
            }

            // limpa campos
            NomeNovoItem = string.Empty;
            AutorNovoItem = string.Empty;
            DataNovoItem = string.Empty;
            OrigemNovoItem = string.Empty;
            TipoNovoItem = string.Empty;
            EstadoConsNovoItem = string.Empty;
            SetorFisicoNovoItem = string.Empty;

            FecharCommand.Execute(JanelaAtual);

            // ItemStorage.Salvar(Itens); - salva no armazenamento permanente após registrar, desabilitado
        }
    }
}
