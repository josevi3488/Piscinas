using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PISCINA_DATOS
{
    public class DPERMISOS
    {

        public List<EPERMISOS> Listar(int idUsuario)
        {
            List<EPERMISOS> lPermisos = new List<EPERMISOS>();

            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {

                    StringBuilder consultaPermisos  = new StringBuilder();
                    consultaPermisos.AppendLine("SELECT P.IdTRol,P.NombreMenu FROM PERMISOS P");
                    consultaPermisos.AppendLine("INNER JOIN ROLES R ON R.IdTRol = P.IdTRol");
                    consultaPermisos.AppendLine("INNER JOIN USUARIOS U ON U.IdTRol = R.IdTRol");
                    consultaPermisos.AppendLine("WHERE U.IDTUSUARIO = @idUsuario");

                    SqlCommand cmd = new SqlCommand(consultaPermisos.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lPermisos.Add(new EPERMISOS()
                            {
                                oRol = new EROLES() { IdTRol= Convert.ToInt32(dr["IdTRol"]) } ,
                                NombreMenu = dr["NombreMenu"].ToString(),
                               
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lPermisos = new List<EPERMISOS>();
                }
            }
            return lPermisos;
        }



        public List<EPERMISOS> Listar()
        {
            List<EPERMISOS> lPermisos = new List<EPERMISOS>();

            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {

                    StringBuilder consultaPermisos = new StringBuilder();
                    
                    consultaPermisos.AppendLine("SELECT P.IdTPermiso, P.IdTRol,R.Descripcion,P.NombreMenu FROM PERMISOS P");
                    consultaPermisos.AppendLine("INNER JOIN ROLES R ON R.IdTRol = P.IdTRol");

                    SqlCommand cmd = new SqlCommand(consultaPermisos.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lPermisos.Add(new EPERMISOS()
                            {
                                IdTPermiso= Convert.ToInt32(dr["IdTPermiso"]),
                                oRol = new EROLES() { IdTRol = Convert.ToInt32(dr["IdTRol"]) , Descripcion = dr["Descripcion"].ToString() },
                                NombreMenu = dr["NombreMenu"].ToString(),

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lPermisos = new List<EPERMISOS>();
                }
            }
            return lPermisos;
        }


        public int CrearPermisos(EPERMISOS obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPERMISOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTRol", obj.oRol.IdTRol);
                    cmd.Parameters.AddWithValue("NombreMenu", obj.NombreMenu);
                    cmd.Parameters.Add("IdResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["IdResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Mensaje = ex.Message;
            }

            return idGenerado;
        }

        public bool EditarPermisos(EPERMISOS obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARPERMISOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTPermiso", obj.IdTPermiso);
                    cmd.Parameters.AddWithValue("IdTRol", obj.oRol.IdTRol);
                    cmd.Parameters.AddWithValue("NombreMenu", obj.NombreMenu);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

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



        public bool EliminarPermisos(EPERMISOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPERMISOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTPermiso", obj.IdTPermiso);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

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
