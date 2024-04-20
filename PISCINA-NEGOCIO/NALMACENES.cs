using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NALMACENES
    {
        private DALMACENES objAlmacenes = new DALMACENES();

        public List<EALMACENES> Listar()
        {
            return objAlmacenes.Listar();
        }

        public int CrearAlmacen(EALMACENES obj,out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción del almacén \n";
            }


            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objAlmacenes.CrearAlmacenes(obj,out Mensaje);
            }
        }

        public bool EditarAlmacen(EALMACENES obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción del almacén \n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objAlmacenes.EditarAlmacenes(obj, out Mensaje);
            }
        }

        public bool EliminarAlmacen(EALMACENES obj, out string Mensaje)
        {
 
            return objAlmacenes.EliminarAlmacenes(obj, out Mensaje);
 
        }
    }
}
