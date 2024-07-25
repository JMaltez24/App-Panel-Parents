using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Models
{
    public class GrupoClase
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Grado { get; set; }
        public int Fk_Id_Maestro { get; set; }
    }
}