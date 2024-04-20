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
    public class DLOTES
    {


        public List<ELOTE_PRODUCTO> Listar()
        {
            List<ELOTE_PRODUCTO> llotes = new List<ELOTE_PRODUCTO>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {
                    StringBuilder consultalote = new StringBuilder();
                    consultalote.AppendLine("SELECT LP.IdTLoteProducto,LP.Lote,P.IdTProducto,P.CodigoProducto,P.NombreProducto, LP.FechaFabricacion,LP.FechaVencimiento FROM LOTE_PRODUCTO LP");
                    consultalote.AppendLine("INNER JOIN PRODUCTOS P ON P.IdTProducto = LP.IdTProducto");

                    SqlCommand cmd = new SqlCommand(consultalote.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            llotes.Add(new ELOTE_PRODUCTO()
                            {
                                IdTLoteProducto = Convert.ToInt32(dr["IdTloteProducto"]),
                                oProductos = new EPRODUCTOS() { IdTProducto = Convert.ToInt32(dr["IdTProducto"]), CodigoProducto = dr["CodigoProducto"].ToString(), NombreProducto = dr["NombreProducto"].ToString() }, 
                                Lote = dr["Lote"].ToString(),
                                FechaFabricacion = dr["FechaFabricacion"].ToString(),
                                FechaVencimiento = dr["FechaVencimiento"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    llotes = new List<ELOTE_PRODUCTO>();
                }
            }

            return llotes;
        }


        public List<ELOTE_PRODUCTO> ListarxProducto(int codProducto)
        {
            List<ELOTE_PRODUCTO> llotes = new List<ELOTE_PRODUCTO>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {
                    StringBuilder consultalote = new StringBuilder();
                    consultalote.AppendLine("SELECT LP.IdTLoteProducto,LP.Lote,P.IdTProducto,P.CodigoProducto,P.NombreProducto, LP.FechaFabricacion,LP.FechaVencimiento FROM LOTE_PRODUCTO LP");
                    consultalote.AppendLine("INNER JOIN PRODUCTOS P ON P.IdTProducto = LP.IdTProducto");
                    consultalote.AppendLine("WHERE P.IdTProducto ="+ codProducto);

                    SqlCommand cmd = new SqlCommand(consultalote.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            llotes.Add(new ELOTE_PRODUCTO()
                            {
                                IdTLoteProducto = Convert.ToInt32(dr["IdTloteProducto"]),
                                oProductos = new EPRODUCTOS() { IdTProducto = Convert.ToInt32(dr["IdTProducto"]), CodigoProducto = dr["CodigoProducto"].ToString(), NombreProducto = dr["NombreProducto"].ToString() },
                                Lote = dr["Lote"].ToString(),
                                FechaFabricacion = dr["FechaFabricacion"].ToString(),
                                FechaVencimiento = dr["FechaVencimiento"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    llotes = new List<ELOTE_PRODUCTO>();
                }
            }

            return llotes;
        }



        public int Crearlote(ELOTE_PRODUCTO obj, out string Mensaje)
        {

            int idloteGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARLOTES".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTProducto", obj.oProductos.IdTProducto);
                    cmd.Parameters.AddWithValue("Lote", obj.Lote);
                    cmd.Parameters.AddWithValue("FechaFabricacion", obj.FechaFabricacion);
                    cmd.Parameters.AddWithValue("FechaVencimiento", obj.FechaVencimiento);
                    cmd.Parameters.Add("IdloteResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idloteGenerado = Convert.ToInt32(cmd.Parameters["IdloteResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idloteGenerado = 0;
                Mensaje = ex.Message;
            }

            return idloteGenerado;
        }

        public bool Editarlote(ELOTE_PRODUCTO obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARLOTES".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTloteProducto", obj.IdTLoteProducto);
                    cmd.Parameters.AddWithValue("IdTProducto", obj.oProductos.IdTProducto);
                    cmd.Parameters.AddWithValue("Lote", obj.Lote);
                    cmd.Parameters.AddWithValue("FechaFabricacion", obj.FechaFabricacion);
                    cmd.Parameters.AddWithValue("FechaVencimiento", obj.FechaVencimiento);
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



        public bool Eliminarlote(ELOTE_PRODUCTO obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARLOTES".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTloteProducto", obj.IdTLoteProducto);
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
