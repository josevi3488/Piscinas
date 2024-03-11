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
    public class DTIPOMOVIMIENTOS
    {


        public List<ETIPOS_MOVIMIENTOS> Listar()
        {
            List<ETIPOS_MOVIMIENTOS> ltipoMovimientos = new List<ETIPOS_MOVIMIENTOS>();
            using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
            {
                try
                {



                    StringBuilder consultatipoMovimiento = new StringBuilder();
                    consultatipoMovimiento.AppendLine("SELECT IdTTipoMov,Descripcion,Movimiento FROM TIPOS_MOVIMIENTOS");



                    SqlCommand cmd = new SqlCommand(consultatipoMovimiento.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            ltipoMovimientos.Add(new ETIPOS_MOVIMIENTOS()
                            {
                                IdTTipoMov = Convert.ToInt32(dr["IdTTipoMov"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Movimiento = dr["Movimiento"].ToString(),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltipoMovimientos = new List<ETIPOS_MOVIMIENTOS>();
                }
            }

            return ltipoMovimientos;
        }



        public int CreartipoMovimiento(ETIPOS_MOVIMIENTOS obj, out string Mensaje)
        {

            int idtipoMovimientoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARTIPOSMOVIMIENTOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Movimiento", obj.Movimiento);
                    cmd.Parameters.Add("IdTMovimientoResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    idtipoMovimientoGenerado = Convert.ToInt32(cmd.Parameters["IdTMovimientoResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idtipoMovimientoGenerado = 0;
                Mensaje = ex.Message;
            }

            return idtipoMovimientoGenerado;
        }

        public bool EditartipoMovimiento(ETIPOS_MOVIMIENTOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARTIPOSMOVIMIENTOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTTipoMov", obj.IdTTipoMov);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Movimiento", obj.Movimiento);
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



        public bool EliminartipoMovimiento(ETIPOS_MOVIMIENTOS obj, out string Mensaje)
        {

            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(DCONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARTIPOSMOVIMIENTOS".ToString(), oConexion);
                    cmd.Parameters.AddWithValue("IdTTipoMov", obj.IdTTipoMov);
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
