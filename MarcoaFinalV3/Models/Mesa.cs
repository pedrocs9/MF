using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Mesa
    {
        public int IdMesa { get; set; }
        public string Numero { get; set; }
        public string Detalle { get; set; }
        public EstadoMesa oEstadoMesa { get; set; }
        public CapacidadMesa oCapacidadMesa { get; set; }
        public bool Estado { get; set; }
        
    }
}