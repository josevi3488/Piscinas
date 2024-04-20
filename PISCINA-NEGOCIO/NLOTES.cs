using PISCINA_DATOS;
using PISCINA_ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PISCINA_NEGOCIO
{
    public class NLOTES
    {
        private DLOTES objLotes = new DLOTES();

        public List<ELOTE_PRODUCTO> Listar()
        {
            return objLotes.Listar();
        }

        public List<ELOTE_PRODUCTO> ListarxProducto(int codProducto)
        {
            return objLotes.ListarxProducto(codProducto);
        }

        public int CrearLote(ELOTE_PRODUCTO obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oProductos.IdTProducto == 0)
            {
                Mensaje += "Seleccione el producto\n";
            }

            if (obj.Lote == "")
            {
                Mensaje += "Ingrese el número de lote\n";
            }

            if (obj.FechaFabricacion == "")
            {
                Mensaje += "Ingrese la Fecha de fabricación\n";
            }

            if (obj.FechaVencimiento == "")
            {
                Mensaje += "Ingrese la Fecha de vencimiento\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objLotes.Crearlote(obj, out Mensaje);
            }
        }

        public bool EditarLote(ELOTE_PRODUCTO obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.oProductos.IdTProducto == 0)
            {
                Mensaje += "Seleccione el producto\n";
            }

            if (obj.Lote == "")
            {
                Mensaje += "Ingrese el número de lote\n";
            }

            if (obj.FechaFabricacion == "")
            {
                Mensaje += "Ingrese la Fecha de fabricación\n";
            }

            if (obj.FechaVencimiento == "")
            {
                Mensaje += "Ingrese la Fecha de vencimiento\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objLotes.Editarlote(obj, out Mensaje);
            }
        }

        public bool EliminarLote(ELOTE_PRODUCTO obj, out string Mensaje)
        {
            return objLotes.Eliminarlote(obj, out Mensaje);
        }
    }
}
