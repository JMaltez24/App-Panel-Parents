using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
    }
    public class AlumnoGrupoMaestro
    {
        public int AlumnoID { get; set; }
        public string AlumnoNombre { get; set; }
        public int? GrupoClaseID { get; set; }
        public int? GrupoClaseAnio { get; set; }
        public string GrupoClaseGrado { get; set; }
        public string MaestroNombre { get; set; }
    }


}