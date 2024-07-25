using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Models
{
    public class Alumno_GrupoClase_Asignatura
    {
        public int Id { get; set; }
        public int Fk_Id_Alumno_GrupoClase { get; set; }
        public int Fk_Id_Asignatura { get; set; }
        public int Nota { get; set; }
    }
}