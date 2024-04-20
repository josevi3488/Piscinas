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
    public class DREPORTES
    {

        public List<EREPORTE_INVENTARIO> ReporteInventario(int Producto)
        {
            List<EREPORTE_INVENTARIO> lista = new List<EREPORTE_INVENTARIO>();
            
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {

                    StringBuilder consultalote = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_REPORTEINVENTARIO", oConexion);
                    cmd.Parameters.AddWithValue("Producto", Producto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new EREPORTE_INVENTARIO()
                            {
                                IdTProducto = Convert.ToInt32(dr["IdTProducto"].ToString()),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                Produto = dr["Producto"].ToString(),
                                Almacen = dr["Almacen"].ToString(),
                                StockMinimo = Convert.ToDecimal(dr["StockMinimo"].ToString()),
                                Stock = Convert.ToDecimal(dr["Stock"].ToString()),
                                Accion = dr["Accion"].ToString(),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<EREPORTE_INVENTARIO>();
                }
            }

            return lista;
        }


    }
}
