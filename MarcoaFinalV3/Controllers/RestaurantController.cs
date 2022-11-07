using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Crear()
        {
            return View();
        }

        public JsonResult Obtener()
        {
            List<Restaurant> lista = RestaurantLogica.Instancia.ObtenerRestaurantes();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Restaurant objeto)
        {
            bool respuesta = false;

            if (objeto.IdRestaurant == 0)
            {

                respuesta = RestaurantLogica.Instancia.RegistrarRestaurant(objeto);
            }
            else
            {
                respuesta = RestaurantLogica.Instancia.ModificarRestaurant(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = RestaurantLogica.Instancia.EliminarRestaurant(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}