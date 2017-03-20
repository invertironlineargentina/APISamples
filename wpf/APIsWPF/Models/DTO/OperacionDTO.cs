using System.Collections.Generic;

namespace APIsWPF.Models.DTO
{
    public class OperacionDTO
    {
        public OperacionDTO() {
            Operaciones = new List<OperacionModelDTO>();
        }
        public List<OperacionModelDTO> Operaciones { get; set; }
    }
}
