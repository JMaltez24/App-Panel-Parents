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
    public class AsignaturaController : ApiController
    {
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////
        [HttpPost]
        [Route("api/asignatura/registrar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult RegistrarAsignatura()
        {
            var headers = Request.Headers;

            // Obtener datos de la asignatura de las cabeceras
            if (headers.Contains("Nombre") &&
                headers.Contains("Descripcion"))
            {
                string nombre = headers.GetValues("Nombre").FirstOrDefault();
                string descripcion = headers.GetValues("Descripcion").FirstOrDefault();

                // Crear objeto Asignatura con los datos recibidos
                Asignatura asignatura = new Asignatura
                {
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                // Llamar al método de la capa de datos para registrar la asignatura
                bool resultado = Asignatura_Metod.Registrar(asignatura);
                if (resultado)
                {
                    return Ok("Asignatura registrada con éxito.");
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


        ///////////////////////////////////////////////////////////////////////////////
        /// 
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////

        [HttpGet]
        [Route("api/asignatura/listar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult ListarAsignaturas()
        {
            List<Asignatura> asignaturas = Asignatura_Metod.Listar();
            if (asignaturas != null && asignaturas.Count > 0)
            {
                return Ok(asignaturas);
            }
            else
            {
                return NotFound();
            }
        }

    }

}
