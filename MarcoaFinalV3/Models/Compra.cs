using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public string Codigo { get; set; }
        public string FechaCompra { get; set; }
        public string NumeroCompra { get; set; }
        public string FechaTexto { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }
        public Restaurant oRestaurant { get; set; }
        public List<DetalleCompra> oDetalleCompra { get; set; }
        public decimal TotalCosto { get; set; }
        public string TipoComprobante { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}