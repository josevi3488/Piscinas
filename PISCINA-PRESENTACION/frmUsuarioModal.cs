using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PISCINA_ENTIDADES;
using PISCINA_NEGOCIO;
using PISCINA_PRESENTACION.Utilidades;
using PISCINA_PRESENTACION;
using System.Windows.Forms;

namespace PISCINA_PRESENTACION
{
    public partial class frmUsuarioModal : Form
    {
        public PISCINA_ENTIDADES.EUSUARIOS usuarios = null;
        public PISCINA_ENTIDADES.EROLES oRol = null;

        private int idUsu;
        public frmUsuarioModal()
        {
            InitializeComponent();
            this.idUsu = 0;
        }

        public void cargarDatos(EUSUARIOS obj) { 
        

            this.idUsu = obj.IdTUsuario;
            txtUsuario.Text = obj.Usuario;

        
        }

        private void frmUsuarioModal_Load(object sender, EventArgs e)
        {

            //Cargar lista de Estados
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cmbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "INACTIVO" });
            cmbEstado.DisplayMember = "Texto";
            cmbEstado.ValueMember = "Valor";
            cmbEstado.SelectedIndex = 0;

            //Cargar lista de roles
            List<EROLES> listaRol = new NROLES().Listar();
            foreach (EROLES item in listaRol)
            {
                cmbRol.Items.Add(new OpcionCombo() { Valor = item.IdTRol, Texto = item.Descripcion });
            }
            cmbRol.DisplayMember = "Texto";
            cmbRol.ValueMember = "Valor";
            cmbRol.SelectedIndex = 0;


            if (usuarios != null)
            {
                txtId.Text =    usuarios.IdTUsuario.ToString();
                txtUsuario.Text = usuarios.Usuario.ToString();
                txtNombreCompleto.Text = usuarios.NombreCompleto.ToString();
                txtCorreo.Text = usuarios.Correo.ToString();
                txtClave.Text = usuarios.Clave.ToString();
                txtConfirmarClave.Text = usuarios.Clave.ToString();

                //Valor del rol
                foreach (OpcionCombo oc in cmbRol.Items) { 
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(usuarios.oRol.IdTRol))
                    {
                        int indiceCombo = cmbRol.Items.IndexOf(oc);
                        cmbRol.SelectedIndex = indiceCombo;
                        break;
                    }
                }

                //Valor de estado
                foreach (OpcionCombo oc in cmbEstado.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(usuarios.Estado))
                    {
                        int indiceCombo = cmbEstado.Items.IndexOf(oc);
                        cmbEstado.SelectedIndex = indiceCombo;
                        break;
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            EUSUARIOS objusuario = new EUSUARIOS()
            {
                IdTUsuario = Convert.ToInt32(txtId.Text),
                Usuario = txtUsuario.Text,
                NombreCompleto = txtNombreCompleto.Text,
                Correo = txtCorreo.Text,
                Clave = txtClave.Text,
                oRol = new EROLES() { IdTRol = Convert.ToInt32(((OpcionCombo)cmbRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cmbEstado.SelectedItem).Valor) == 1 ? true : false,
            };

            if (Convert.ToInt32(txtId.Text) == 0)
            {
                //Crear Usuario
                int idUsuarioGenerado = new NUSUARIOS().CrearUsuario(objusuario, out mensaje);

                if (idUsuarioGenerado != 0)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;
                }
                else // si no crea el usuario, muestra mensaje de error
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                //Actualizar Usuario
                bool resultado = new NUSUARIOS().EditarUsuario(objusuario, out mensaje);

                if (resultado)
                {
                    //refrescar Datagridview del formulario padre 
                    MessageBox.Show("Registro insertado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();

                    DialogResult = DialogResult.OK;
                }
                else { 
                    MessageBox.Show(mensaje); 
                }

            }

            //try
            //{
            //    if (usuarios == null) usuarios = new EUSUARIOS();
            //        usuarios.Usuario = txtUsuario.Text;
            //        usuarios.NombreCompleto = txtNombreCompleto.Text;
            //        usuarios.Correo = txtCorreo.Text;
            //        usuarios.Clave = txtClave.Text;
            //        //usuarios.oRol.IdTRol = 0;
            //        //usuarios.Estado = true;
            //    DialogResult = DialogResult.OK;

            //}
            //catch (Exception ex) {
            //    MessageBox.Show("Se ha producido un error en los datos \n" + ex.Message);
            //}

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void LimpiarCampos() { 
            txtUsuario.Text = string.Empty;
            txtNombreCompleto .Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtConfirmarClave.Text = string.Empty;
            cmbRol.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;

            txtUsuario.Select();

        }
    }
}
