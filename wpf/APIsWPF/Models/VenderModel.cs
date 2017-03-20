using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsWPF.Models
{
    public class VenderModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Validez { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PrecioLimite { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IdOperador { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Simbolo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mercado { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Modalidad { get; set; }
    }
    public enum Modalidad
    {
        PrecioMercado = 1,
        PrecioLimite = 2
    }
}
