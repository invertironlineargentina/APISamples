using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Web.Script.Serialization;
using System.Web;
using System.Configuration;
using Newtonsoft.Json;
using APIsWPF.Models;
using APIsWPF.Configuracion;

namespace APIsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IOLWindow
    {
        int vieneDe = 0;

        public MainWindow()
        {
            InitializeComponent();
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            txtTokenEndpoint.Text = _apiUrl + "/token";
            btnOperaciones.IsEnabled = false;
            btnMiCuenta.IsEnabled = false;
            btnPanelCotizacion.IsEnabled = false;
            btnPortafolio.IsEnabled = false;
            Cotizacion.IsEnabled = false;
        }

        private async void btnToken_Click(object sender, RoutedEventArgs e)
        {
            btnToken.IsEnabled = false;
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Debe completar los campos de usuario y contraseña");
                btnToken.IsEnabled = true;
                return;
            }

            var parametros = new Dictionary<string, string>()
                {
                    {"username", txtUserName.Text},
                    {"password", txtPassword.Password},
                    {"grant_type", "password"}
                };

            var respuesta = await EnviarPeticionPost(txtTokenEndpoint.Text, parametros);
            if (respuesta.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show(respuesta.Cuerpo);
                btnToken.IsEnabled = true;
                return;
            }

            var serializador = new JavaScriptSerializer();
            var respuestaSerializada = serializador.Deserialize<Dictionary<string, string>>(respuesta.Cuerpo);
            IOLWindow._bearerToken = respuestaSerializada.Where(x => x.Key == "access_token").FirstOrDefault().Value;
            txtBearerToken.Text = IOLWindow._bearerToken;
            IOLWindow._refreshToken = respuestaSerializada.Where(x => x.Key == "refresh_token").FirstOrDefault().Value;
            txtRefreshToken.Text = IOLWindow._refreshToken;
            labelExitoso.Visibility = Visibility.Visible;
            btnToken.IsEnabled = true;
            btnOperaciones.IsEnabled = true;
            btnMiCuenta.IsEnabled = true;
            btnPanelCotizacion.IsEnabled = true;
            btnPortafolio.IsEnabled = true;
            Cotizacion.IsEnabled = true;
        }

        private async void Cotizacion_Click(object sender, RoutedEventArgs e)
        {
            //Cotizacion.IsEnabled = false;
            txtEndPoint.Text ="Ejemplo de Url " + _apiUrl + "/api/Titulos/Cotizacion?simbolo=ALUA&Mercado=BCBA";
            //vieneDe = 1;
            //var datos = await EnviarPeticionGet(txtEndPoint.Text, null, txtBearerToken.Text);
            //AbrirVentana(vieneDe, datos.Cuerpo);
            //Cotizacion.IsEnabled = true;
            var ctv = new CotizacionTitulo();
            ctv.ShowDialog();
        }

        private async void btnPanelCotizacion_Click(object sender, RoutedEventArgs e)
        {
            txtEndPoint.Text = "Ejemplo de Url " + _apiUrl + "/api/Titulos/PanelCotizaciones?InstrumentosCotizacion=Accion&Panel=Merval&Pais=Argentina";
            var pcv = new PanelCotizacion();
            pcv.ShowDialog();
        }

        private void AbrirVentana(int vieneDe, string cuerpo)
        {
            switch (vieneDe)
            {
                case 1:
                    var ctv = new CotizacionTitulo(cuerpo);
                    ctv.ShowDialog();
                    break;
                case 2:
                    var pcv = new PanelCotizacion(cuerpo);
                    pcv.ShowDialog();
                    break;
                case 3:
                    var portfolio = new Portafolio(cuerpo);
                    portfolio.ShowDialog();
                    break;
                case 4:
                    var miCuenta = new MiCuenta(cuerpo);
                    miCuenta.ShowDialog();
                    break;
            }
        }

        private async void btnPortafolio_Click(object sender, RoutedEventArgs e)
        {
            btnPortafolio.IsEnabled = false;
            txtEndPoint.Text = _apiUrl + "/api/micuenta/miportafolio";
            vieneDe = 3;
            var datos = await EnviarPeticionGet(txtEndPoint.Text, null, txtBearerToken.Text);
            AbrirVentana(vieneDe, datos.Cuerpo);
            btnPortafolio.IsEnabled = true;
        }

        private async void btnMiCuenta_Click(object sender, RoutedEventArgs e)
        {
            btnMiCuenta.IsEnabled = false;
            txtEndPoint.Text = _apiUrl + "/api/micuenta/estadocuenta";
            vieneDe = 4;
            var datos = await EnviarPeticionGet(txtEndPoint.Text, null, txtBearerToken.Text);
            AbrirVentana(vieneDe, datos.Cuerpo);
            btnMiCuenta.IsEnabled = true;
        }

        private void btnMisOperaciones_Click(object sender, RoutedEventArgs e)
        {
            btnOperaciones.IsEnabled = false;
            var misOperaciones = new Operaciones();
            misOperaciones.ShowDialog();
            btnOperaciones.IsEnabled = true;
        }
    }

    public class RespuestaHTTP
    {
        public string Cuerpo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
