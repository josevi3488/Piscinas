using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NINVENTARIOS
    {
        private DINVENTARIOS objInventarios = new DINVENTARIOS();

        public List<EINVENTARIO> Listar()
        {
            return objInventarios.Listar();
        }

        public int CrearInventario(EINVENTARIO obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oAlmacen.IdTAlmacen == 0)
            {
                Mensaje += "Seleccione el almacén\n";
            }

            if (obj.oTipoMovimiento.IdTTipoMov == 0)
            {
                Mensaje += "Seleccione el tipo de movimiento\n";
            }

            if (obj.oProductos.IdTProducto == 0)
            {
                Mensaje += "Seleccione el producto\n";
            }

            if (obj.Cantidad == 0)
            {
                Mensaje += "Ingrese la cantidad\n";
            }

            if (obj.oLote.IdTLoteProducto == 0)
            {
                Mensaje += "Seleccione el lote del producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objInventarios.CrearInventario(obj, out Mensaje);
            }
        }

    }
}
