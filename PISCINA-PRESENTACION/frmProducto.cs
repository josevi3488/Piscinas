using ClosedXML.Excel;
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

    public partial class frmProducto : Form
    {
        List<EPRODUCTOS> listaProductos;

        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn dvgColumna in dgvProducto.Columns)
            {
                if (dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;


            dgvProducto.Rows.Clear();
            //Mostrar Productos en el grid
            listaProductos = new NPRODUCTOS().Listar();
            foreach (EPRODUCTOS item in listaProductos)
            {
                dgvProducto.Rows.Add(new Object[] {"",item.IdTProducto,item.CodigoProducto,item.NombreProducto,item.oCategoriaProducto.Descripcion,item.StockMinimo,item.CodigoBarra,

                item.Estado == true ? 1 : 0,
                item.Estado == true ? "ACTIVO" : "INACTIVO",
                });

            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvProducto.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProducto.Rows)
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
            foreach (DataGridViewRow row in dgvProducto.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvProducto_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            frmProductoModal frmProductoModal = new frmProductoModal();
            if (frmProductoModal.ShowDialog() == DialogResult.OK)
            {
                frmProducto_Load(sender, e);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    EPRODUCTOS objproductos = new EPRODUCTOS()
                    {
                        IdTProducto = Convert.ToInt32(txtId.Text),
                    };

                    bool respuesta = new NPRODUCTOS().EliminarProducto(objproductos, out mensaje);

                    if (respuesta)
                    {
                        MessageBox.Show("Registro eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvProducto.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        
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
            frmProductoModal frmProductoModal = new frmProductoModal();
            string idProducto = dgvProducto.CurrentRow.Cells["IdTProducto"].Value.ToString();
            var obj = listaProductos.FirstOrDefault(o => o.IdTProducto.Equals(Int32.Parse(idProducto)));

            frmProductoModal.productos = obj;

            if (frmProductoModal.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Aceptar");
                frmProducto_Load(sender, e);
            }
        }

        private void dgvProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducto.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvProducto.Rows[indice].Cells["IdTProducto"].Value.ToString();
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if(dgvProducto.Rows.Count < 1) 
            { 
                MessageBox.Show("No hay datos para exportar","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn columna in dgvProducto.Columns)
                {
                    if(columna.HeaderText != "" && columna.Visible)
                        dt.Columns.Add(columna.HeaderText,typeof(string));
                }

                foreach (DataGridViewRow row in dgvProducto.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]{
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                           
                        });
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("Reporte_Productos_{0}.xlsx",DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if(saveFile.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch {
                        MessageBox.Show("Hubo un error al generar el reporte","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }

                }
            }
        }
    }
}
