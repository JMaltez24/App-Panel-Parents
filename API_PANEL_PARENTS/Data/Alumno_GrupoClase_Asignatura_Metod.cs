using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class Alumno_GrupoClase_Asignatura_Metod
    {
        public static bool AgregarNota(int fk_Id_Alumno_GrupoClase, int fk_Id_Asignatura, int nota)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Nota", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fk_Id_Alumno_GrupoClase", fk_Id_Alumno_GrupoClase);
                cmd.Parameters.AddWithValue("@Fk_Id_Asignatura", fk_Id_Asignatura);
                cmd.Parameters.AddWithValue("@Nota", nota);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException sqlEx)
                {
                    // Registrar el error si es necesario
                    // Log(sqlEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    // Registrar el error si es necesario
                    // Log(ex.Message);
                    return false;
                }
            }
        }


        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
    }
}