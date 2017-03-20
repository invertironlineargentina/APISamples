using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for CotizacionTitulo.xaml
    /// </summary>
    public partial class CotizacionTitulo : IOLWindow
    {
        private string _json;
        public CotizacionTitulo()
        {
            InitializeComponent();
        }
        public CotizacionTitulo(string json)
        {
            InitializeComponent();
            _json = json;
            Inicializar(_json);
        }

        private void Inicializar(string json)
        {
            var cotizacion = ObtenerJson(json);
            if (cotizacion != null)
            {

                lblApertura.Content = cotizacion.Apertura;
                lblFecha.Content = cotizacion.FechaHora;
                lblMaximo.Content = cotizacion.Maximo;
                lblMinimo.Content = cotizacion.Minimo;
                lblUltimo.Content = cotizacion.UltimoPrecio;
                IList<PuntasModel> puntas = new List<PuntasModel>();
                if (cotizacion.Puntas == null)
                    puntas.Add(cotizacion.PuntasUltimas);
                else
                    puntas = cotizacion.Puntas;

                dataGrid.ItemsSource = puntas;

            }
            else
            {
                MessageBox.Show("Titulo no encontrado", "Información", MessageBoxButton.OK, MessageBoxImage.Warning);
                lblApertura.Content = string.Empty;
                lblFecha.Content = string.Empty;
                lblMaximo.Content = string.Empty;
                lblMinimo.Content = string.Empty;
                lblUltimo.Content = string.Empty;
                dataGrid.ItemsSource = null;
            }

        }

        private CotizacionModel ObtenerJson(string jsonStr)
        {
            var cotizacion = JsonConvert.DeserializeObject<CotizacionModel>(jsonStr);
            string json = JsonConvert.SerializeObject(cotizacion, Formatting.Indented);
            txtJson.Document.Blocks.Clear();
            txtJson.Document.Blocks.Add(new Paragraph(new Run(json)));
            return cotizacion;
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            btnBuscar.IsEnabled = false;
            var uri = _apiUrl + $"/api/Titulos/Cotizacion?simbolo={txtSimbolo.Text}&Mercado={cmbMercado.Text}";
            lblUrl.Content = uri;
            var datos = await EnviarPeticionGet(uri, null, _bearerToken);
            Inicializar(datos.Cuerpo);
            btnBuscar.IsEnabled = true;
           
        }
    }
}
