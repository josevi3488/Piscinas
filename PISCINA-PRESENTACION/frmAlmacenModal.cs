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
    public partial class frmAlmacenModal : Form
    {
        public PISCINA_ENTIDADES.EALMACENES almacenes = null;
        public frmAlmacenModal()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cmbEstado.SelectedIndex = 0;

            txtCodigo.Select();
        }

        private void frmAlmacenModal_Load(object sender, EventArgs e)
        {
            //Cargar lista de Estados
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "INACTIVO" });
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";
            cmbEstado.SelectedIndex = 0;


            if (almacenes != null)
            {
                txtCodigo.ReadOnly = true;
                txtId.Text = almacenes.IdTAlmacen.ToString();
                txtCodigo.Text = almacenes.CodigoAlmacen.ToString();
                txtDescripcion.Text = almacenes.Descripcion.ToString();

                //Valor de estado
                foreach (OpcionCombo oc in cmbEstado.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(almacenes.Estado))
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            EALMACENES objalmacenes = new EALMACENES()
            {
                IdTAlmacen = Convert.ToInt32(txtId.Text),
                CodigoAlmacen = txtCodigo.Text,
                Descripcion = txtDescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear almacenes
                int idalmacenGenerado = new NALMACENES().CrearAlmacen(objalmacenes, out mensaje);

                if (idalmacenGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;
                }
                else // si no crea el almacenes, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                //Actualizar almacenes
                bool resultado = new NALMACENES().EditarAlmacen(objalmacenes, out mensaje);

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
    }
}
