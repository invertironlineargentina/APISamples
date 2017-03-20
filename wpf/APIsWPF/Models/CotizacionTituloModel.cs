using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsWPF.Models
{
    public class CotizacionModel
    {
        public decimal UltimoPrecio { get; set; }
        public decimal Variacion { get; set; }
        public decimal Apertura { get; set; }
        public decimal Maximo { get; set; }
        public decimal Minimo { get; set; }
        public decimal PuntosVariacion { get; set; }
        public DateTime FechaHora { get; set; }
        public TendenciaCotizacion Tendencia { get; set; }
        public decimal CierreAnterior { get; set; }
        public decimal MontoOperado { get; set; }
        public int CantidadOperaciones { get; set; }
        public decimal PrecioPromedio { get; set; }
        /// <summary>
        /// Se Carga si es una Cotización de Futuro
        /// </summary>
        public decimal PrecioAjuste { get; set; }
        /// <summary>
        /// Se Carga si es una Cotización de Futuro
        /// </summary>
        public decimal InteresesAbiertos { get; set; }
        /// <summary>
        /// Mejores puntas
        /// </summary>
        public PuntasModel PuntasUltimas { get; set; }
        /// <summary>
        /// Todas las puntas
        /// </summary>
        public IList<PuntasModel> Puntas { get; set; }
    }

    public class PuntasModel
    {
        public decimal CantidadCompra { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal CantidadVenta { get; set; }

    }
    public enum TendenciaCotizacion
    {
        Sube,
        Baja,
        Mantiene
    }
}