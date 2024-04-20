using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NCATEGORIAS
    {
        private DCATEGORIAS objCategorias = new DCATEGORIAS();

        public List<ECATEGORIA_PRODUCTOS> Listar()
        {
            return objCategorias.Listar();
        }

        public int CrearCategoria(ECATEGORIA_PRODUCTOS obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción de la Categoria\n";
            }

            
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCategorias.CrearCategoria(obj, out Mensaje);
            }
        }

        public bool EditarCategoria(ECATEGORIA_PRODUCTOS obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Ingrese la descripción de la categoria\n";
            }

  
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCategorias.EditarCategoria(obj, out Mensaje);
            }
        }

        public bool EliminarCategoria(ECATEGORIA_PRODUCTOS obj, out string Mensaje)
        {
            return objCategorias.EliminarCategoria(obj, out Mensaje);
        }
    }
}
