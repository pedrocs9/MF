using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using MarcoaFinalV3.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Crear()
        {
            return View();
        }
        public JsonResult Obtener()
        {
            List<Usuario> oListaUsuario = UsuarioLogica.Instancia.ObtenerUsuarios();
            return Json(new { data = oListaUsuario }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Usuario objeto)
        {
            bool respuesta = false;

            if (objeto.IdUsuario == 0)
            {
                objeto.Clave = Encriptar.GetSHA256(objeto.Clave);

                respuesta = UsuarioLogica.Instancia.RegistrarUsuario(objeto);
            }
            else
            {
                respuesta = UsuarioLogica.Instancia.ModificarUsuario(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = UsuarioLogica.Instancia.EliminarUsuario(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}