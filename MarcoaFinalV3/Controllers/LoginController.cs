
using MarcoaFinalV3.Logica;
using MarcoaFinalV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarcoaFinalV3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string correo, string password)
        {


            Usuario ousuario = UsuarioLogica.Instancia.ObtenerUsuarios().Where(u => u.Correo == correo && u.Clave == password).FirstOrDefault();

            if (ousuario == null)
            {
                ViewBag.Error = "Usuario o contraseña no correcta";
                return View();
            }

            Session["Usuario"] = ousuario;

            if (ousuario.IdRol == 4)
            {
                return RedirectToAction("Index", "Tienda");
            }
            else if(ousuario.IdRol == 1)
            {
                return RedirectToAction("IndexAdmin", "Home");
            }else if(ousuario.IdRol == 2)
            {
                return RedirectToAction("IndexVendedor", "Home");
            }
            else
            {
                return RedirectToAction("IndexCocinero", "Home");
            }


        }
        public ActionResult Registrarse()
        {
            return View(new Usuario() { Nombres = "", Apellidos = "", Correo = "", Clave = "", ConfirmarClave = "", IdRol = 0, IdRestaurant = 0 });
        }

        [HttpPost]
        public ActionResult Registrarse(string NNombres, string NApellidos, string NCorreo, string NContrasena, string NConfirmarContrasena)
        {
            
            Usuario oUsuario = new Usuario()
            {
                Nombres = NNombres,
                Apellidos = NApellidos,
                Correo = NCorreo,
                Clave = NContrasena,
                ConfirmarClave = NConfirmarContrasena,
                IdRestaurant = 1,
                IdRol = 4
                

            };
            if (NNombres == "" & NApellidos == "" & NCorreo == "" & NContrasena == "" & NConfirmarContrasena == "")
            {
                ViewBag.Error2 = "Ingrese Datos Solicitados";
                return View(oUsuario);
            }

            if (NContrasena != NConfirmarContrasena)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View(oUsuario);
            }
            else
            {


                bool idusuario_respuesta = UsuarioLogica.Instancia.RegistrarUsuario(oUsuario);
                ViewBag.Mensaje = "Usuario Creado Correctamente";

                if (idusuario_respuesta == false)
                {
                    ViewBag.Error = "Error al registrar";
                    return View();

                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }


    }
}