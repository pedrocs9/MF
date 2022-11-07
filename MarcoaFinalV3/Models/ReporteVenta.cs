using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class ReporteVenta
    {
        public string FechaVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreRestaurant { get; set; }
        public string RucRestaurant { get; set; }
        public string NombreEmpleado { get; set; }
        public string CantidadUnidadesVendidas { get; set; }
        public string CantidadProductos { get; set; }
        public string TotalVenta { get; set; }
    }
}