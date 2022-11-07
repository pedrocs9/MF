using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Crear()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        public JsonResult Obtener()
        {
            List<Categoria> lista = CategoriaLogica.Instancia.ObtenerCategoria();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Categoria objeto)
        {
            bool respuesta = false;

            if (objeto.IdCategoria == 0)
            {

                respuesta = CategoriaLogica.Instancia.RegistrarCategoria(objeto);
            }
            else
            {
                respuesta = CategoriaLogica.Instancia.ModificarCategoria(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = CategoriaLogica.Instancia.EliminarCategoria(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}