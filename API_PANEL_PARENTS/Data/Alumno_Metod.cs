using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class Alumno_Metod
    {
        public static bool Registrar(Alumno alumno)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Alumno", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                cmd.Parameters.AddWithValue("@Codigo", alumno.Codigo);
                cmd.Parameters.AddWithValue("@FechaNacimiento", alumno.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Genero", alumno.Genero);

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

        /////////////////////////////////////////////////////////////////////////////////////
        ///

        public static List<Alumno> Listar()
        {
            List<Alumno> alumnos = new List<Alumno>();

            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Listar_Alumnos", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Alumno alumno = new Alumno
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Codigo = reader["Codigo"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            Genero = reader["Genero"].ToString()
                        };
                        alumnos.Add(alumno);
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Registrar el error si es necesario
                    // Log(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    // Registrar el error si es necesario
                    // Log(ex.Message);
                }
            }

            return alumnos;
        }

    }
}