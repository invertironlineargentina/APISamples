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
    public partial class Vender : IOLWindow
    {
        private string _json;
        private string simbolo;
        private string mercado;

        public Vender()
        {
            InitializeComponent();
        }
        public Vender(string json)
        {
            InitializeComponent();
            _json = json;
            Inicializar(_json);
        }

        public Vender(string json, string simbolo, string mercado) : this(json)
        {
            this.simbolo = simbolo;
            this.mercado = mercado;
            txtSimbolo.Text = simbolo;
            txtMercado.Text = mercado;
        }

        private void Inicializar(string json)
        {
            var cotizacion = ObtenerJson(json);
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

        private CotizacionModel ObtenerJson(string jsonStr)
        {
            var cotizacion = JsonConvert.DeserializeObject<CotizacionModel>(jsonStr);
            return cotizacion;
        }

        private async void EnviarURL_Click(object sender, RoutedEventArgs e)
        {
            var uri = IOLWindow._apiUrl + "/api/Titulos/PanelCotizaciones?InstrumentosCotizacion=Accion&Panel=Merval&Pais=Argentina";
            var datos = await EnviarPeticionGet(uri, null, _bearerToken);
            var pcv = new PanelCotizacion(datos.Cuerpo);
            pcv.Show();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVender_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
