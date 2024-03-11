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
    public partial class frmTransaccionInventarioModal : Form
    {

        public frmTransaccionInventarioModal()
        {
            
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmTransaccionInventarioModal_Load(object sender, EventArgs e)
        {
            //Cargar lista de almacenes
            List<EALMACENES> listaAlmacen = new NALMACENES().Listar();
            foreach (EALMACENES item in listaAlmacen)
            {
                cmbAlmacen.Items.Add(new OpcionCombo() { Valor = item.IdTAlmacen, Texto = item.CodigoAlmacen + " - " + item.Descripcion });
            }
            cmbAlmacen.DisplayMember = "Texto";
            cmbAlmacen.ValueMember = "Valor";
            cmbAlmacen.SelectedIndex = 0;


            //Cargar lista de Tipos de movimientos
            List<ETIPOS_MOVIMIENTOS> listaTMovimiento = new NTIPOMOVIMIENTOS().Listar();
            foreach (ETIPOS_MOVIMIENTOS item in listaTMovimiento)
            {
                cmbTipoMovimiento.Items.Add(new OpcionCombo() { Valor = item.IdTTipoMov, Texto = item.Descripcion });
            }
            cmbTipoMovimiento.DisplayMember = "Texto";
            cmbTipoMovimiento.ValueMember = "Valor";
            cmbTipoMovimiento.SelectedIndex = 0;



            //Cargar lista de productos
            List<EPRODUCTOS> listaProducto = new NPRODUCTOS().Listar();
            foreach (EPRODUCTOS item in listaProducto)
            {
                cmbProducto.Items.Add(new OpcionCombo() { Valor = item.IdTProducto, Texto = item.CodigoProducto + " - " + item.NombreProducto });
            }
            cmbProducto.DisplayMember = "Texto";
            cmbProducto.ValueMember = "Valor";
            cmbProducto.SelectedIndex = 0;


            //Cargar lista de lotes de productos
            List<ELOTE_PRODUCTO> listaLotes = new NLOTES().Listar();
            foreach (ELOTE_PRODUCTO item in listaLotes)
            {
                cmbLote.Items.Add(new OpcionCombo() { Valor = item.IdTLoteProducto, Texto = item.Lote + " - " + item.oProductos.NombreProducto });
            }
            cmbLote.DisplayMember = "Texto";
            cmbLote.ValueMember = "Valor";
            cmbLote.SelectedIndex = 0;

            //txtUsuarioConectado.Text = usuarioConectado.IdTUsuario.ToString();

        }

        private void LimpiarCampos()
        {
            cmbAlmacen.SelectedIndex = 0;
            cmbTipoMovimiento.SelectedIndex = 0;
            cmbProducto.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            cmbLote.SelectedIndex = 0;

            cmbAlmacen.Select();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            EINVENTARIO objinventario = new EINVENTARIO()
            {
                oAlmacen = new EALMACENES() {IdTAlmacen= Convert.ToInt32(((OpcionCombo)cmbAlmacen.SelectedItem).Valor) },
                oTipoMovimiento = new ETIPOS_MOVIMIENTOS() {IdTTipoMov = Convert.ToInt32(((OpcionCombo)cmbTipoMovimiento.SelectedItem).Valor) },
                oProductos = new EPRODUCTOS() { IdTProducto = Convert.ToInt32(((OpcionCombo)cmbProducto.SelectedItem).Valor) },
                Cantidad = Convert.ToDecimal(txtCantidad.Text),
                RefDocumento = txtDocumento.Text,
                //oUsuario = new EUSUARIOS() { IdTUsuario = Convert.ToInt32(txtUsuarioConectado.Text)},
                oUsuario = new EUSUARIOS() { IdTUsuario = Convert.ToInt32(4) },
                oLote = new ELOTE_PRODUCTO() {IdTLoteProducto= Convert.ToInt32(((OpcionCombo)cmbLote.SelectedItem).Valor) },
            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear inventario
                int idinventarioGenerado = new NINVENTARIOS().CrearInventario(objinventario, out mensaje);

                if (idinventarioGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    LimpiarCampos();
                }
                else // si no crea el inventario, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
        }
    }
}
