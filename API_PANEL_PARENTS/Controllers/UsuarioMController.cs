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
    public class UsuarioMController : ApiController
    {

        /////////////////////////////////////////////////////////////////////////////////
        //////////////////////////REGISTRAR MAESTROS/////////////////////////////////////
        //aqui se envia por el fromBody la info necesaria 
        //este metodo de controlador es para recibir la solicitud del consumidor
        ////////////////////////////////////////////////////////////////////////////////
        ///

        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////
        [HttpPost]
        [Route("api/usuariom/registrar")]
        public IHttpActionResult C_registrar([FromBody] UsuarioM usuarioM)
        {
            bool resultado = UsuarioM_Metod.Registrar(usuarioM);
            if (resultado)
            {
                return Ok("Usuario registrado con éxito.");
            }
            else
            {
                return InternalServerError();
            }
        }


        ////////////////////////////////////////////////////
        /// este metodo es para autenticar a los usuarios
        //////////////////////////////////////////////////

        //[HttpGet]
        //[Route("api/usuariom/autenticarm")]
        //public IHttpActionResult Autenticar()
        //{
        //    // Obtener las cabeceras
        //    var headers = Request.Headers;
        //    if (headers.Contains("Usuario") && headers.Contains("Contrasenia"))
        //    {
        //        var credenciales = new UsuarioM
        //        {
        //            Usuario = headers.GetValues("Usuario").FirstOrDefault(),
        //            Contrasenia = headers.GetValues("Contrasenia").FirstOrDefault()
        //        };

        //        bool resultado = UsuarioM_Metod.Autenticar(credenciales);
        //        if (resultado)
        //        {
        //            return Ok("Autenticación exitosa.");

        //        }
        //        else
        //        {
        //            return Unauthorized();
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Faltan credenciales.");
        //    }
        //}

        /// <summary>
        /// Endpoint para autenticar usuarios y generar un token JWT.
        /// </summary>
        /// <returns>Resultado de la autenticación y el token JWT si es exitoso.</returns>
        /// 


        ////////////////////////////////WINDONWS FORM/////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////AQUI ES TA LA AUTENTICACION//////////////////////////////////
        ///////////////////////////////////DE WINDOWNS FORM///////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        ///

        [HttpGet]
        [Route("api/usuariom/autenticarm")]
        public IHttpActionResult Autenticar()
        {
            var headers = Request.Headers;

            if (headers.Contains("Usuario") && headers.Contains("Contrasenia"))
            {
                var credenciales = new UsuarioM
                {
                    Usuario = headers.GetValues("Usuario").FirstOrDefault(),
                    Contrasenia = headers.GetValues("Contrasenia").FirstOrDefault()
                };

                bool resultado = UsuarioM_Metod.Autenticar(credenciales);
                if (resultado)
                {
                    // Generar el token JWT para la aplicación de escritorio
                    var token = JwtTokenGenerator.GenerateToken(credenciales.Usuario, "desktop");
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest("Faltan credenciales.");
            }
        }



        ///////////////////////////////////////////////////////////////////////////////
        ///////////ESTE METODO MUESTRA TODOS LOS MAESTROS/////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        ///

        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////

        [HttpGet]
        [Route("api/maestro/listarmaestro")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult ListarMaestros()
        {
            List<UsuarioM> maestros = UsuarioM_Metod.Obtener_Maestros();

            if (maestros != null && maestros.Count > 0)
            {
                return Ok(maestros);
            }
            else
            {
                return NotFound();
                

            }
        }



    }
}
