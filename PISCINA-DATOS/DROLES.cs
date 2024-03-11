using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISCINA_ENTIDADES;

namespace PISCINA_DATOS
{
    public class DROLES
    {

        public List<EROLES> Listar()
        {
            List<EROLES> lista = new List<EROLES>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdTRol,Descripcion from ROLES");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EROLES()
                            {
                                IdTRol = Convert.ToInt32(dr["IdTRol"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<EROLES>();
                }
            }
            return lista;
        }

    }
}
