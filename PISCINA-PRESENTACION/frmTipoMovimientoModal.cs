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
    public partial class frmTipoMovimientoModal : Form
    {
        public PISCINA_ENTIDADES.ETIPOS_MOVIMIENTOS tmovimientos = null;
        public frmTipoMovimientoModal()
        {
            InitializeComponent();
        }


        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            cmbTipoMovimiento.SelectedIndex = 0;

            txtDescripcion.Select();
        }


        private void frmTipoMovimientoModal_Load(object sender, EventArgs e)
        {

            if (tmovimientos != null)
            {

                txtId.Text = tmovimientos.IdTTipoMov.ToString();
                txtDescripcion.Text = tmovimientos.Descripcion.ToString();
                cmbTipoMovimiento.Text = tmovimientos.Movimiento.ToString();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            ETIPOS_MOVIMIENTOS objtmov = new ETIPOS_MOVIMIENTOS()
            {
                IdTTipoMov = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text.ToString(),
                Movimiento = cmbTipoMovimiento.SelectedItem.ToString(),
               
            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear tipos movimientos
                int idtipoGenerado = new NTIPOMOVIMIENTOS().CrearTipoMovimiento(objtmov, out mensaje);

                if (idtipoGenerado != 0)
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
                bool resultado = new NTIPOMOVIMIENTOS().EditarTipoMovimiento(objtmov, out mensaje);

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
    }
}
