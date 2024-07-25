using API_PANEL_PARENTS.Data;
using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_PANEL_PARENTS.Controllers
{
    public class PadreController : ApiController
    {

        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////
        [HttpPost]
        [Route("api/padre/registrar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult RegistrarPadre()
        {
            var headers = Request.Headers;

            // Obtener datos del padre de las cabeceras
            if (headers.Contains("Nombre") &&
                headers.Contains("Usuario") &&
                headers.Contains("Contraseniaa"))
            {
                string nombre = headers.GetValues("Nombre").FirstOrDefault();
                string usuario = headers.GetValues("Usuario").FirstOrDefault();
                string contraseniaa = headers.GetValues("Contraseniaa").FirstOrDefault();

                // Crear objeto Padre con los datos recibidos
                Padre padre = new Padre
                {
                    Nombre = nombre,
                    Usuario = usuario,
                    Contraseniaa = contraseniaa
                };

                // Llamar al método de la capa de datos para registrar al padre
                bool resultado = Padre_Metod.Registrar(padre);
                if (resultado)
                {
                    return Ok("Padre registrado con éxito.");
                }
                else
                {
                    return InternalServerError();
                }
            }
            else
            {
                return BadRequest("Faltan datos en las cabeceras.");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        /////////////Este Endpoint es para autenticar a los padres desde la apk////////////////
        /////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////APK APK APK////////////////////////////////////////////
        [HttpGet]
        [Route("api/padre/autenticar")]
        public IHttpActionResult AutenticarPadre()
        {
            var headers = Request.Headers;

            if (headers.Contains("Usuario") && headers.Contains("Contrasena"))
            {
                string usuario = headers.GetValues("Usuario").FirstOrDefault();
                string contrasena = headers.GetValues("Contrasena").FirstOrDefault();

                int resultado = int.Parse(Padre_Metod.Autenticar(usuario, contrasena));

                if (resultado > 0)
                {
                    // Generar el token JWT para la aplicación móvil
                    var token = JwtTokenGenerator.GenerateToken(usuario, "mobile");
                    return Ok(new
                    {
                        Token = token,
                        ID = resultado
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest("Faltan datos de autenticación.");
            }
        }

        ///




        ////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////FIN////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////


    }
}
