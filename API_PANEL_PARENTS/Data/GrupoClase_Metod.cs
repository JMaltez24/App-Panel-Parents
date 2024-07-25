using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class GrupoClase_Metod
    {
        public static bool Registrar(GrupoClase grupoClase)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_GrupoClase", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Anio", grupoClase.Anio);
                cmd.Parameters.AddWithValue("@Grado", grupoClase.Grado);
                cmd.Parameters.AddWithValue("@Fk_Id_Maestro", grupoClase.Fk_Id_Maestro);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException sqlEx)
                {
                    // Registrar el error si es necesario
                    Console.WriteLine("SQL Error: " + sqlEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    // Registrar el error si es necesario
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}