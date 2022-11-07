using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class GestionController : Controller
    {
        // GET: Gestion
        public ActionResult Recepcion()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        

        public ActionResult RecepcionRegistro(int idmesa)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            Mesa objeto = MesaLogica.Instancia.Listar().Where(h => h.IdMesa == idmesa).FirstOrDefault();

            return View(objeto);
        }

        public ActionResult DetalleRecepcion(int idmesa)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            Recepcion oRecepcion = RecepcionLogica.Instancia.Listar().Where(h => h.oMesa.IdMesa == idmesa && h.Estado == true).FirstOrDefault();

            if (oRecepcion != null)
            {

                List<Venta> oVenta = (from vn in VentaLogica.Instancia.Listar()
                                      where vn.oRecepcion.IdRecepcion == oRecepcion.IdRecepcion
                                      select new Venta()
                                      {
                                          IdVenta = vn.IdVenta,
                                          oRecepcion = new Recepcion() { IdRecepcion = vn.oRecepcion.IdRecepcion },
                                          Total = vn.Total,
                                          Estado = vn.Estado,
                                          oDetalleVenta = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.IdVenta == vn.IdVenta).ToList()
                                      }).ToList();

                oRecepcion.oVenta = oVenta;
            }

            return View(oRecepcion);
        }


        public ActionResult Salida()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Gestion
        public ActionResult SalidaRegistro(int idmesa)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            Recepcion oRecepcion = RecepcionLogica.Instancia.Listar().Where(h => h.oMesa.IdMesa == idmesa && h.Estado == true).FirstOrDefault();

            if (oRecepcion != null)
            {

                List<Venta> oVenta = (from vn in VentaLogica.Instancia.Listar()
                                      where vn.oRecepcion.IdRecepcion == oRecepcion.IdRecepcion
                                      select new Venta()
                                      {
                                          IdVenta = vn.IdVenta,
                                          oRecepcion = new Recepcion() { IdRecepcion = vn.oRecepcion.IdRecepcion },
                                          Total = vn.Total,
                                          Estado = vn.Estado,
                                          oDetalleVenta = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.IdVenta == vn.IdVenta).ToList()
                                      }).ToList();

                oRecepcion.oVenta = oVenta;
            }

            return View(oRecepcion);
        }

        // GET: Gestion
        public ActionResult Venta(int idmesa)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            Recepcion objeto = RecepcionLogica.Instancia.Listar().Where(h => h.oMesa.IdMesa == idmesa && h.Estado == true).FirstOrDefault();

            return View(objeto);
        }

        [HttpGet]
        public JsonResult ListarMesa(int idcapacidad)
        {
            List<Mesa> oLista = new List<Mesa>();
            oLista = MesaLogica.Instancia.Listar().Where(x => x.oCapacidadMesa.IdCapacidadMesa == (idcapacidad == 0 ? x.oCapacidadMesa.IdCapacidadMesa : idcapacidad)).OrderBy(o => o.IdMesa).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = ProductoLogica.Instancia.ObtenerProducto().OrderBy(o => o.Nombre).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ActualizarEstadoMesa(int idmesa, int idestadomesa)
        {

            bool respuesta = false;
            respuesta = MesaLogica.Instancia.ActualizarEstado(idmesa, idestadomesa);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarPersona(Recepcion objeto)
        {
            bool respuesta = false;
            respuesta = RecepcionLogica.Instancia.Registrar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarCapacidad()
        {
            List<CapacidadMesa> oLista = new List<CapacidadMesa>();
            oLista = CapacidadMesaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CerrarRecepcion(Recepcion objeto)
        {
            bool respuesta = false;
            respuesta = RecepcionLogica.Instancia.Cerrar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RegistrarVenta(Venta objeto)
        {
            bool respuesta = false;
            respuesta = VentaLogica.Instancia.Registrar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }




    }
}