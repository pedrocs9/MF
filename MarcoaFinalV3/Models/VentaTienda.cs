using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class VentaTienda
    {
        public int IdVentaTienda { get; set; }
        public Usuario oUsuario { get; set; }
        public EstadoVentaTienda oEstadoVentaTienda { get; set; }
        public int TotalProducto { get; set; }
        public decimal Total { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public Comuna oComuna { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCompra { get; set; }

    }
}