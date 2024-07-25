using API_PANEL_PARENTS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_PANEL_PARENTS.Controllers
{
    public class Alumno_GrupoClase_AsignaturaController : ApiController
    {

        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////


        [HttpPost]
        [Route("api/alumnogrupoclaseasignatura/agregarNota")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult AgregarNota()
        {
            var headers = Request.Headers;

            // Obtener datos del registro de las cabeceras
            if (headers.Contains("Fk_Id_Alumno_GrupoClase") &&
                headers.Contains("Fk_Id_Asignatura") &&
                headers.Contains("Nota"))
            {
                int fk_Id_Alumno_GrupoClase = Convert.ToInt32(headers.GetValues("Fk_Id_Alumno_GrupoClase").FirstOrDefault());
                int fk_Id_Asignatura = Convert.ToInt32(headers.GetValues("Fk_Id_Asignatura").FirstOrDefault());
                int nota = Convert.ToInt32(headers.GetValues("Nota").FirstOrDefault());

                // Llamar al método de la capa de datos para agregar la nota
                bool resultado = Alumno_GrupoClase_Asignatura_Metod.AgregarNota(fk_Id_Alumno_GrupoClase, fk_Id_Asignatura, nota);
                if (resultado)
                {
                    return Ok("Nota agregada con éxito.");
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

    }
}
