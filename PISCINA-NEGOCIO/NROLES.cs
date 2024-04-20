using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISCINA_DATOS;
using PISCINA_ENTIDADES;

namespace PISCINA_NEGOCIO
{
    public class NROLES
    {
        private DROLES objdroles = new DROLES();

        public List<EROLES> Listar()
        {
            return objdroles.Listar();
        }


        public int CrearRol(EROLES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción del rol \n";
            }


            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objdroles.CrearRol(obj, out Mensaje);
            }
        }

        public bool EditarRol(EROLES obj, out string Mensaje)
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
                return objdroles.EditarRoles(obj, out Mensaje);
            }
        }

        public bool EliminarRol(EROLES obj, out string Mensaje)
        {

            return objdroles.EliminarRoles(obj, out Mensaje);

        }


    }
}
