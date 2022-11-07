using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class ReporteProducto
    {
        public string RucRestaurant { get; set; }
        public string NombreRestaurant { get; set; }
        public string DireccionRestaurant { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string StockenRestaurant { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioVenta { get; set; }
    }
}