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
    public class DINVENTARIOS
    {

        public List<EINVENTARIO> Listar()
        {
            List<EINVENTARIO> lInventarios = new List<EINVENTARIO>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {



                    StringBuilder consultaInventario = new StringBuilder();
                    consultaInventario.AppendLine("SELECT I.IdTInventario,I.IdTAlmacen,A.CodigoAlmacen,A.Descripcion,I.IdTTipoMov,TM.Descripcion DescTipoMovimiento,TM.Movimiento, I.IdTProducto,P.CodigoProducto,P.NombreProducto,I.Cantidad,I.RefDocumento,I.IdTUsuario,U.Usuario,I.IdTLoteProducto,LP.Lote,LP.FechaFabricacion,LP.FechaVencimiento FROM INVENTARIO I");
                    //consultaInventario.AppendLine("SELECT I.IdTInventario,A.Descripcion,TM.Descripcion,P.CodigoProducto,P.NombreProducto,I.Cantidad,I.RefDocumento,U.IdTUsuario,U.Usuario,LP.IdTLoteProducto,LP.Lote,LP.FechaFabricacion,LP.FechaVencimiento FROM INVENTARIO I");
                    consultaInventario.AppendLine("INNER JOIN ALMACENES A ON A.IdTAlmacen = I.IdTAlmacen");
                    consultaInventario.AppendLine("INNER JOIN TIPOS_MOVIMIENTOS TM ON TM.IdTTipoMov = I.IdTTipoMov");
                    consultaInventario.AppendLine("INNER JOIN PRODUCTOS P ON P.IdTProducto = I.IdTProducto");
                    consultaInventario.AppendLine("INNER JOIN CATEGORIA_PRODUCTOS CP ON P.IdTCategoria = CP.IdTCategoria");
                    consultaInventario.AppendLine("INNER JOIN USUARIOS U ON U.IdTUsuario = I.IdTUsuario");
                    consultaInventario.AppendLine("INNER JOIN LOTE_PRODUCTO LP ON LP.IdTLoteProducto = I.IdTLoteProducto");


                    SqlCommand cmd = new SqlCommand(consultaInventario.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lInventarios.Add(new EINVENTARIO()
                            {
                                IdTInventario = Convert.ToInt32(dr["IdTInventario"]),
                                oAlmacen = new EALMACENES { IdTAlmacen = Convert.ToInt32(dr["IdTAlmacen"]), CodigoAlmacen = dr["CodigoAlmacen"].ToString(), Descripcion = dr["Descripcion"].ToString() },
                                oTipoMovimiento = new ETIPOS_MOVIMIENTOS { IdTTipoMov = Convert.ToInt32(dr["IdTTipoMov"]), Movimiento = dr["Movimiento"].ToString(),  Descripcion = dr["DescTipoMovimiento"].ToString() },
                                oProductos = new EPRODUCTOS { IdTProducto = Convert.ToInt32(dr["IdTProducto"]), CodigoProducto=dr["CodigoProducto"].ToString(), NombreProducto = dr["NombreProducto"].ToString() },
                                Cantidad = Convert.ToDecimal(dr["Cantidad"]),
                                RefDocumento = dr["RefDocumento"].ToString(),
                                oUsuario = new EUSUARIOS { IdTUsuario = Convert.ToInt32(dr["IdTUsuario"]), Usuario = dr["Usuario"].ToString() },
                                oLote = new ELOTE_PRODUCTO { IdTLoteProducto = Convert.ToInt32(dr["IdTLoteProducto"]), Lote = dr["Lote"].ToString() , FechaFabricacion = dr["FechaFabricacion"].ToString() , FechaVencimiento = dr["FechaVencimiento"].ToString() },
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lInventarios = new List<EINVENTARIO>();
                }
            }

            return lInventarios;
        }



        public int CrearInventario(EINVENTARIO obj, out string Mensaje)
        {

            int idInventarioGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARINVENTARIO".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTAlmacen", obj.oAlmacen.IdTAlmacen);
                    cmd.Parameters.AddWithValue("IdTTipoMov", obj.oTipoMovimiento.IdTTipoMov);
                    cmd.Parameters.AddWithValue("IdTProducto", obj.oProductos.IdTProducto);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("RefDocumento", obj.RefDocumento);
                    cmd.Parameters.AddWithValue("IdTUsuario", obj.oUsuario.IdTUsuario);
                    cmd.Parameters.AddWithValue("IdTLoteProducto", obj.oLote.IdTLoteProducto);
                    cmd.Parameters.Add("IdInventarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idInventarioGenerado = Convert.ToInt32(cmd.Parameters["IdInventarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idInventarioGenerado = 0;
                Mensaje = ex.Message;
            }

            return idInventarioGenerado;
        }

    }
}
