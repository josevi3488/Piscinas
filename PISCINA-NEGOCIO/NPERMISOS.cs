using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public  class NPERMISOS
    {
        private DPERMISOS objPermisos = new DPERMISOS();

        public List<EPERMISOS> Listar(int idUsuario)
        {
            return objPermisos.Listar(idUsuario);
        }

        public List<EPERMISOS> Listar()
        {
            return objPermisos.Listar();
        }


        public int CrearPermisos(EPERMISOS obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oRol.IdTRol == 0)
            {
                Mensaje += "Seleccione el Rol\n";
            }

            if (obj.NombreMenu =="")
            {
                Mensaje += "Seleccione el menú\n";
            }


            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objPermisos.CrearPermisos(obj, out Mensaje);
            }
        }

        public bool EditarPermisos(EPERMISOS obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objPermisos.EditarPermisos(obj, out Mensaje);
            }
        }

        public bool EliminarPermisos(EPERMISOS obj, out string Mensaje)
        {

            return objPermisos.EliminarPermisos(obj, out Mensaje);

        }


    }
}
