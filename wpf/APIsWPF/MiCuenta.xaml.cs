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
    public partial class MiCuenta : IOLWindow
    {
        IOLWindow a = new IOLWindow();
        private EstadoCuentaDTO dto; 
        private string _json;

        public MiCuenta()
        {
            InitializeComponent();
        }

        public MiCuenta(string json)
        {
            InitializeComponent();
            dto = new EstadoCuentaDTO();
            _json = json;
            Inicializar(_json);
        }

        private void Inicializar(string json)
        {
            var estadoCuenta   = ObtenerJson(json);
            GridDineroPorCuenta.ItemsSource = estadoCuenta.DineroPorCuenta;
            GridSaldosCuenta.ItemsSource = estadoCuenta.SaldosCuenta;
            GridSaldosMargen.ItemsSource = estadoCuenta.SaldosMargen;
            
           
        }

        private EstadoCuentaDTO ObtenerJson(string jsonStr)
        {
             dto = JsonConvert.DeserializeObject<EstadoCuentaDTO>(jsonStr);
            string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            txtJson.Document.Blocks.Clear();
            txtJson.Document.Blocks.Add(new Paragraph(new Run(json)));
            return dto;
        }

        private void SaldosCuenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = (SaldoCuenta) GridSaldosCuenta.SelectedItem;
            var saldos = s.Saldos;
            GridSaldosHs.ItemsSource = saldos;
        }
    }
}
