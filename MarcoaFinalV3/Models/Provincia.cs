using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Provincia
    {
        public int IdProvincia { get; set; }
        public int IdRegion { get; set; }
        public Region oRegion { get; set; }
        public string Descripcion { get; set; }
    }
}