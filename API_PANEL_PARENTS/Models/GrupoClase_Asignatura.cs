using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Models
{
    public class GrupoClase_Asignatura
    {
        public int id { get; set; }
        public int Fk_Id_GrupoClase {  get; set; }
        public int Fk_Id_Asignatura {  get; set; }
    }
}