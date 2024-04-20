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
    public partial class frmTransaccionInventario : Form
    {

        List<EINVENTARIO> listaInventario;
        private EUSUARIOS usuarioConectado;

        public frmTransaccionInventario(EUSUARIOS oUsuario = null)
        {
            usuarioConectado = oUsuario;
            InitializeComponent();
        }

        private void frmTransaccionInventario_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn dvgColumna in dgvInventarios.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;


            dgvInventarios.Rows.Clear();
            //Mostrar Inventarios en el grid
            listaInventario = new NINVENTARIOS().Listar();
            foreach (EINVENTARIO item in listaInventario)
            {
                dgvInventarios.Rows.Add(new Object[] {"",item.IdTInventario,item.oAlmacen.Descripcion,item.oTipoMovimiento.Descripcion,item.oProductos.CodigoProducto +"-"+item.oProductos.NombreProducto ,item.Cantidad,item.RefDocumento,
                item.oUsuario.Usuario,
                item.oLote.Lote,
                });

            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvInventarios.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvInventarios.Rows)
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
            foreach (DataGridViewRow row in dgvInventarios.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmTransaccionInventarioModal frmTransInventarioModal = new frmTransaccionInventarioModal(usuarioConectado);
            if (frmTransInventarioModal.ShowDialog() == DialogResult.OK)    
            {
                //MessageBox.Show("Aceptar");
                frmTransaccionInventario_Load(sender, e);
            }
        }

        private void dgvInventarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
    }
}
