using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class CompraController : Controller
    {
        private static Usuario SesionUsuario;
        // GET: Compra
        public ActionResult Crear()
        {
            SesionUsuario = (Usuario)Session["Usuario"];
            return View();
        }
        // GET: Compra
        public ActionResult Consultar()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Documento(int idcompra = 0)
        {

            Compra oCompra = CompraLogica.Instancia.ObtenerDetalleCompra(idcompra);

            if (oCompra == null)
            {
                oCompra = new Compra();
            }


            return View(oCompra);
        }


        public JsonResult Obtener(string fechainicio, string fechafin, int idproveedor, int idrestaurant)
        {
            List<Compra> lista = CompraLogica.Instancia.ObtenerListaCompra(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idproveedor, idrestaurant);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Guardar(string xml)
        {
            xml = xml.Replace("!idusuario¡", SesionUsuario.IdUsuario.ToString());

            bool respuesta = CompraLogica.Instancia.RegistrarCompra(xml);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}