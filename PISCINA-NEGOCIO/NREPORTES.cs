using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NREPORTES
    {
        private DREPORTES objdreporte = new DREPORTES();

        public List<EREPORTE_INVENTARIO> ReporteInventario(int Producto)
        {
            return objdreporte.ReporteInventario(Producto);
        }
    }
}
