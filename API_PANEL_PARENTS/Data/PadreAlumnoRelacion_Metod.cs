using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace API_PANEL_PARENTS.Data
{
    public class PadreAlumnoRelacion_Metod
    {
        public static string AgregarPadreAlumno(int idPadre, string codigoAlumno)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_PadreHijo", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Padre", idPadre);
                cmd.Parameters.AddWithValue("@Codigo", codigoAlumno);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["Mensaje"].ToString();
                    }
                    return "Error al agregar la relación padre-alumno.";
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
        ////////////////////////////////////////////////////////////////////////////
        ///ESTO ES PARA QUE A PARTIR DEL ID DE PADRE SE MUESTRE los datos de los hijos/alumno

        public static List<AlumnoGrupoMaestro> MostrarAlumnosdePadre(int idPadre)
        {
            List<AlumnoGrupoMaestro> resultado = new List<AlumnoGrupoMaestro>();

            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Mostrar_Alumnos_Padre", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Padre", idPadre);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AlumnoGrupoMaestro item = new AlumnoGrupoMaestro
                        {
                            AlumnoID = Convert.ToInt32(reader["AlumnoID"]),
                            AlumnoNombre = reader["AlumnoNombre"].ToString(),
                            GrupoClaseID = reader["GrupoClaseID"] as int?,
                            GrupoClaseAnio = reader["GrupoClaseAnio"] as int?,
                            GrupoClaseGrado = reader["GrupoClaseGrado"].ToString(),
                            MaestroNombre = reader["MaestroNombre"].ToString()
                        };
                        resultado.Add(item);
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

            return resultado;
        }


    }
}