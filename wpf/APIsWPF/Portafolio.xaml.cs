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
    public partial class Portafolio : IOLWindow
    {
        IOLWindow a = new IOLWindow();
        private string _json;

        public Portafolio()
        {
            InitializeComponent();
        }

        public Portafolio(string json)
        {
            InitializeComponent();
            _json = json;
            Inicializar(_json);
        }

        private void Inicializar(string json)
        {
            var portafolio = ObtenerJson(json);
            dataGrid.ItemsSource = portafolio.PorfolioActivo;
            
        }

        private MiPorfolioDTO ObtenerJson(string jsonStr)
        {
            var dto = JsonConvert.DeserializeObject<MiPorfolioDTO>(jsonStr);
            string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            txtJson.Document.Blocks.Clear();
            txtJson.Document.Blocks.Add(new Paragraph(new Run(json)));
            return dto;
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

        private async void ButtonVender_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var split = btn.Content.ToString().Split('-');
            var simbolo = split[0];
            var mercado = split[1];
            var uri = IOLWindow._apiUrl + "/api/Titulos/Cotizacion?Simbolo=" + simbolo +"&Mercado="+mercado;
            var datos = await EnviarPeticionGet(uri, null, _bearerToken);
            var ct = new Vender(datos.Cuerpo,simbolo,mercado);
            ct.ShowDialog();

        }
    }
}
