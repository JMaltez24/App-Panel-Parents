using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Models
{
    public class Anuncio
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
        public int Fk_Id_Maestro { get; set; }
    }

}