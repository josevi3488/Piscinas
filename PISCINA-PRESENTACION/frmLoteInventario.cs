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
using System.Globalization;

namespace PISCINA_PRESENTACION
{
    public partial class frmLoteInventario : Form
    {

        List<ELOTE_PRODUCTO> listaLoteProductos;
        CultureInfo culture = new CultureInfo("es-ES");
        public frmLoteInventario()
        {
            InitializeComponent();
        }

        private void frmLoteInventario_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn dvgColumna in dgvLotes.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;

            dgvLotes.Rows.Clear();
            //Mostrar Lotes en el grid
            listaLoteProductos = new NLOTES().Listar();
            foreach (ELOTE_PRODUCTO item in listaLoteProductos)
            {
                dgvLotes.Rows.Add(new Object[] {"", item.IdTLoteProducto,item.oProductos.CodigoProducto,item.oProductos.NombreProducto,item.Lote,item.FechaFabricacion,item.FechaVencimiento,
                });

            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvLotes.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvLotes.Rows)
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
            foreach (DataGridViewRow row in dgvLotes.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvLotes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            frmLoteInventarioModal frmLoteInventarioModal = new frmLoteInventarioModal();
            if (frmLoteInventarioModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmLoteInventario_Load(sender, e);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el lote?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    ELOTE_PRODUCTO objlotes = new ELOTE_PRODUCTO()
                    {
                        IdTLoteProducto = Convert.ToInt32(txtId.Text),
                    };

                    bool respuesta = new NLOTES().EliminarLote(objlotes, out mensaje);

                    if (respuesta)
                    {
                        MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvLotes.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
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
            frmLoteInventarioModal frmLoteInventarioModal = new frmLoteInventarioModal();
            string idLote = dgvLotes.CurrentRow.Cells["IdTLoteProducto"].Value.ToString();
            var obj = listaLoteProductos.FirstOrDefault(o => o.IdTLoteProducto.Equals(Int32.Parse(idLote)));

            frmLoteInventarioModal.lotes = obj;

            if (frmLoteInventarioModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmLoteInventario_Load(sender, e);
            }
        }

        private void dgvLotes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLotes.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvLotes.Rows[indice].Cells["IdTLoteProducto"].Value.ToString();
                }
            }
        }
    }
}
