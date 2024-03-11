using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_ENTIDADES
{
    public class EUSUARIOS
    {
        public int IdTUsuario { get; set; }
        public string Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public EROLES oRol { get; set; }
        public bool Estado { get; set; }
        public string FechaCreacion { get; set; }
    }
}
