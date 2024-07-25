using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class Asignatura_Metod
    {
        public static bool Registrar(Asignatura asignatura)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Asignatura", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Nombre", asignatura.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", asignatura.Descripcion);

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

        //////////////////////////////////////////////////////////////////////////////////
        ///

        public static List<Asignatura> Listar()
        {
            List<Asignatura> asignaturas = new List<Asignatura>();

            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Listar_Asignaturas", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Asignatura asignatura = new Asignatura
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString()
                        };
                        asignaturas.Add(asignatura);
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

            return asignaturas;
        }
    }
}