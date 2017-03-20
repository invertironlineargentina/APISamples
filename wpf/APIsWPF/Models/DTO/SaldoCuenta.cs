using System.Collections.Generic;

namespace APIsWPF.Models.DTO
{
    public class SaldoCuenta
    {
        public string DescripcionCuenta { get; set; }
        public string Moneda { get; set; }
        public decimal Disponible { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Liquido { get; set; }        
        public List<SaldoDescripcion> Saldos {get;set;}
        public SaldoCuenta() {
            Saldos = new List<SaldoDescripcion>();
        }
    }

    public class SaldoDescripcion
    {
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public override string ToString()
        {
            return $"{Descripcion}-{Monto}";
        }
    }
}
