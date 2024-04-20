using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NTIPOMOVIMIENTOS
    {
        private DTIPOMOVIMIENTOS objTipoMovimientos = new DTIPOMOVIMIENTOS();

        public List<ETIPOS_MOVIMIENTOS> Listar()
        {
            return objTipoMovimientos.Listar();
        }

        public int CrearTipoMovimiento(ETIPOS_MOVIMIENTOS obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción\n";
            }

            if (obj.Movimiento == "")
            {
                Mensaje += "Ingrese el tipo de movimiento\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objTipoMovimientos.CreartipoMovimiento(obj, out Mensaje);
            }
        }

        public bool EditarTipoMovimiento(ETIPOS_MOVIMIENTOS obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción\n";
            }

            if (obj.Movimiento == "")
            {
                Mensaje += "Ingrese el tipo de movimiento\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objTipoMovimientos.EditartipoMovimiento(obj, out Mensaje);
            }
        }

        public bool EliminarTipoMovimiento(ETIPOS_MOVIMIENTOS obj, out string Mensaje)
        {
            return objTipoMovimientos.EliminartipoMovimiento(obj, out Mensaje);
        }
    }
}
