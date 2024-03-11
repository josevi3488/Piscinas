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
    public class DPRODUCTOS
    {

        public List<EPRODUCTOS> Listar()
        {
            List<EPRODUCTOS> lproductos = new List<EPRODUCTOS>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {



                    StringBuilder consultaproducto = new StringBuilder();
                    consultaproducto.AppendLine("SELECT P.IdTProducto,P.CodigoProducto,P.NombreProducto,CAT.IdTCategoria,CAT.Descripcion,P.StockMinimo,P.UltimoPrecioCompra,P.CodigoBarra,P.Estado FROM PRODUCTOS P");
                    consultaproducto.AppendLine("INNER JOIN CATEGORIA_PRODUCTOS CAT ON P.IdTCategoria = CAT.IdTCategoria");


                    SqlCommand cmd = new SqlCommand(consultaproducto.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lproductos.Add(new EPRODUCTOS()
                            {
                                IdTProducto = Convert.ToInt32(dr["IdTproducto"]),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                oCategoriaProducto = new ECATEGORIA_PRODUCTOS() { IdTCategoria = Convert.ToInt32(dr["IdTCategoria"]), Descripcion = dr["Descripcion"].ToString() },
                                StockMinimo = Convert.ToDecimal(dr["StockMinimo"]),
                                UltimoPrecioCompra = Convert.ToDecimal(dr["UltimoPrecioCompra"]),
                                CodigoBarra = dr["CodigoBarra"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lproductos = new List<EPRODUCTOS>();
                }
            }

            return lproductos;
        }



        public int CrearProducto(EPRODUCTOS obj, out string Mensaje)
        {

            int idproductoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRODUCTO".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("CodigoProducto", obj.CodigoProducto);
                    cmd.Parameters.AddWithValue("NombreProducto", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("IdTCategoria", obj.oCategoriaProducto.IdTCategoria);
                    cmd.Parameters.AddWithValue("StockMinimo", obj.StockMinimo);
                    cmd.Parameters.AddWithValue("UltimoPrecioCompra", obj.UltimoPrecioCompra);
                    cmd.Parameters.AddWithValue("CodigoBarra", obj.CodigoBarra);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("IdProductoResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idproductoGenerado = Convert.ToInt32(cmd.Parameters["IdProductoResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idproductoGenerado = 0;
                Mensaje = ex.Message;
            }

            return idproductoGenerado;
        }

        public bool Editarproducto(EPRODUCTOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTproducto", obj.IdTProducto);
                    cmd.Parameters.AddWithValue("CodigoProducto", obj.CodigoProducto);
                    cmd.Parameters.AddWithValue("NombreProducto", obj.NombreProducto);
                    cmd.Parameters.AddWithValue("IdTCategoria", obj.oCategoriaProducto.IdTCategoria);
                    cmd.Parameters.AddWithValue("StockMinimo", obj.StockMinimo);
                    cmd.Parameters.AddWithValue("UltimoPrecioCompra", obj.UltimoPrecioCompra);
                    cmd.Parameters.AddWithValue("CodigoBarra", obj.CodigoBarra);
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



        public bool Eliminarproducto(EPRODUCTOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPRODUCTOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTProducto", obj.IdTProducto);
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
