using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class EREPORTE_INVENTARIO
    {
        public int IdTProducto {  get; set; }   
        public string CodigoProducto { get; set; }
        public string Produto { get; set; }
        public string Almacen { get; set;}
        public decimal StockMinimo { get; set; }
        public decimal Stock { get; set; }
        public string Accion {  get; set; }
    }
}
