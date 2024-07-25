using API_PANEL_PARENTS.Data;
using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static API_PANEL_PARENTS.Data.PadreAlumnoRelacion_Metod;


namespace API_PANEL_PARENTS.Controllers
{
    public class PadreAlumnoRelacion_Controller : ApiController
    {

        ////////////////////////////////////////////////////JOEL////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////esta vaina es para relacionar a un papa con un alumno//////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////APK APK APK////////////////////////////////////////////
        [HttpPost]
        [Route("api/padre/alumno/agregar")]
        [AppAuthorize(AppType = "mobile")]
        public IHttpActionResult AgregarPadreAlumno()
        {
            var headers = Request.Headers;

            if (headers.Contains("Id_Padre") && headers.Contains("Codigo"))
            {
                string idPadreStr = headers.GetValues("Id_Padre").FirstOrDefault();
                string codigo = headers.GetValues("Codigo").FirstOrDefault();

                if (int.TryParse(idPadreStr, out int idPadre))
                {
                    string resultado = Padre_Metod.AgregarPadreAlumno(idPadre, codigo);

                    if (resultado == "Relación padre-alumno agregada exitosamente.")
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest(resultado);
                    }
                }
                else
                {
                    return BadRequest("Id_Padre inválido.");
                }
            }
            else
            {
                return BadRequest("Faltan datos de autenticación.");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////FIN////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////
        /// 
        ////////////////////////////////APK APK APK////////////////////////////////////////////

        [HttpGet]
        [Route("api/alumnos/de/padre")]
        [AppAuthorize(AppType = "mobile")]
        public IHttpActionResult MostrarAlumnosPadre()
        {
            var headers = Request.Headers;

            if (headers.Contains("Id_Padre"))
            {
                int idPadre = Convert.ToInt32(headers.GetValues("Id_Padre").FirstOrDefault());

                try
                {
                    var alumnos = PadreAlumnoRelacion_Metod.MostrarAlumnosdePadre(idPadre);
                    if (alumnos != null && alumnos.Count > 0)
                    {
                        return Ok(alumnos);
                    }
                    else
                    {
                        return Ok("Aun no se ha relacionado a ningun alumno");
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
                return BadRequest("Faltan datos en las cabeceras.");
            }
        }
    }
}