using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class ESTOCK
    {
       public int IdTAlmacen { get; set; }
       public EPRODUCTOS oProductos { get; set; }
       public string NombreProducto { get; set; }
       public decimal StokMinimo { get; set; }
       public decimal StokFinal { get; set; }

    }
}
