using PISCINA_ENTIDADES;
using PISCINA_NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace PISCINA_PRESENTACION
{
    public partial class Inicio : Form
    {
        private static EUSUARIOS usuarioConectado;
        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo = null;

        public Inicio(EUSUARIOS objUsuario)
        {
            usuarioConectado = objUsuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<EPERMISOS> listaPermisos = new NPERMISOS().Listar(usuarioConectado.IdTUsuario);

            foreach (IconMenuItem iconMenu in menuPrincipal.Items)
            {
                bool menuEncontrado = listaPermisos.Any(m => m.NombreMenu == iconMenu.Name);
                
                if(menuEncontrado == false)
                {
                    iconMenu.Visible = false;
                }
                
            }

            lblUsuario.Text = usuarioConectado.NombreCompleto;
        }

        private void fechaHora_Tick(object sender, EventArgs e)
        {
            lblFechaSistema.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy ") + DateTime.Now.ToString(" hh:mm:ss ");
        }

        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            
            if (menuActivo != null) {
                //menuActivo.BackColor = Color.White;
            }
            menuActivo = menu;
            menuActivo.BackColor = Color.Silver;
            
            
            if(formularioActivo != null)
            {
                formularioActivo.Close();
            }
            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle= FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.White;

            contenedor.Controls.Add(formulario);
            formulario.Show();

        }

        private void subMenuUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuSeguridad, new frmUsuario());
        }

        private void subMenuPermisos_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuSeguridad, new frmPermiso());
        }

        private void subMenuRoles_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuSeguridad, new frmRol());
        }

        private void subMenuCategoria_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuProductos, new frmCategoriaProducto());
        }

        private void subMenuProducto_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuProductos, new frmProducto());
        }

        private void subMenuConsultaInventario_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuInventarios, new frmConsultaInventario());
        }

        private void subMenuLotes_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuInventarios, new frmLoteInventario());
        }

        private void subMenuAlmacenes_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuInventarios, new frmAlmacen());
        }

        private void subMenuTipoMovimiento_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuInventarios, new frmTipoMovimiento());
        }

        private void subMenuTransacciones_Click(object sender, EventArgs e)
        {
            abrirFormulario(MenuInventarios, new frmTransaccionInventario());
        }
    }
}
