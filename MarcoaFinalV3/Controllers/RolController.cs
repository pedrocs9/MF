using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Crear()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Obtener()
        {
            List<Rol> olista = RolLogica.Instancia.ObtenerRol();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Rol objeto)
        {
            bool respuesta = false;

            if (objeto.IdRol == 0)
            {

                respuesta = RolLogica.Instancia.RegistrarRol(objeto);
            }
            else
            {
                respuesta = RolLogica.Instancia.ModificarRol(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = RolLogica.Instancia.EliminarRol(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}