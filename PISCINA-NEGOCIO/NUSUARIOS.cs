using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISCINA_DATOS;
using PISCINA_ENTIDADES;

namespace PISCINA_NEGOCIO
{
    public class NUSUARIOS
    {
        private DUSUARIOS objUsuarios = new DUSUARIOS();

        public List<EUSUARIOS> Listar()
        {
            return objUsuarios.Listar();
        }

        public int CrearUsuario(EUSUARIOS obj,out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Usuario == "")
            {
                Mensaje += "Ingrese el usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Ingrese el nombre completo\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Ingrese la clave\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objUsuarios.CrearUsuario(obj,out Mensaje);
            }
        }

        public bool EditarUsuario(EUSUARIOS obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Usuario == "")
            {
                Mensaje += "Ingrese el usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Ingrese el nombre completo\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Ingrese la clave\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objUsuarios.EditarUsuario(obj, out Mensaje);
            }
        }

        public bool EliminarUsuario(EUSUARIOS obj, out string Mensaje)
        {
            return objUsuarios.EliminarUsuario(obj, out Mensaje); 
        }
    }
}
