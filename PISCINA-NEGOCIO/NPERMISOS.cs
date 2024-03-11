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
    }
}
