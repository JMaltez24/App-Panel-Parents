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
    public class GrupoClaseController : ApiController
    {
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////

        [HttpPost]
        [Route("api/grupoclase/registrar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult RegistrarGrupoClase()
        {
            var headers = Request.Headers;

            // Obtener datos del grupo de clase de las cabeceras
            if (headers.Contains("Anio") &&
                headers.Contains("Grado") &&
                headers.Contains("Fk_Id_Maestro"))
            {
                int anio = Convert.ToInt32(headers.GetValues("Anio").FirstOrDefault());
                string grado = headers.GetValues("Grado").FirstOrDefault();
                int fk_Id_Maestro = Convert.ToInt32(headers.GetValues("Fk_Id_Maestro").FirstOrDefault());

                // Crear objeto GrupoClase con los datos recibidos
                GrupoClase grupoClase = new GrupoClase
                {
                    Anio = anio,
                    Grado = grado,
                    Fk_Id_Maestro = fk_Id_Maestro
                };

                // Llamar al método de la capa de datos para registrar el grupo de clase
                bool resultado = GrupoClase_Metod.Registrar(grupoClase);
                if (resultado)
                {
                    return Ok("Grupo de clase registrado con éxito.");
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
