using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class ProductoTienda
    {
        public int IdProductoTienda { get; set; }
        public Producto2 oProducto { get; set; }
        public Restaurant oRestaurant { get; set; }
        public int Stock { get; set; }
        public decimal PrecioUnidadCompra { get; set; }
        public decimal PrecioUnidadVenta { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Iniciado { get; set; }
    }
}