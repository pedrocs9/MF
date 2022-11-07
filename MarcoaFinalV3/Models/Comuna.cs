using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Comuna
    {
        public int IdComuna { get; set; }
        public Provincia oProvincia { get; set; }
        public int IdProvincia { get; set; }
        public int IdRegion { get; set; }
        public string Descripcion { get; set; }
    }

}