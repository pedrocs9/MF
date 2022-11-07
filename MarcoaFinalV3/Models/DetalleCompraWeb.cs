using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class DetalleCompraWeb
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public ProductoWeb oProductoWeb { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}