using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class Padre_Metod
    {
        public static bool Registrar(Padre padre)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Padre", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Nombre", padre.Nombre);
                cmd.Parameters.AddWithValue("@Usuario", padre.Usuario);
                cmd.Parameters.AddWithValue("@Contraseniaa", padre.Contraseniaa);

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

        /// Este metodo servira para la relacion entre Padre -Alumno ///
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

        /// FIN ///

        /////////////////////////////////////////////////////////////////////////////////////
        /////////////Este metodo es para autenticar a los padres desde la apk////////////////
        /////////////////////////////////////////////////////////////////////////////////////  

        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////AQUI ES TA LA AUTENTICACION//////////////////////////////////
        /////////////////////////////////DE APLICACION MOBIL//////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        public static string Autenticar(string usuario, string contrasenia)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Autenticar_Padre", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", contrasenia);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["ID"].ToString();
                    }
                    return "Usuario o contraseña incorrectos.";
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

        /////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////FIN////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////










    }
}