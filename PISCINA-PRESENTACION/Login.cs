using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PISCINA_NEGOCIO;
using PISCINA_ENTIDADES;


namespace PISCINA_PRESENTACION
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //List<EUSUARIOS> list = new NUSUARIOS().Listar();

            EUSUARIOS obtenerUsuario= new NUSUARIOS().Listar().Where(u => u.Usuario==txtUsuario.Text && u.Clave==txtClave.Text).FirstOrDefault();    

            if (obtenerUsuario != null)
            {
                Inicio frmInicio = new Inicio(obtenerUsuario);
                frmInicio.Show();
                this.Hide();
                frmInicio.FormClosing += cerrarForm;
            }
            else
            {
                MessageBox.Show("Usuario no existe","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }


        }

        public void cerrarForm(object sender, FormClosingEventArgs e) {
            txtUsuario.Text = string.Empty;
            txtClave.Text= string.Empty;
            this.Show();
        }
    }
}
