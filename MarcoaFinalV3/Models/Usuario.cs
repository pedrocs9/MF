using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcoaFinalV3.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public int IdRestaurant { get; set; }
        public Restaurant oRestaurant{ get; set; }
        public int IdRol { get; set; }
        public Rol oRol { get; set; }
        public List<Menu> oListaMenu { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}