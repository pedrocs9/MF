using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Recepcion
    {
        public int IdRecepcion { get; set; }
        public Usuario oUsuario { get; set; }
        public Mesa oMesa { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string FechaEntradaTexto { get; set; }
        

        public decimal PrecioInicial { get; set; }
        public string PrecioIncialTexto { get; set; }


        public decimal Adelanto { get; set; }
        public string AdelantoTexto { get; set; }
        public decimal PrecioRestante { get; set; }
        public string PrecioRestanteTexto { get; set; }

        public decimal TotalPagado { get; set; }
        public string TotalPagadoTexto { get; set; }

        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public List<Venta> oVenta { get; set; }
    }
}