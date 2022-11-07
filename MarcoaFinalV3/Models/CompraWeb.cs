using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class CompraWeb
    {
        public int IdCompra { get; set; }
        public int IdUsuario { get; set; }
        public string TotalProducto { get; set; }
        public decimal Total { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string IdComuna { get; set; }
        public string FechaTexto { get; set; }
        public List<DetalleCompraWeb> oDetalleCompraWeb { get; set; }
    }
}