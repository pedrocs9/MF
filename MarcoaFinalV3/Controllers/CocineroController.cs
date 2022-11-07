using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class CocineroController : Controller
    {
        // GET: Cocinero
        public ActionResult Platos()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarPlatos()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = ProductoLogica.Instancia.ListarPlatos().OrderBy(o => o.Nombre).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        
    }
}