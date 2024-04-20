using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NPRODUCTOS
    {
        private DPRODUCTOS objProductos = new DPRODUCTOS();

        public List<EPRODUCTOS> Listar()
        {
            return objProductos.Listar();
        }

        public int CrearProducto(EPRODUCTOS obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.CodigoProducto == "")
            {
                Mensaje += "Ingrese el código del producto\n";
            }

            if (obj.NombreProducto == "")
            {
                Mensaje += "Ingrese el nombre del producto\n";
            }

            if (obj.oCategoriaProducto.IdTCategoria == 0)
            {
                Mensaje += "Seleccione la categoría del producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objProductos.CrearProducto(obj, out Mensaje);
            }
        }

        public bool EditarProducto(EPRODUCTOS obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.CodigoProducto == "")
            {
                Mensaje += "Ingrese el código del producto\n";
            }

            if (obj.NombreProducto == "")
            {
                Mensaje += "Ingrese el nombre del producto\n";
            }

            if (obj.oCategoriaProducto.IdTCategoria == 0)
            {
                Mensaje += "Seleccione la categoría del producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objProductos.Editarproducto(obj, out Mensaje);
            }
        }

        public bool EliminarProducto(EPRODUCTOS obj, out string Mensaje)
        {
            return objProductos.Eliminarproducto(obj, out Mensaje);
        }
    }
}
