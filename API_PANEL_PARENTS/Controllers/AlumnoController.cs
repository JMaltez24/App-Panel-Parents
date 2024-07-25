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
    public class AlumnoController : ApiController
    {
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////

        [HttpPost]
        [Route("api/alumno/registrar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult RegistrarAlumno()
        {
            var headers = Request.Headers;

            // Obtener datos del alumno de las cabeceras
            if (headers.Contains("Nombre") &&
                headers.Contains("Codigo") &&
                headers.Contains("FechaNacimiento") &&
                headers.Contains("Genero"))
            {
                string nombre = headers.GetValues("Nombre").FirstOrDefault();
                string codigo = headers.GetValues("Codigo").FirstOrDefault();
                DateTime fechaNacimiento = Convert.ToDateTime(headers.GetValues("FechaNacimiento").FirstOrDefault());
                string genero = headers.GetValues("Genero").FirstOrDefault();

                // Crear objeto Alumno con los datos recibidos
                Alumno alumno = new Alumno
                {
                    Nombre = nombre,
                    Codigo = codigo,
                    FechaNacimiento = fechaNacimiento,
                    Genero = genero
                };

                // Llamar al método de la capa de datos para registrar al alumno
                bool resultado = Alumno_Metod.Registrar(alumno);
                if (resultado)
                {
                    return Ok("Alumno registrado con éxito.");
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
        //////////////////////////////////////////////////////////////////////////////////////
        ///
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////

        [HttpGet]
        [Route("api/alumno/listar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult ListarAlumnos()
        {
            List<Alumno> alumnos = Alumno_Metod.Listar();
            if (alumnos != null && alumnos.Count > 0)
            {
                return Ok(alumnos);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
