using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class EINVENTARIO
    {
        public int IdTInventario {get;set;}
        public EALMACENES  oAlmacen { get;set;}
        public string FechaRegistro { get; set; }
        public ETIPOS_MOVIMIENTOS oTipoMovimiento { get; set; }
        //public string Movimiento { get; set; }
        public EPRODUCTOS oProductos { get; set; }
        public decimal Cantidad { get; set; }
        public string RefDocumento { get; set; }
        public EUSUARIOS oUsuario { get; set; }

        public ELOTE_PRODUCTO oLote { get; set; }

    }
}
