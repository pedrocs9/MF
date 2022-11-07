using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Crear()
        {
            return View();
        }

        public JsonResult Obtener()
        {
            List<Proveedor> olista = ProveedorLogica.Instancia.ObtenerProveedor();
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Proveedor objeto)
        {
            bool respuesta = false;

            if (objeto.IdProveedor == 0)
            {

                respuesta = ProveedorLogica.Instancia.RegistrarProveedor(objeto);
            }
            else
            {
                respuesta = ProveedorLogica.Instancia.ModificarProveedor(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = ProveedorLogica.Instancia.EliminarProveedor(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}