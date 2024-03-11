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
    public partial class frmTipoMovimiento : Form
    {

        List<ETIPOS_MOVIMIENTOS> listaTipoMov;
        public frmTipoMovimiento()
        {
            InitializeComponent();
        }

        private void frmTipoMovimiento_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn dvgColumna in dgvTipoMov.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;



            //Mostrar TipoMov en el grid
            listaTipoMov = new NTIPOMOVIMIENTOS().Listar();
            foreach (ETIPOS_MOVIMIENTOS item in listaTipoMov)
            {
                dgvTipoMov.Rows.Add(new Object[] {"",item.IdTTipoMov,item.Descripcion,item.Movimiento,
                });

            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvTipoMov.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvTipoMov.Rows)
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
            foreach (DataGridViewRow row in dgvTipoMov.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvTipoMov_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
