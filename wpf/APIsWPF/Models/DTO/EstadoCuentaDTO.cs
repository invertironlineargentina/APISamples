using System.Collections.Generic;

namespace APIsWPF.Models.DTO
{
    public class EstadoCuentaDTO
    {
        public EstadoCuentaDTO() {
            SaldosMargen = new List<SaldoMargen>();
            SaldosCuenta = new List<SaldoCuenta>();
        }
        public List<TotalCuenta> DineroPorCuenta { get; set; }
        public List<SaldoMargen> SaldosMargen { get; set; }
        public List<SaldoCuenta> SaldosCuenta { get; set; }
    }
}
