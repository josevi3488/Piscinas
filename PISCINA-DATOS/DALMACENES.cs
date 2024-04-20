using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_DATOS
{
    public class DALMACENES
    {

        public List<EALMACENES> Listar()
        {
            List<EALMACENES> lAlmacenes = new List<EALMACENES>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {

                    StringBuilder consultaAlmacenes = new StringBuilder();
                    consultaAlmacenes.AppendLine("SELECT IdTAlmacen,CodigoAlmacen,Descripcion,Estado FROM ALMACENES");

                    SqlCommand cmd = new SqlCommand(consultaAlmacenes.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lAlmacenes.Add(new EALMACENES()
                            {
                                IdTAlmacen = Convert.ToInt32(dr["IdTAlmacen"]),
                                CodigoAlmacen = dr["CodigoAlmacen"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lAlmacenes = new List<EALMACENES>();
                }
            }

            return lAlmacenes;
        }



        public int CrearAlmacenes(EALMACENES obj, out string Mensaje)
        {

            int idAlmacenesGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARALMACENES".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("CodigoAlmacen", obj.CodigoAlmacen);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdAlmacenResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idAlmacenesGenerado = Convert.ToInt32(cmd.Parameters["IdAlmacenResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idAlmacenesGenerado = 0;
                Mensaje = ex.Message;
            }

            return idAlmacenesGenerado;
        }

        public bool EditarAlmacenes(EALMACENES obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARALMACENES".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTAlmacen", obj.IdTAlmacen);
                    cmd.Parameters.AddWithValue("CodigoAlmacen", obj.CodigoAlmacen);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                     cmd.Parameters.AddWithValue("Estado", obj.Estado);
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



        public bool EliminarAlmacenes(EALMACENES obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARALMACENES".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTAlmacen", obj.IdTAlmacen);
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
