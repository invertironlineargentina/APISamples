using System;

namespace APIsWPF.Models.DTO
{
    public class PorfolioActivoDTO
    {
        public int IdTitulo { get; set; }
        public string Simbolo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal TitulosComprometido { get; set; }
        public decimal VariacionDiaria { get; set; }
        public decimal UltimoPrecio { get; set; }
        public decimal PPC { get; set; }
        public decimal Ganacia { get; set; }
        public decimal Perdida { get; set; }
        public decimal Valorizado { get; set; }
        public string Mercado { get; set; }
        public string Vender => string.Concat(Simbolo,"-" ,Mercado);
    }
}
