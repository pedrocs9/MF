using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class EstadoVentaTienda
    {
        public int IdDetalleVentaTienda { get; set; }
        public VentaTienda oVentaTienda { get; set; }
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}