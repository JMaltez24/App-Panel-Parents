using API_PANEL_PARENTS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_PANEL_PARENTS.Data
{
    public class UsuarioM_Metod
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////METODO PARA REGISTARA MAESTROS
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool Registrar(UsuarioM usuarioM)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Agregar_Maestro", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Nombre", usuarioM.Nombre);
                cmd.Parameters.AddWithValue("@Usuario", usuarioM.Usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", usuarioM.Contrasenia);
                cmd.Parameters.AddWithValue("@rol", "Maestro");

                // Capturadora de errores
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException sqlEx)
                {
                    //  registrar el error si es necesario
                    // Log(sqlEx.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    //// registrar el error si es necesario
                    // Log(ex.Message);
                    return false;
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////METODO PARA AUTENTICAR MAESTROS
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool Autenticar(UsuarioM credenciales)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Autenticar_Usuario", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros
                cmd.Parameters.AddWithValue("@Usuario", credenciales.Usuario);
                cmd.Parameters.AddWithValue("@Contrasenia", credenciales.Contrasenia);

                try
                {
                    connection.Open();
                    var resultado = cmd.ExecuteScalar();
                    return (int)resultado == 1;
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
        ///////////////////////////LISTAR MAESTRO//////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////

        public static List<UsuarioM> Obtener_Maestros()
        {
            List<UsuarioM> maestros = new List<UsuarioM>();

            using (SqlConnection connection = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand cmd = new SqlCommand("Sp_Obtener_Todos_Maestros", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UsuarioM maestro = new UsuarioM
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Usuario = reader["Usuario"].ToString(),
                            Contrasenia = reader["Contraseniaa"].ToString()
                        };
                        maestros.Add(maestro);
                    }
                    reader.Close();
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

            return maestros;
        }


    }
}