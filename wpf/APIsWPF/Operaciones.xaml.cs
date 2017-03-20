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
using APIsWPF.Models.DTO;

namespace APIsWPF
{
    /// <summary>
    /// Interaction logic for PanelCotizacion.xaml
    /// </summary>
    public partial class Operaciones : IOLWindow
    {
        IOLWindow a = new IOLWindow();
        private string _json;

        public Operaciones()
        {
            InitializeComponent();
            dpFechaDesde.SelectedDate = DateTime.Now.AddMonths(-4);
            dpFechaHasta.SelectedDate = DateTime.Now;
            cmbEstadoTransaccion.SelectedIndex = 0;
        }

        public Operaciones(string json)
        {
            InitializeComponent();
            _json = json;
            Inicializar(_json);
        }

        private void Inicializar(string json)
        {
            var panel = ObtenerJson(json);
            dataGrid.ItemsSource = panel.Operaciones;
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var id = btn.Content.ToString();
            var uri = IOLWindow._apiUrl + "/api/Titulos/CotizacionPorId?id=" + id;
            var datos = await EnviarPeticionGet(uri, null, _bearerToken);
            var ct = new CotizacionTitulo(datos.Cuerpo);
            ct.ShowDialog();
           
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            btnBuscar.IsEnabled = false;
            string estado = cmbEstadoTransaccion.Text;
            if (dpFechaDesde.SelectedDate != null)
            {
                if (dpFechaHasta.SelectedDate != null)
                {
                    var uri = _apiUrl + $"/api/micuenta/misoperaciones?EstadoTransaccion={estado}&FechaDesde={dpFechaDesde.SelectedDate.Value.ToString("yyyy-MM-dd")}&FechaHasta={dpFechaHasta.SelectedDate.Value.ToString("yyyy-MM-dd")} 23:59:59";
                    lblUrl.Content = uri;
                    var datos = await EnviarPeticionGet(uri, null, _bearerToken);
                    Inicializar(datos.Cuerpo);
                }
            }
            btnBuscar.IsEnabled = true;
        }

        private OperacionDTO ObtenerJson(string jsonStr)
        {
            var dto = JsonConvert.DeserializeObject<OperacionDTO>(jsonStr);
            string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            txtJson.Document.Blocks.Clear();
            txtJson.Document.Blocks.Add(new Paragraph(new Run(json)));
            return dto;
        }
    }
}
