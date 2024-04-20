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
    public partial class frmCategoriaProductoModal : Form
    {

        public PISCINA_ENTIDADES.ECATEGORIA_PRODUCTOS categorias = null;
        public frmCategoriaProductoModal()
        {
            InitializeComponent();
        }


        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            cmbEstado.SelectedIndex = 0;

            txtDescripcion.Select();

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            ECATEGORIA_PRODUCTOS objcategorias = new ECATEGORIA_PRODUCTOS()
            {
                IdTCategoria = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear Categoria
                int idCategoriaGenerado = new NCATEGORIAS().CrearCategoria(objcategorias, out mensaje);

                if (idCategoriaGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;


                }
                else // si no crea el Categoria, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                //Actualizar Categoria
                bool resultado = new NCATEGORIAS().EditarCategoria(objcategorias, out mensaje);

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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmCategoriaProductoModal_Load(object sender, EventArgs e)
        {
            //Cargar lista de Estados
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "INACTIVO" });
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";
            cmbEstado.SelectedIndex = 0;


            if (categorias != null)
            {
                txtId.Text = categorias.IdTCategoria.ToString();
                txtDescripcion.Text = categorias.Descripcion.ToString();


                //Valor de estado
                foreach (OpcionCombo oc in cmbEstado.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(categorias.Estado))
                    {
                        int indiceCombo = cmbEstado.Items.IndexOf(oc);
                        cmbEstado.SelectedIndex = indiceCombo;
                        break;
                    }
                }
            }


        }
    }
}
