using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class Anuncio_Metod
    {
        public static int Registrar(Anuncio anuncio)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Anuncio", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Asunto", anuncio.Asunto);
                cmd.Parameters.AddWithValue("@Fecha", anuncio.Fecha);
                cmd.Parameters.AddWithValue("@Contenido", anuncio.Contenido);
                cmd.Parameters.AddWithValue("@Fk_Id_Maestro", anuncio.Fk_Id_Maestro);

                try
                {
                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (SqlException sqlEx)
                {
                    // Registrar el error si es necesario
                    // Log(sqlEx.Message);
                    return 0;
                }
                catch (Exception ex)
                {
                    // Registrar el error si es necesario
                    // Log(ex.Message);
                    return 0;
                }
            }
        }
    }
}