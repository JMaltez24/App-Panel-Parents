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
    public class AnuncioController : ApiController
    {
        
        ////////////////////////////////WINDONWS FORM////////////////////////////////////////////
        ///
        [HttpPost]
        [Route("api/anuncio/registrar")]
        [AppAuthorize(AppType = "desktop")]
        public IHttpActionResult RegistrarAnuncio()
        {
            var headers = Request.Headers;

            // Obtener datos del anuncio de las cabeceras
            if (headers.Contains("Asunto") &&
                headers.Contains("Fecha") &&
                headers.Contains("Contenido") &&
                headers.Contains("Fk_Id_Maestro"))
            {
                string asunto = headers.GetValues("Asunto").FirstOrDefault();
                DateTime fecha = Convert.ToDateTime(headers.GetValues("Fecha").FirstOrDefault());
                string contenido = headers.GetValues("Contenido").FirstOrDefault();
                int fk_Id_Maestro = Convert.ToInt32(headers.GetValues("Fk_Id_Maestro").FirstOrDefault());

                // Crear objeto Anuncio con los datos recibidos
                Anuncio anuncio = new Anuncio
                {
                    Asunto = asunto,
                    Fecha = fecha,
                    Contenido = contenido,
                    Fk_Id_Maestro = fk_Id_Maestro
                };

                // Llamar al método de la capa de datos para registrar el anuncio
                int anuncioId = Anuncio_Metod.Registrar(anuncio);
                if (anuncioId > 0)
                {
                    return Ok("Anuncio registrado con éxito. ID: " + anuncioId);
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
