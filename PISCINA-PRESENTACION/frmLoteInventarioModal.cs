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
    public partial class frmLoteInventarioModal : Form
    {
        public PISCINA_ENTIDADES.ELOTE_PRODUCTO lotes = null;
        public frmLoteInventarioModal()
        {
            InitializeComponent();
        }

        private void frmLoteInventarioModal_Load(object sender, EventArgs e)
        {

            //Cargar lista de productos
            List<EPRODUCTOS> lista = new NPRODUCTOS().Listar();
            foreach (EPRODUCTOS item in lista)
            {
                cmbProducto.Items.Add(new OpcionCombo() { Valor = item.IdTProducto, Texto = item.NombreProducto });
            }
            cmbProducto.DisplayMember = "Texto";
            cmbProducto.ValueMember = "Valor";
            cmbProducto.SelectedIndex = 0;


            if (lotes != null)
            {
                txtId.Text = lotes.IdTLoteProducto.ToString();
                txtLote.Text = lotes.Lote.ToString();
                txtFechaFabricacionDT.Text = lotes.FechaFabricacion.ToString();
                txtFechaVencimientoDT.Text = lotes.FechaVencimiento.ToString();

                //Valor del combo
                foreach (OpcionCombo oc in cmbProducto.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(lotes.oProductos.IdTProducto))
                    {
                        int indiceCombo = cmbProducto.Items.IndexOf(oc);
                        cmbProducto.SelectedIndex = indiceCombo;
                        break;
                    }
                }
            }
        }


        private void LimpiarCampos()
        {
            cmbProducto.SelectedIndex = 0;
            txtLote.Text = string.Empty;
            txtFechaFabricacionDT.Value = DateTime.Now;
            txtFechaVencimientoDT.Value = DateTime.Now;

            cmbProducto.Select();

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            ELOTE_PRODUCTO objlote = new ELOTE_PRODUCTO()
            {
                IdTLoteProducto = Convert.ToInt32(txtId.Text),
                oProductos = new EPRODUCTOS () { IdTProducto = Convert.ToInt32(((OpcionCombo)cmbProducto.SelectedItem).Valor) },
                Lote =  txtLote.Text,
                FechaFabricacion = txtFechaFabricacionDT.Value.ToString("yyyy/MM/dd"),
                FechaVencimiento = txtFechaVencimientoDT.Value.ToString("yyyy/MM/dd"),
            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear
                int idLoteGenerado = new NLOTES().CrearLote(objlote, out mensaje);

                if (idLoteGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;
                }
                else // si no crea el Categoria, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                //Actualizar Categoria
                bool resultado = new NLOTES().EditarLote(objlote, out mensaje);

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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
