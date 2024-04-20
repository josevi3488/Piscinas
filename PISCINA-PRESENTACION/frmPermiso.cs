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
    public partial class frmPermiso : Form
    {

        List<EPERMISOS> listaPermisos;

        public frmPermiso()
        {
            InitializeComponent();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvPermisos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvPermisos.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = string.Empty;
            foreach (DataGridViewRow row in dgvPermisos.Rows)
            {
                row.Visible = true;
            }
        }

        private void frmPermiso_Load(object sender, EventArgs e)
        {

            foreach (DataGridViewColumn dvgColumna in dgvPermisos.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;



            dgvPermisos.Rows.Clear();
            //Mostrar usuarios en el grid
            listaPermisos = new NPERMISOS().Listar();
            foreach (EPERMISOS item in listaPermisos)
            {
                dgvPermisos.Rows.Add(new Object[] {"",item.IdTPermiso,item.oRol.Descripcion,item.NombreMenu
                });

            }
        }

        private void dgvPermisos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var anchoImagen = Properties.Resources.check20.Width;
                var altoImagen = Properties.Resources.check20.Height;
                var ubicacionX = e.CellBounds.Left + (e.CellBounds.Width - anchoImagen) / 2;
                var ubicacionY = e.CellBounds.Top + (e.CellBounds.Height - altoImagen) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(ubicacionX, ubicacionY, anchoImagen, altoImagen));
                e.Handled = true;

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmPermisoModal frmPermisoModal = new frmPermisoModal();
            if (frmPermisoModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmPermiso_Load(sender, e);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmPermisoModal frmPermisoModal = new frmPermisoModal();
            string idPermisos = dgvPermisos.CurrentRow.Cells["IdTPermiso"].Value.ToString();
            var obj = listaPermisos.FirstOrDefault(o => o.IdTPermiso.Equals(Int32.Parse(idPermisos)));

            frmPermisoModal.permisos = obj;

            if (frmPermisoModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmPermiso_Load(sender, e);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el permiso?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    EPERMISOS objalmacenes = new EPERMISOS()
                    {
                        IdTPermiso = Convert.ToInt32(txtId.Text),
                    };

                    bool respuesta = new NPERMISOS().EliminarPermisos(objalmacenes, out mensaje);

                    if (respuesta)
                    {
                        MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvPermisos.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void dgvPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPermisos.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvPermisos.Rows[indice].Cells["IdTPermiso"].Value.ToString();
                }
            }
        }
    }
}
