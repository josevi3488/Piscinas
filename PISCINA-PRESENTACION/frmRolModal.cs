using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class frmRolModal : Form
    {
        public PISCINA_ENTIDADES.EROLES roles = null;

        public frmRolModal()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtDescripcion.Select();
        }


        private void frmRolModal_Load(object sender, EventArgs e)
        {
            if (roles != null)
            {
                txtId.Text = roles.IdTRol.ToString();
                txtDescripcion.Text = roles.Descripcion.ToString(); 
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;

            EROLES objroles = new EROLES()
            {
                IdTRol = Convert.ToInt32(txtId.Text),
                Descripcion = txtDescripcion.Text,

            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear
                int idGenerado = new NROLES().CrearRol(objroles, out mensaje);

                if (idGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;
                }
                else // si no crea, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                //Actualizar
                bool resultado = new NROLES().EditarRol(objroles, out mensaje);

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
