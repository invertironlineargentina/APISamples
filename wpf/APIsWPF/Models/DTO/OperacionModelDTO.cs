using System;

namespace APIsWPF.Models.DTO
{
    public class OperacionModelDTO
    {
        public int NumeroTransaccion { get; set; }
        public DateTime FechaOrden { get; set; }
        public string TipoOperacion { get; set; }
        public string EstadoOperacion { get; set; }
        public string Simbolo { get; set; }
        public decimal CantidadMonto { get; set; }
        public string OperacionAPrecio { get; set; }
        public DateTime FechaOperada { get; set; }
        public decimal CantidadOperada { get; set; }
        public decimal PrecioOperado { get; set; }
        public decimal MontoOperado { get; set; }
    }
}
