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
    public partial class frmCategoriaProducto : Form
    {
        List<ECATEGORIA_PRODUCTOS> listaCategoria;

        public frmCategoriaProducto()
        {
            InitializeComponent();
        }

        private void cargarGridCategorias() {

            dgvCategorias.Rows.Clear();

            //Mostrar Categorias en el grid
            listaCategoria = new NCATEGORIAS().Listar();
            foreach (ECATEGORIA_PRODUCTOS item in listaCategoria)
            {
                dgvCategorias.Rows.Add(new Object[] {"",item.IdTCategoria,item.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "ACTIVO" : "INACTIVO",
                });
            }
        }
        private void cargarDatos() {
            foreach (DataGridViewColumn dvgColumna in dgvCategorias.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;

            cargarGridCategorias();

            
        }

        private void frmCategoriaProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvCategorias.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvCategorias.Rows)
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
            foreach (DataGridViewRow row in dgvCategorias.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvCategorias_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            frmCategoriaProductoModal frmCategoriaModal = new frmCategoriaProductoModal();
            if (frmCategoriaModal.ShowDialog() == DialogResult.OK)     
            {
                //MessageBox.Show("Aceptar");

                //frmCategoriaProducto_Load(sender, e);


                cargarGridCategorias();

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    ECATEGORIA_PRODUCTOS objcategorias = new ECATEGORIA_PRODUCTOS()
                    {
                        IdTCategoria = Convert.ToInt32(txtId.Text),
                    };

                    bool respuesta = new NCATEGORIAS().EliminarCategoria(objcategorias, out mensaje);

                    if (respuesta)
                    {
                        MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvCategorias.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
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
            frmCategoriaProductoModal frmCategoriaModal = new frmCategoriaProductoModal();
            string idCategoria = dgvCategorias.CurrentRow.Cells["IdTCategoria"].Value.ToString();
            var obj = listaCategoria.FirstOrDefault(o => o.IdTCategoria.Equals(Int32.Parse(idCategoria)));

            frmCategoriaModal.categorias = obj;

            if (frmCategoriaModal.ShowDialog() == DialogResult.OK)     
            {
                //MessageBox.Show("Aceptar");
                //frmCategoriaProducto_Load(sender, e);

                cargarGridCategorias();

            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvCategorias.Rows[indice].Cells["IdTCategoria"].Value.ToString();
                }
            }
        }

    }
}
