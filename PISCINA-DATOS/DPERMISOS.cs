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
                    consultaPermisos.AppendLine("SELECT P.IdTRol,R.Descripcion,P.NombreMenu FROM PERMISOS P");
                    consultaPermisos.AppendLine("INNER JOIN ROLES R ON R.IdTRol = P.IdTRol");
                    consultaPermisos.AppendLine("INNER JOIN USUARIOS U ON U.IdTRol = R.IdTRol");


                    SqlCommand cmd = new SqlCommand(consultaPermisos.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lPermisos.Add(new EPERMISOS()
                            {
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
    }
}
