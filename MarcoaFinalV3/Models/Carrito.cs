
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public ProductoWeb oProductoWeb { get; set; }
        public Usuario oUsuario { get; set; }
    }
}