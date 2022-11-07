using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class MesaController : Controller
    {
        // GET: Mesa
        public ActionResult Crear()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        [HttpGet]
        public JsonResult ListarCapacidadMesa()
        {
            List<CapacidadMesa> oLista = new List<CapacidadMesa>();
            oLista = CapacidadMesaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarCapacidadMesa(CapacidadMesa objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdCapacidadMesa == 0) ? CapacidadMesaLogica.Instancia.RegistrarCapacidadMesa(objeto) : CapacidadMesaLogica.Instancia.ModificarCapacidadMesa(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarCapacidadMesa(int id)
        {
            bool respuesta = false;
            respuesta = CapacidadMesaLogica.Instancia.EliminarCapacidadMesa(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarMesa()
        {
            List<Mesa> oLista = new List<Mesa>();
            oLista = MesaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarMesa(Mesa objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdMesa == 0) ? MesaLogica.Instancia.Registrar(objeto) : MesaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarMesa(int id)
        {
            bool respuesta = false;
            respuesta = MesaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}