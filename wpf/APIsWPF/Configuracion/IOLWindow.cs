using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;

namespace APIsWPF.Configuracion
{
    public class IOLWindow : Window
    {
        public static HttpClient _client = new HttpClient();
        public static string _apiUrl;

        public static string _bearerToken;
        public static string _refreshToken;

        //User-Defined UI Configuration class containing System.Drawing.Color 
        //and Brush properties (platform-agnostic styling in your Project.Core.dll assembly)
        //public UIStyle UIStyle => Core.UIStyle.Current;

        //IValueConverter that converts System.Drawing.Color properties 
        //into WPF-equivalent Colors and Brushes 
        //You can skip this if you do not need or did not implement your own ValueConverter
        //public static IValueConverter UniversalValueConverter { get; } = new UniversalValueConverter();

        public async Task<RespuestaHTTP> EnviarPeticionGet(string url, Dictionary<string, string> parametrosDictionary, string token = "")
        {
            LimpiarCabecerasPeticion();
            if (!string.IsNullOrEmpty(token))
                AgregarTokenAPeticion(token);

            string parametrosString = ConvertirParametrosDiccionarioAString(parametrosDictionary);
            if (!string.IsNullOrEmpty(parametrosString))
                parametrosString = "?" + parametrosString;
            var respuesta = await IOLWindow._client.GetAsync(url + parametrosString);
            string cuerpo = await respuesta.Content.ReadAsStringAsync();

            var respuestaFormateada = new RespuestaHTTP()
            {
                Cuerpo = cuerpo,
                StatusCode = respuesta.StatusCode
            };

            return respuestaFormateada;
        }

        private string ConvertirParametrosDiccionarioAString(Dictionary<string, string> parametros)
        {
            string parametrosConvertidos = "";
            if (parametros != null)
            {
                foreach (string key in parametros.Keys)
                {
                    parametrosConvertidos += HttpUtility.UrlEncode(key) + "="
                          + HttpUtility.UrlEncode(parametros[key]) + "&";
                }
                parametrosConvertidos = parametrosConvertidos.Substring(0, parametrosConvertidos.Length - 1);
            }

            return parametrosConvertidos;
        }

        private void AgregarTokenAPeticion(string token)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        }

        private void LimpiarCabecerasPeticion()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RespuestaHTTP> EnviarPeticionPost(string url, Dictionary<string, string> parametrosDictionary, string token = "")
        {
            LimpiarCabecerasPeticion();
            if (!string.IsNullOrEmpty(token))
                AgregarTokenAPeticion(token);

            string parametrosString = ConvertirParametrosDiccionarioAString(parametrosDictionary);
            var parametros = new StringContent(parametrosString);
            var respuesta = await IOLWindow._client.PostAsync(url, parametros);
            string cuerpo = await respuesta.Content.ReadAsStringAsync();


            var respuestaFormateada = new RespuestaHTTP()
            {
                Cuerpo = cuerpo,
                StatusCode = respuesta.StatusCode
            };

            return respuestaFormateada;
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;

        private const int WS_MAXIMIZEBOX = 0x10000; //maximize button
        private const int WS_MINIMIZEBOX = 0x20000; //minimize button

        public IOLWindow()
        {
            
            this.SourceInitialized += MainWindow_SourceInitialized;
        }

        private IntPtr _windowHandle;
        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _windowHandle = new WindowInteropHelper(this).Handle;

            //disable minimize button
            DisableMinimizeButton();
        }

        protected void DisableMinimizeButton()
        {
            if (_windowHandle == IntPtr.Zero)
                throw new InvalidOperationException("The window has not yet been completely initialized");

            SetWindowLong(_windowHandle, GWL_STYLE, GetWindowLong(_windowHandle, GWL_STYLE) & ~WS_MAXIMIZEBOX);
        }
    }
}
