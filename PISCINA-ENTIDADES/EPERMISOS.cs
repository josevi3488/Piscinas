using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class EPERMISOS
    {
        public int IdTPermiso{get; set;}
        public EROLES oRol { get; set; } 
        public string NombreMenu { get; set; }
        public string FechaCreacion { get; set;}
    }
}
