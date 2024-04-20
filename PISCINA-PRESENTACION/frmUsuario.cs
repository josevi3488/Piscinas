using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PISCINA_PRESENTACION;
using PISCINA_ENTIDADES;
using PISCINA_NEGOCIO;
using System.Windows.Controls;
using PISCINA_PRESENTACION.Utilidades;

namespace PISCINA_PRESENTACION
{
    public partial class frmUsuario : Form
    {
        List<EUSUARIOS> listaUsuario;

        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {

            foreach (DataGridViewColumn dvgColumna in dgvUsuarios.Columns)
            {
                if(dvgColumna.Visible == true && dvgColumna.Name != "btnSeleccionar")
                {
                    cmbBusqueda.Items.Add(new OpcionCombo() { Valor = dvgColumna.Name, Texto = dvgColumna.HeaderText });
                }
            }
            cmbBusqueda.DisplayMember = "Texto";
            cmbBusqueda.ValueMember = "Valor";
            cmbBusqueda.SelectedIndex = 0;


            dgvUsuarios.Rows.Clear();
            //Mostrar usuarios en el grid
            listaUsuario = new NUSUARIOS().Listar();
            foreach (EUSUARIOS item in listaUsuario)
            {
                dgvUsuarios.Rows.Add(new Object[] {"",item.IdTUsuario,item.NombreCompleto,item.Usuario,item.Correo,item.Clave,
                item.oRol.IdTRol,
                item.oRol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "ACTIVO" : "INACTIVO",
                });

            }


        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmUsuarioModal frmUsuModal = new frmUsuarioModal();
            if (frmUsuModal.ShowDialog() == DialogResult.OK)     //  frmUsuModal.ShowDialog() - activa en forma modal  el formulario de usuarios
            {
                //MessageBox.Show("Aceptar");
                frmUsuario_Load(sender, e);

            }
        }

        private void dgvUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var anchoImagen = Properties.Resources.check20.Width;
                var altoImagen = Properties.Resources.check20.Height;
                var ubicacionX = e.CellBounds.Left + (e.CellBounds.Width - anchoImagen)/2;
                var ubicacionY = e.CellBounds.Top + (e.CellBounds.Height - altoImagen) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20,new Rectangle(ubicacionX,ubicacionY,anchoImagen,altoImagen));
                e.Handled = true;

            }
 
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvUsuarios.Rows[indice].Cells["IdTUsuario"].Value.ToString();                    
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmUsuarioModal frmUsuModal = new frmUsuarioModal();
            string idUsuario = dgvUsuarios.CurrentRow.Cells["IdTUsuario"].Value.ToString();
            var obj = listaUsuario.FirstOrDefault(o => o.IdTUsuario.Equals(Int32.Parse(idUsuario)));
            
            frmUsuModal.usuarios = obj;

            if (frmUsuModal.ShowDialog() == DialogResult.OK)     //  frmUsuModal.ShowDialog() - activa en forma modal  el formulario de usuarios
            {
                //MessageBox.Show("Aceptar");
                frmUsuario_Load(sender, e);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtId.Text) != 0)
            {
                if(MessageBox.Show("¿Desea eliminar el usuario?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    EUSUARIOS objusuario = new EUSUARIOS()
                    {
                        IdTUsuario = Convert.ToInt32(txtId.Text),
                    };

                    bool respuesta = new NUSUARIOS().EliminarUsuario(objusuario, out mensaje);

                    if (respuesta)
                    {
                        dgvUsuarios.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cmbBusqueda.SelectedItem).Valor.ToString();
            if(dgvUsuarios.Rows.Count > 0 ) { 
                foreach(DataGridViewRow row in dgvUsuarios.Rows)
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
            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
