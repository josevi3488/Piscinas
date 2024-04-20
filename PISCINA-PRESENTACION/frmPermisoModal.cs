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
    public partial class frmPermisoModal : Form
    {
        public PISCINA_ENTIDADES.EPERMISOS permisos = null;
        public frmPermisoModal()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            cmbRol.SelectedIndex = 0;
            chkMenu.SelectedIndex = 0;
            cmbRol.Select();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            int creado = 0;
            string menu = string.Empty;

            foreach (string itemchecked in chkMenu.CheckedItems)
            {
                EPERMISOS objpermisos = new EPERMISOS()
                {
                    IdTPermiso = Convert.ToInt32(txtId.Text),
                    oRol = new EROLES() { IdTRol = Convert.ToInt32(((OpcionCombo)cmbRol.SelectedItem).Valor) },
                    NombreMenu = itemchecked,
                };

                if (Convert.ToInt32(txtId.Text) == 0)
                {
                    //Crear
                    int idGenerado = new NPERMISOS().CrearPermisos(objpermisos, out mensaje);

                    if (idGenerado != 0)
                    {
                        creado = 1;
                    }
                    else // si no crea, muestra mensaje de error
                    {
                        MessageBox.Show(mensaje);
                    }
                }
                else
                {
                    //Actualizar
                    bool resultado = new NPERMISOS().EditarPermisos(objpermisos, out mensaje);

                    if (resultado)
                    {
                        creado = 1;
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
            }

            if (creado == 1)
            {
                MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                DialogResult = DialogResult.OK;
            }
        }

        private void frmPermisoModal_Load(object sender, EventArgs e)
        {
            //Cargar lista de roles
            List<EROLES> listaRol = new NROLES().Listar();
            foreach (EROLES item in listaRol)
            {
                cmbRol.Items.Add(new OpcionCombo() { Valor = item.IdTRol, Texto = item.Descripcion });
            }
            cmbRol.DisplayMember = "Texto";
            cmbRol.ValueMember = "Valor";
            cmbRol.SelectedIndex = 0;


            if (permisos != null)
            {
                txtId.Text = permisos.IdTPermiso.ToString();

                //Valor de estado
                foreach (OpcionCombo oc in cmbRol.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(permisos.oRol.IdTRol))
                    {
                        int indiceCombo = cmbRol.Items.IndexOf(oc);
                        cmbRol.SelectedIndex = indiceCombo;
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
