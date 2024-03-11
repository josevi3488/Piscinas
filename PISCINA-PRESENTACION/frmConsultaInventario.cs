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
    public partial class frmConsultaInventario : Form
    {

        List<EINVENTARIO> listaStock;
        List<EPRODUCTOS> listaProductos;
        public frmConsultaInventario()
        {
            InitializeComponent();
        }

        private void frmConsultaInventario_Load(object sender, EventArgs e)
        {
            listaProductos = new NPRODUCTOS().Listar();

            cmbProducto.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Todos" });
            foreach (EPRODUCTOS item in listaProductos)
            {
                cmbProducto.Items.Add(new OpcionCombo() { Valor = item.IdTProducto, Texto = item.CodigoProducto + " - " + item.NombreProducto });
            }
            cmbProducto.DisplayMember = "Texto";
            cmbProducto.ValueMember = "Valor";
            cmbProducto.SelectedIndex = 0;



            foreach (DataGridViewColumn dvgColumna in dgvStock.Columns)
            {
                if (dvgColumna.Visible == true)
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;



           
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(((OpcionCombo)cmbProducto.SelectedItem).Valor.ToString());

            List<EREPORTE_INVENTARIO> lista = new List<EREPORTE_INVENTARIO>();
            lista = new NREPORTES().ReporteInventario(idProducto);

            dgvStock.Rows.Clear();

            foreach (EREPORTE_INVENTARIO rc in lista) {
                dgvStock.Rows.Add( new object[]
                {
                    rc.IdTProducto,
                    rc.CodigoProducto,
                    rc.Produto,
                    rc.Almacen,
                    rc.StockMinimo,
                    rc.Stock,
                    rc.Accion
                });
            }

        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (dgvStock.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewRow row in dgvStock.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString()
                        });
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteInventario_{0}.xlsx",DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //XLWorkbook wb = 
                    }
                    catch {
                        MessageBox.Show("Error al generar el reporte","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if (dgvStock.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvStock.Rows)
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
            foreach (DataGridViewRow row in dgvStock.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
