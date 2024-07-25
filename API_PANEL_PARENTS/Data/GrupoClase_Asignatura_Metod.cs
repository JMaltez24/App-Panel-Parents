using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class GrupoClase_Asignatura_Metod
    {
        public static bool Asignar(GrupoClase_Asignatura grupoClaseAsignatura)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_GrupoClase_Asignatura", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Fk_Id_GrupoClase", grupoClaseAsignatura.Fk_Id_GrupoClase);
                cmd.Parameters.AddWithValue("@Fk_Id_Asignatura", grupoClaseAsignatura.Fk_Id_Asignatura);

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
    }
}