using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System.Security.Claims;
using System.Reflection;

namespace PISCINA_DATOS
{
    public class DUSUARIOS
    {
        public List<EUSUARIOS> Listar()
        {
            List<EUSUARIOS> lusuarios= new List<EUSUARIOS>();
            using(SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try {
                    
 

                    StringBuilder consultaUsuario = new StringBuilder();
                    consultaUsuario.AppendLine("select u.IdTUsuario,u.Usuario,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IdTRol,r.Descripcion from usuarios u");
                    consultaUsuario.AppendLine("inner join roles r on r.IdTRol = u.IdTRol");


                    SqlCommand cmd = new SqlCommand(consultaUsuario.ToString(),oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) { 
                        
                        while(dr.Read()) {
                            lusuarios.Add(new EUSUARIOS()
                            {
                                IdTUsuario = Convert.ToInt32(dr["IdTUsuario"]),
                                Usuario = dr["Usuario"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new EROLES() { IdTRol = Convert.ToInt32(dr["IdTRol"]), Descripcion = dr["Descripcion"].ToString() }
                            });
                        }
                    }
                
                }
                catch(Exception ex) { 
                    lusuarios = new List<EUSUARIOS>();
                } 
            }
            
            return lusuarios;
        }



        public int CrearUsuario(EUSUARIOS obj, out string Mensaje) {

            int idUsuarioGenerado = 0;
            Mensaje = string.Empty;

            try {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena)) {
                    
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("Usuario", obj.Usuario);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdTRol", obj.oRol.IdTRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idUsuarioGenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                idUsuarioGenerado = 0;
                Mensaje = ex.Message;
            }

            return idUsuarioGenerado;
        }

        public bool EditarUsuario(EUSUARIOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTUsuario", obj.IdTUsuario);
                    cmd.Parameters.AddWithValue("Usuario", obj.Usuario);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IdTRol", obj.oRol.IdTRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }



        public bool EliminarUsuario(EUSUARIOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTUsuario", obj.IdTUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }


    }
}
