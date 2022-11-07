using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Crear()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
             return View();
        }

        // GET: Producto
        public ActionResult Asignar()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = ProductoLogica.Instancia.ObtenerProducto().OrderBy(o => o.Nombre).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            respuesta = ProductoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPorRestaurant(int IdRestaurant)
        {

            List<Producto> oListaProducto = ProductoLogica.Instancia.ObtenerProducto();
            List<ProductoTienda> oListaProductoTienda = ProductoTiendaLogica.Instancia.ObtenerProductoTienda();

            oListaProducto = oListaProducto.Where(x => x.Estado == true).ToList();
            if (IdRestaurant != 0)
            {
                oListaProductoTienda = oListaProductoTienda.Where(x => x.oRestaurant.IdRestaurant == IdRestaurant).ToList();
                oListaProducto = (from producto in oListaProducto
                                  join productotienda in oListaProductoTienda on producto.IdProducto equals productotienda.oProducto.IdProducto
                                  where productotienda.oRestaurant.IdRestaurant == IdRestaurant
                                  select producto).ToList();
            }

            return Json(new { data = oListaProducto }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(Producto objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdProducto == 0) ? ProductoLogica.Instancia.Registrar(objeto) : ProductoLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int id = 0)
        {
            bool respuesta = ProductoLogica.Instancia.Eliminar(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarProductoTienda(ProductoTienda objeto)
        {
            bool respuesta = ProductoTiendaLogica.Instancia.RegistrarProductoTienda(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ModificarProductoTienda(ProductoTienda objeto)
        {
            bool respuesta = ProductoTiendaLogica.Instancia.ModificarProductoTienda(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarProductoTienda(int id)
        {
            bool respuesta = ProductoTiendaLogica.Instancia.EliminarProductoTienda(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ObtenerAsignaciones()
        {
            List<ProductoTienda> lista = ProductoTiendaLogica.Instancia.ObtenerProductoTienda();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}