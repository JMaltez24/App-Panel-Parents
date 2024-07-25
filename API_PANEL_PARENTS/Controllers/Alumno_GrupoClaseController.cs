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
    public class Alumno_GrupoClaseController : ApiController
    {
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////
        
        [HttpPost]
        [Route("api/alumnogrupoclase/registrar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult AgregarAlumnoGrupoClase()
        {
            var headers = Request.Headers;

            if (headers.Contains("Fk_Id_Alumno") && headers.Contains("Fk_Id_GrupoClase"))
            {
                int fkIdAlumno = Convert.ToInt32(headers.GetValues("Fk_Id_Alumno").FirstOrDefault());
                int fkIdGrupoClase = Convert.ToInt32(headers.GetValues("Fk_Id_GrupoClase").FirstOrDefault());

                try
                {
                    string mensaje = Alumno_GrupoClase_Metod.AgregarAlumnoGrupoClase(fkIdAlumno, fkIdGrupoClase);
                    if (mensaje.Contains("exitosamente"))
                    {
                        return Ok(mensaje);
                    }
                    else
                    {
                        return BadRequest(mensaje);
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
