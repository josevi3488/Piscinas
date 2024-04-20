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
    public partial class frmAlmacen : Form
    {

        List<EALMACENES> listaAlmacenes;

        public frmAlmacen()
        {
            InitializeComponent();
        }

        private void frmAlmacen_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn dvgColumna in dgvAlmacenes.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;

            dgvAlmacenes.Rows.Clear();
            //Mostrar Almacenes en el grid
            listaAlmacenes = new NALMACENES().Listar();
            foreach (EALMACENES item in listaAlmacenes)
            {
                dgvAlmacenes.Rows.Add(new Object[] {"",item.IdTAlmacen,item.CodigoAlmacen,item.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "ACTIVO" : "INACTIVO",
                });

            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvAlmacenes.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvAlmacenes.Rows)
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
            foreach (DataGridViewRow row in dgvAlmacenes.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvAlmacenes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            frmAlmacenModal frmAlmacenModal = new frmAlmacenModal();
            if (frmAlmacenModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmAlmacen_Load(sender, e);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    EALMACENES objalmacenes = new EALMACENES()
                    {
                        IdTAlmacen = Convert.ToInt32(txtId.Text),
                    };

                    bool respuesta = new NALMACENES().EliminarAlmacen(objalmacenes, out mensaje);

                    if (respuesta)
                    {
                        MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvAlmacenes.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmAlmacenModal frmAlmacenModal = new frmAlmacenModal();
            string idAlmacen = dgvAlmacenes.CurrentRow.Cells["IdTAlmacen"].Value.ToString();
            var obj = listaAlmacenes.FirstOrDefault(o => o.IdTAlmacen.Equals(Int32.Parse(idAlmacen)));

            frmAlmacenModal.almacenes = obj;

            if (frmAlmacenModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmAlmacen_Load(sender, e);
            }
        }

        private void dgvAlmacenes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAlmacenes.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvAlmacenes.Rows[indice].Cells["IdTAlmacen"].Value.ToString();
                }
            }
        }
    }
}
