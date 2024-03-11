using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class ELOTE_PRODUCTO
    {
        public int IdTLoteProducto { get; set; }
        public EPRODUCTOS oProductos { get; set; }
        public string Lote { get; set; }
        public string FechaFabricacion { get;set; }
        public string FechaVencimiento { get; set; }
        public string FechaCreacion { get; set; }
    }
}
