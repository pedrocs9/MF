using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Ventas()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }
        public ActionResult Mesa()
        {
            return View();
        }
        public ActionResult Vendedor()
        {
            return View();
        }


        public JsonResult ObtenerProducto(int idrestaurant, string codigoproducto)
        {
            List<ReporteProducto> lista = ReporteLogica.Instancia.ReporteProductoTienda(idrestaurant, codigoproducto);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerVenta(string fechainicio, string fechafin, int idrestaurant)
        {

            List<ReporteVenta> lista = ReporteLogica.Instancia.ReporteVenta(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin), idrestaurant);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}