
using MarcoaFinalV3.Models;
using MarcoaFinalV3.Logica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace MarcoaFinalV3.Controllers
{
    public class TiendaController : Controller
    {
        private static Usuario oUsuario;
        private static ProductoWeb oProductoWeb;
        
        // GET: Tienda

        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            else
                oUsuario = (Usuario)Session["Usuario"];

            return View();
        }
        public ActionResult Carrito()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            else
                oUsuario = (Usuario)Session["Usuario"];

            return View();
        }
        public ActionResult Compras()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            else
                oUsuario = (Usuario)Session["Usuario"];

            return View();
        }

        //VISTA
        public ActionResult Producto(int idproducto = 0)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            else
                oUsuario = (Usuario)Session["Usuario"];
                

            ProductoWeb oProducto = new ProductoWeb();
            List<ProductoWeb> oLista = new List<ProductoWeb>();

            oLista = ProductoWebLogica.Instancia.Listar();
            oProducto = (from o in oLista
                         where o.IdProducto == idproducto
                         select new ProductoWeb()
                         {
                             IdProducto = o.IdProducto,
                             Nombre = o.Nombre,
                             Descripcion = o.Descripcion,
                             oMarca = o.oMarca,
                             oCategoria = o.oCategoria,
                             Precio = o.Precio,
                             Stock = o.Stock,
                             RutaImagen = o.RutaImagen,
                             base64 = utilidades.convertirBase64(Server.MapPath(o.RutaImagen)),
                             extension = Path.GetExtension(o.RutaImagen).Replace(".", ""),
                             Activo = o.Activo
                         }).FirstOrDefault();

            return View(oProducto);
        }

        [HttpPost]
        public JsonResult ListarProducto(int idcategoria = 0)
        {
            List<ProductoWeb> oLista = new List<ProductoWeb>();

            oLista = ProductoWebLogica.Instancia.Listar();
            oLista = (from o in oLista
                      select new ProductoWeb()
                      {
                          IdProducto = o.IdProducto,
                          Nombre = o.Nombre,
                          Descripcion = o.Descripcion,
                          oMarca = o.oMarca,
                          oCategoria = o.oCategoria,
                          Precio = o.Precio,
                          Stock = o.Stock,
                          RutaImagen = o.RutaImagen,
                          base64 = utilidades.convertirBase64(Server.MapPath(o.RutaImagen)),
                          extension = Path.GetExtension(o.RutaImagen).Replace(".", ""),
                          Activo = o.Activo
                      }).ToList();

            if (idcategoria != 0)
            {
                oLista = oLista.Where(x => x.oCategoria.IdCategoria == idcategoria).ToList();
            }

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = CategoriaLogica.Instancia.ObtenerCategoria();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertarCarrito(Carrito oCarrito)
        {
            oCarrito.oUsuario = new Usuario() { IdUsuario = oUsuario.IdUsuario };
            
            int _respuesta = 0;
            _respuesta = CarritoLogica.Instancia.Registrar(oCarrito);
            return Json(new { respuesta = _respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CantidadCarrito()
        {
            int _respuesta = 0;
            _respuesta = CarritoLogica.Instancia.Cantidad(oUsuario.IdUsuario);
            return Json(new { respuesta = _respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerCarrito()
        {
            List<Carrito> oLista = new List<Carrito>();
            oLista = CarritoLogica.Instancia.Obtener(oUsuario.IdUsuario);

            if (oLista.Count != 0)
            {
                oLista = (from d in oLista
                          select new Carrito()
                          {
                              IdCarrito = d.IdCarrito,
                              oProductoWeb = new ProductoWeb()
                              {
                                  IdProducto = d.oProductoWeb.IdProducto,
                                  Nombre = d.oProductoWeb.Nombre,
                                  oMarca = new Marca() { Descripcion = d.oProductoWeb.oMarca.Descripcion },
                                  Precio = d.oProductoWeb.Precio,
                                  RutaImagen = d.oProductoWeb.RutaImagen,
                                  base64 = utilidades.convertirBase64(Server.MapPath(d.oProductoWeb.RutaImagen)),
                                  extension = Path.GetExtension(d.oProductoWeb.RutaImagen).Replace(".", ""),
                              }
                          }).ToList();
            }


            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCarrito(string IdCarrito, string IdProducto)
        {
            bool respuesta = false;
            respuesta = CarritoLogica.Instancia.Eliminar(IdCarrito, IdProducto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

       
        

       


       

        //[HttpPost]
        //public JsonResult GuardarProducto(string objeto, HttpPostedFileBase imagenArchivo)
        //{

        //    Response oresponse = new Response() { resultado = true, mensaje = "" };

        //    try
        //    {
        //        ProductoWeb oProducto = new ProductoWeb();
        //        oProducto = JsonConvert.DeserializeObject<ProductoWeb>(objeto);

        //        string GuardarEnRuta = "~/Imagenes/Productos/";
        //        string physicalPath = Server.MapPath("~/Imagenes/Productos");

        //        if (!Directory.Exists(physicalPath))
        //            Directory.CreateDirectory(physicalPath);

        //        if (oProducto.IdProducto == 0)
        //        {
        //            int id = ProductoWebLogica.Instancia.Registrar(oProducto);
        //            oProducto.IdProducto = id;
        //            oresponse.resultado = oProducto.IdProducto == 0 ? false : true;

        //        }
        //        else
        //        {
        //            oresponse.resultado = ProductoWebLogica.Instancia.Modificar(oProducto);
        //        }


        //        if (imagenArchivo != null && oProducto.IdProducto != 0)
        //        {
        //            string extension = Path.GetExtension(imagenArchivo.FileName);
        //            GuardarEnRuta = GuardarEnRuta + oProducto.IdProducto.ToString() + extension;
        //            oProducto.RutaImagen = GuardarEnRuta;

        //            imagenArchivo.SaveAs(physicalPath + "/" + oProducto.IdProducto.ToString() + extension);

        //            oresponse.resultado = ProductoWebLogica.Instancia.ActualizarRutaImagen(oProducto);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        oresponse.resultado = false;
        //        oresponse.mensaje = e.Message;
        //    }

        //    return Json(oresponse, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult EliminarProductoWeb(int id)
        //{
        //    bool respuesta = false;
        //    respuesta = ProductoWebLogica.Instancia.Eliminar(id);
        //    return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        //}



       

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }

    }
    public class Response
    {

        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }
}