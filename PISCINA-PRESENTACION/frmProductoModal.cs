using PISCINA_ENTIDADES;
using PISCINA_NEGOCIO;
using PISCINA_PRESENTACION.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PISCINA_PRESENTACION
{
    public partial class frmProductoModal : Form
    {
        public PISCINA_ENTIDADES.EPRODUCTOS productos = null;

        public frmProductoModal()
        {
            InitializeComponent();
        }


        private void LimpiarCampos()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            cmbCategoria.SelectedIndex = 0;
            txtStockMinimo.Text = string.Empty;
            txtCodigoBarra.Text = string.Empty;
            cmbEstado.SelectedIndex = 0;

            txtCodigo.Select();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            EPRODUCTOS objproductos = new EPRODUCTOS()
            {
                IdTProducto = Convert.ToInt32(txtId.Text),
                CodigoProducto = txtCodigo.Text,
                NombreProducto = txtNombre.Text,
                oCategoriaProducto = new  ECATEGORIA_PRODUCTOS() { IdTCategoria = Convert.ToInt32(((OpcionCombo)cmbCategoria.SelectedItem).Valor) },
                StockMinimo = Convert.ToDecimal(txtStockMinimo.Text),
                CodigoBarra = txtCodigoBarra.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (Convert.ToInt32(txtId.Text) == 0) //Crear productos
            {
                int idproductoGenerado = new NPRODUCTOS().CrearProducto(objproductos, out mensaje);

                if (idproductoGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado","Confirmación",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;

                }
                else // si no crea el productos, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
            else//Actualizar productos
            {
                
                bool resultado = new NPRODUCTOS().EditarProducto(objproductos, out mensaje);

                if (resultado)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }
        }

        private void frmProductoModal_Load(object sender, EventArgs e)
        {
            //Cargar lista de Estados
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "INACTIVO" });
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";
            cmbEstado.SelectedIndex = 0;

            //Cargar lista de categorias
            List<ECATEGORIA_PRODUCTOS> listaCatProductos = new NCATEGORIAS().Listar();
            foreach (ECATEGORIA_PRODUCTOS item in listaCatProductos)
            {
                cmbCategoria.Items.Add(new OpcionCombo() { Valor = item.IdTCategoria, Texto = item.Descripcion});
            }
            cmbCategoria.DisplayMember = "Texto";
            cmbCategoria.ValueMember = "Valor";
            cmbCategoria.SelectedIndex = 0;

            if (productos != null)
            {
                txtCodigo.ReadOnly = true;
                txtId.Text = productos.IdTProducto.ToString();
                txtCodigo.Text = productos.CodigoProducto.ToString();
                txtNombre.Text = productos.NombreProducto.ToString();

                //Valor de la categoria
                foreach (OpcionCombo ocCat in cmbCategoria.Items)
                {
                    if (Convert.ToInt32(ocCat.Valor) == Convert.ToInt32(productos.oCategoriaProducto.IdTCategoria))
                    {
                        int indiceCombo = cmbCategoria.Items.IndexOf(ocCat);
                        cmbCategoria.SelectedIndex = indiceCombo;
                        break;
                    }
                }

                txtStockMinimo.Text = productos.StockMinimo.ToString();
                txtCodigoBarra.Text = productos.CodigoBarra.ToString();

                //Valor de estado
                foreach (OpcionCombo oc in cmbEstado.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(productos.Estado))
                    {
                        int indiceCombo = cmbEstado.Items.IndexOf(oc);
                        cmbEstado.SelectedIndex = indiceCombo;
                        break;
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
