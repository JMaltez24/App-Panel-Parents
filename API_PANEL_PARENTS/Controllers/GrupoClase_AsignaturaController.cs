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
    public class GrupoClase_AsignaturaController : ApiController
    {

        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////
        [HttpPost]
        [Route("api/grupoclaseasignatura/asignarclase")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult RegistrarGrupoClaseAsignatura()
        {
            var headers = Request.Headers;

            // Obtener datos del grupo-clase-asignatura de las cabeceras
            if (headers.Contains("Fk_Id_GrupoClase") && headers.Contains("Fk_Id_Asignatura"))
            {
                int fk_Id_GrupoClase = Convert.ToInt32(headers.GetValues("Fk_Id_GrupoClase").FirstOrDefault());
                int fk_Id_Asignatura = Convert.ToInt32(headers.GetValues("Fk_Id_Asignatura").FirstOrDefault());

                // Crear objeto GrupoClaseAsignatura con los datos recibidos
                GrupoClase_Asignatura grupoClaseAsignatura = new GrupoClase_Asignatura
                {
                    Fk_Id_GrupoClase = fk_Id_GrupoClase,
                    Fk_Id_Asignatura = fk_Id_Asignatura
                };

                // Llamar al método de la capa de datos para registrar el grupo-clase-asignatura
                bool resultado = GrupoClase_Asignatura_Metod.Asignar(grupoClaseAsignatura);
                if (resultado)
                {
                    return Ok("Clase Asignada exitosamente");
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
