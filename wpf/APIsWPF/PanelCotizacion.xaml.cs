using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using APIsWPF.Configuracion;
using APIsWPF.Models;

namespace APIsWPF
{
    /// <summary>
    /// Interaction logic for PanelCotizacion.xaml
    /// </summary>
    public partial class PanelCotizacion : IOLWindow
    {
        IOLWindow a = new IOLWindow();
        private string _json;

        public PanelCotizacion()
        {
            InitializeComponent();
            cmbInstrumento.ItemsSource = new InstrumentoDTO().Iniciar();
            cmbInstrumento.SelectedIndex = 0;
        }

        public PanelCotizacion(string json)
        {
            InitializeComponent();
            _json = json;
            Inicializar(_json);
        }

        private void Inicializar(string json)
        {
            var panel = ObtenerJson(json);
            dataGrid.ItemsSource = panel.Rows;
            dataGrid.Columns[5].Visibility = Visibility.Collapsed;
            dataGrid.Columns[6].Visibility = Visibility.Collapsed;


        }

        private PanelModel ObtenerJson(string jsonStr)
        {
            var panel = JsonConvert.DeserializeObject<PanelModel>(jsonStr);
            string json = JsonConvert.SerializeObject(panel, Formatting.Indented);
            txtJson.Document.Blocks.Clear();
            txtJson.Document.Blocks.Add(new Paragraph(new Run(json)));
            return panel;
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            var instrumento = (InstrumentoDTO) cmbInstrumento.SelectedValue;
            var panel = (PanelDTO) cmbPanel.SelectedValue;
            btnBuscar.IsEnabled = false;
            var uri = _apiUrl + $"/api/Titulos/PanelCotizaciones?InstrumentosCotizacion={instrumento.Nombre}&Panel={panel.Nombre}&Pais=Argentina";
            lblUrl.Content = uri;
            var datos = await EnviarPeticionGet(uri, null, _bearerToken);
            Inicializar(datos.Cuerpo);
            btnBuscar.IsEnabled = true;


        }

        public List<string> Instrumentos()
        {
            var list = new List<string>
            {
                "Acciones",
                "Bonos",
                "Opciones",
                "Monedas",
                "Cauciones",
                "CHPD",
                "Futuros",
            };


            return list;
        }

        private void cmbInstrumento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = (InstrumentoDTO)cmbInstrumento.SelectedValue;
            cmbPanel.ItemsSource = new PanelDTO().Iniciar().Where(p => p.IdInstrumento == value.IdInstrumento);
            cmbPanel.SelectedIndex = 0;
        }
    }

    public class InstrumentoDTO
    {
        public int IdInstrumento { get; set; }
        public string Nombre { get; set; }

        public List<InstrumentoDTO> Iniciar()
        {
            var list = new List<InstrumentoDTO>
            {
                //Accion
                new InstrumentoDTO {IdInstrumento = 1, Nombre = "Acciones"},
                new InstrumentoDTO {IdInstrumento = 2, Nombre = "Bonos"},
                new InstrumentoDTO {IdInstrumento = 3, Nombre = "Opciones"},
                new InstrumentoDTO {IdInstrumento = 4, Nombre = "Monedas"},
                new InstrumentoDTO {IdInstrumento = 5, Nombre = "Cauciones"},
                new InstrumentoDTO {IdInstrumento = 6, Nombre = "CHPD"},
                new InstrumentoDTO {IdInstrumento = 7, Nombre = "Futuros"},
            };
            return list;
        }
    }

    public class PanelDTO
    {
        public int IdPanel { get; set; }
        public int IdInstrumento { get; set; }
        public string Nombre { get; set; }

        public List<PanelDTO> Iniciar()
        {
            var list = new List<PanelDTO>
                {
                    //Accion
                    new PanelDTO {IdPanel = 1, IdInstrumento = 1, Nombre = "Merval"},
                    new PanelDTO {IdPanel = 2, IdInstrumento = 1, Nombre = "Panel General"},
                    new PanelDTO {IdPanel = 3, IdInstrumento = 1, Nombre = "Merval 25"},
                    new PanelDTO {IdPanel = 4, IdInstrumento = 1, Nombre = "Merval Argentina"},
                    new PanelDTO {IdPanel = 5, IdInstrumento = 1, Nombre = "Burcap"},
                    new PanelDTO {IdPanel = 6, IdInstrumento = 1, Nombre = "CEDEARs"},
                    //Bonos
                    new PanelDTO {IdPanel = 7, IdInstrumento = 2, Nombre = "Cortos En Pesos"},
                    new PanelDTO {IdPanel = 8, IdInstrumento = 2, Nombre = "Largos En Pesos"},
                    new PanelDTO {IdPanel = 9, IdInstrumento = 2, Nombre = "Cortos En Dólares"},
                    new PanelDTO {IdPanel = 10, IdInstrumento = 2, Nombre = "Largos En Dólares"},
                    new PanelDTO {IdPanel = 11, IdInstrumento = 2, Nombre = "Cortos En Otras Monedas"},
                    new PanelDTO {IdPanel = 12, IdInstrumento = 2, Nombre = "Largos En Otras Monedas"},
                    new PanelDTO {IdPanel = 13, IdInstrumento = 2, Nombre = "Cupones PBI"},
                    new PanelDTO {IdPanel = 14, IdInstrumento = 2, Nombre = "Provinciales"},
                    new PanelDTO {IdPanel = 15, IdInstrumento = 2, Nombre = "Fideicomisos"},
                    //Opciones
                    new PanelDTO {IdPanel = 16, IdInstrumento = 3, Nombre = "De Acciones"},
                    new PanelDTO {IdPanel = 17, IdInstrumento = 3, Nombre = "De Bonos"},
                    new PanelDTO {IdPanel = 18, IdInstrumento = 3, Nombre = "De Cedears"},
                    new PanelDTO {IdPanel = 19, IdInstrumento = 3, Nombre = "In-the-Money"},
                    new PanelDTO {IdPanel = 20, IdInstrumento = 3, Nombre = "Out-the-Money"},
                    new PanelDTO {IdPanel = 21, IdInstrumento = 3, Nombre = "Calls"},
                    new PanelDTO {IdPanel = 22, IdInstrumento = 3, Nombre = "Puts"},
                    //Moneda
                    new PanelDTO {IdPanel = 23, IdInstrumento = 4, Nombre = "Principales divisas"},
                    new PanelDTO {IdPanel = 24, IdInstrumento = 4, Nombre = "Otras divisas"},
                    //Cauciones
                    new PanelDTO {IdPanel = 25, IdInstrumento = 5, Nombre = "Todas"},
                    //Futuros
                    new PanelDTO {IdPanel = 26, IdInstrumento = 7, Nombre = "Dólar USA"},
                    new PanelDTO {IdPanel = 27, IdInstrumento = 7, Nombre = "MERVAL"},
                    new PanelDTO {IdPanel = 28, IdInstrumento = 7, Nombre = "BONAR X"},
                    new PanelDTO {IdPanel = 29, IdInstrumento = 7, Nombre = "BONAR 2024"},
                    new PanelDTO {IdPanel = 30, IdInstrumento = 7, Nombre = "DICA"},
                    new PanelDTO {IdPanel = 31, IdInstrumento = 7, Nombre = "Cupón PBI"},
                    new PanelDTO {IdPanel = 32, IdInstrumento = 7, Nombre = "Oro"},
                    new PanelDTO {IdPanel = 33, IdInstrumento = 7, Nombre = "Petróleo WTI"},
                    new PanelDTO {IdPanel = 34, IdInstrumento = 7, Nombre = "Soja Chicago"},
                    new PanelDTO {IdPanel = 35, IdInstrumento = 7, Nombre = "Indice Soja Rosafé"},
                    new PanelDTO {IdPanel = 36, IdInstrumento = 7, Nombre = "Soja"},
                    new PanelDTO {IdPanel = 37, IdInstrumento = 7, Nombre = "Maíz Chicago"},
                    new PanelDTO {IdPanel = 38, IdInstrumento = 7, Nombre = "Maíz"},
                    new PanelDTO {IdPanel = 39, IdInstrumento = 7, Nombre = "Trigo"},

                };
            return list;
        }
    }

}
