using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class EPRODUCTOS
    {
        public int IdTProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public ECATEGORIA_PRODUCTOS oCategoriaProducto { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal UltimoPrecioCompra { get; set; }
        public string CodigoBarra { get; set; }
        public bool Estado { get; set; }
        public string FechaCreacion { get; set; }

    }
}
