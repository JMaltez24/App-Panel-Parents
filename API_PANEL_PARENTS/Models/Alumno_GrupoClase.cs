using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Models
{
    public class Alumno_GrupoClase
    {
        public int Id { get; set; }
        public int Fk_Id_Alumno { get; set; }
        public int Fk_Id_GrupoClase { get; set; }
    }
}