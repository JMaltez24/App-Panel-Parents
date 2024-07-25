using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class Alumno_GrupoClase_Metod
    {
        public static string AgregarAlumnoGrupoClase(int fkIdAlumno, int fkIdGrupoClase)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Alumno_GrupoClase", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fk_Id_Alumno", fkIdAlumno);
                cmd.Parameters.AddWithValue("@Fk_Id_GrupoClase", fkIdGrupoClase);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["Mensaje"].ToString();
                    }
                    return "Error al agregar la relación alumno-grupo de clase.";
                }
                catch (SqlException sqlEx)
                {
                    // Registrar el error si es necesario
                    // Log(sqlEx.Message);
                    return "Error de base de datos.";
                }
                catch (Exception ex)
                {
                    // Registrar el error si es necesario
                    // Log(ex.Message);
                    return "Error del servidor.";
                }
            }
        }

    }
}