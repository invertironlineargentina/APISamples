using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ProtoBuf;
//using IOL.Framework.Common.Services;

namespace APIsWPF.Models
{
    public enum TipoPanel
    {
        Activos,
        Divisas,
        Cauciones,
        Indices,
        Chpd,
        Futuros,
    }
    
    //[ProtoContract]
    public class PanelModel
    {
        public PanelModel()
        {
            this.TipoPanel = TipoPanel.Activos;
        }

        public PanelModel(TipoPanel panel)
        {
            this.TipoPanel = panel;
        }

        public string SimboloPanel { get; set; }

        public UInt16? MinutosDelay { get; set; }

        public TipoPanel TipoPanel { get; set; }

        public PanelRow[] Rows { get; set; }


    }
    public class PanelRow
    {
        public PanelRow()
        {
        }

        public PanelRow(PanelRowTitulo titulo)
        {
            this.Titulo = titulo;
        }

        public PanelRowTitulo Titulo { get; set; }

        public PuntasModelPanel Puntas { get; set; }

        public decimal? PrecUlt { get; set; }

        public decimal? VarPorc { get; set; }

        public decimal? Apertura { get; set; }

        public decimal? Maximo { get; set; }

        public decimal? Minimo { get; set; }

        public decimal? UltCierre { get; set; }

        public UInt64? Volumen { get; set; }

        public UInt64? CantOperac { get; set; }

        public DateTime? FechHora { get; set; }

        public string TipoOpcion { get; set; }

        public decimal? PrecioEjercicio { get; set; }

        public string FechaVencimiento { get; set; }

        public int? IdMercado { get; set; }

        public int? IdMoneda { get; set; }

    }

    public class PanelRowTitulo
    {
        public PanelRowTitulo()
        {
        }

        public PanelRowTitulo(string simbolo, int? id)
            : this()
        {
            this.Simbolo = simbolo;
            this.ID = id;
        }

        public string Simbolo { get; set; }

        public int? ID { get; set; }

        public override string ToString()
        {
            return this.Simbolo.ToUpper().Trim() + (ID != null ? " (" + ID.Value.ToString() + ")" : "");
        }
    }

    public class PuntasModelPanel
    {
        public decimal cantComp { get; set; }
        public decimal precComp { get; set; }
        public decimal precVent { get; set; }
        public decimal cantVent { get; set; }

    }
}
