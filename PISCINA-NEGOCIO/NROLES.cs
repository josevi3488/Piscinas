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
    }
}
