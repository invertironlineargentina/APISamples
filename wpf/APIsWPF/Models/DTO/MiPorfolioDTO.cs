using System.Collections.Generic;

namespace APIsWPF.Models.DTO
{
    public class MiPorfolioDTO
    {
        public MiPorfolioDTO()
        {
            PorfolioActivo = new List<PorfolioActivoDTO>();
        }
        public List<PorfolioActivoDTO> PorfolioActivo { get; set; }
    }
}
