namespace PISCINA_PRESENTACION
{
    partial class Inicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.MenuProductos = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuProducto = new FontAwesome.Sharp.IconMenuItem();
            this.MenuInventarios = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuConsultaInventario = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuLotes = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuAlmacenes = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuTipoMovimiento = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuTransacciones = new FontAwesome.Sharp.IconMenuItem();
            this.MenuSeguridad = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRoles = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuPermisos = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblFechaSistema = new System.Windows.Forms.Label();
            this.fechaHora = new System.Windows.Forms.Timer(this.components);
            this.menuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.AutoSize = false;
            this.menuPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.menuPrincipal.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuProductos,
            this.MenuInventarios,
            this.MenuSeguridad});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 90);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuPrincipal.Size = new System.Drawing.Size(352, 853);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "menuStrip1";
            // 
            // MenuProductos
            // 
            this.MenuProductos.AutoSize = false;
            this.MenuProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MenuProductos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuCategoria,
            this.subMenuProducto});
            this.MenuProductos.IconChar = FontAwesome.Sharp.IconChar.PersonSwimming;
            this.MenuProductos.IconColor = System.Drawing.Color.Black;
            this.MenuProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuProductos.IconSize = 30;
            this.MenuProductos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuProductos.Name = "MenuProductos";
            this.MenuProductos.Size = new System.Drawing.Size(200, 45);
            this.MenuProductos.Text = "Gestión de Productos";
            // 
            // subMenuCategoria
            // 
            this.subMenuCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuCategoria.IconColor = System.Drawing.Color.Black;
            this.subMenuCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuCategoria.Name = "subMenuCategoria";
            this.subMenuCategoria.Size = new System.Drawing.Size(248, 26);
            this.subMenuCategoria.Text = "Categoría de Productos";
            this.subMenuCategoria.Click += new System.EventHandler(this.subMenuCategoria_Click);
            // 
            // subMenuProducto
            // 
            this.subMenuProducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuProducto.IconColor = System.Drawing.Color.Black;
            this.subMenuProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuProducto.Name = "subMenuProducto";
            this.subMenuProducto.Size = new System.Drawing.Size(248, 26);
            this.subMenuProducto.Text = "Productos";
            this.subMenuProducto.Click += new System.EventHandler(this.subMenuProducto_Click);
            // 
            // MenuInventarios
            // 
            this.MenuInventarios.AutoSize = false;
            this.MenuInventarios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MenuInventarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuConsultaInventario,
            this.subMenuLotes,
            this.subMenuAlmacenes,
            this.subMenuTipoMovimiento,
            this.subMenuTransacciones});
            this.MenuInventarios.IconChar = FontAwesome.Sharp.IconChar.Store;
            this.MenuInventarios.IconColor = System.Drawing.Color.Black;
            this.MenuInventarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuInventarios.IconSize = 30;
            this.MenuInventarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuInventarios.Margin = new System.Windows.Forms.Padding(1);
            this.MenuInventarios.Name = "MenuInventarios";
            this.MenuInventarios.Size = new System.Drawing.Size(200, 45);
            this.MenuInventarios.Text = "Gestión de Inventarios";
            // 
            // subMenuConsultaInventario
            // 
            this.subMenuConsultaInventario.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuConsultaInventario.IconColor = System.Drawing.Color.Black;
            this.subMenuConsultaInventario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuConsultaInventario.Name = "subMenuConsultaInventario";
            this.subMenuConsultaInventario.Size = new System.Drawing.Size(274, 26);
            this.subMenuConsultaInventario.Text = "Consulta de Inventarios";
            this.subMenuConsultaInventario.Click += new System.EventHandler(this.subMenuConsultaInventario_Click);
            // 
            // subMenuLotes
            // 
            this.subMenuLotes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuLotes.IconColor = System.Drawing.Color.Black;
            this.subMenuLotes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuLotes.Name = "subMenuLotes";
            this.subMenuLotes.Size = new System.Drawing.Size(274, 26);
            this.subMenuLotes.Text = "Lotes de Inventario";
            this.subMenuLotes.Click += new System.EventHandler(this.subMenuLotes_Click);
            // 
            // subMenuAlmacenes
            // 
            this.subMenuAlmacenes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuAlmacenes.IconColor = System.Drawing.Color.Black;
            this.subMenuAlmacenes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuAlmacenes.Name = "subMenuAlmacenes";
            this.subMenuAlmacenes.Size = new System.Drawing.Size(274, 26);
            this.subMenuAlmacenes.Text = "Almacenes";
            this.subMenuAlmacenes.Click += new System.EventHandler(this.subMenuAlmacenes_Click);
            // 
            // subMenuTipoMovimiento
            // 
            this.subMenuTipoMovimiento.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuTipoMovimiento.IconColor = System.Drawing.Color.Black;
            this.subMenuTipoMovimiento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuTipoMovimiento.Name = "subMenuTipoMovimiento";
            this.subMenuTipoMovimiento.Size = new System.Drawing.Size(274, 26);
            this.subMenuTipoMovimiento.Text = "Tipos de Movimiento";
            this.subMenuTipoMovimiento.Click += new System.EventHandler(this.subMenuTipoMovimiento_Click);
            // 
            // subMenuTransacciones
            // 
            this.subMenuTransacciones.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuTransacciones.IconColor = System.Drawing.Color.Black;
            this.subMenuTransacciones.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuTransacciones.Name = "subMenuTransacciones";
            this.subMenuTransacciones.Size = new System.Drawing.Size(274, 26);
            this.subMenuTransacciones.Text = "Transacciones de Inventario";
            this.subMenuTransacciones.Click += new System.EventHandler(this.subMenuTransacciones_Click);
            // 
            // MenuSeguridad
            // 
            this.MenuSeguridad.AutoSize = false;
            this.MenuSeguridad.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MenuSeguridad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRoles,
            this.subMenuPermisos,
            this.subMenuUsuarios});
            this.MenuSeguridad.IconChar = FontAwesome.Sharp.IconChar.Key;
            this.MenuSeguridad.IconColor = System.Drawing.Color.Black;
            this.MenuSeguridad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuSeguridad.IconSize = 30;
            this.MenuSeguridad.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuSeguridad.Name = "MenuSeguridad";
            this.MenuSeguridad.Size = new System.Drawing.Size(200, 45);
            this.MenuSeguridad.Text = "Gestión de Seguridad";
            // 
            // subMenuRoles
            // 
            this.subMenuRoles.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuRoles.IconColor = System.Drawing.Color.Black;
            this.subMenuRoles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuRoles.Name = "subMenuRoles";
            this.subMenuRoles.Size = new System.Drawing.Size(150, 26);
            this.subMenuRoles.Text = "Roles";
            this.subMenuRoles.Click += new System.EventHandler(this.subMenuRoles_Click);
            // 
            // subMenuPermisos
            // 
            this.subMenuPermisos.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuPermisos.IconColor = System.Drawing.Color.Black;
            this.subMenuPermisos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuPermisos.Name = "subMenuPermisos";
            this.subMenuPermisos.Size = new System.Drawing.Size(150, 26);
            this.subMenuPermisos.Text = "Permisos";
            this.subMenuPermisos.Click += new System.EventHandler(this.subMenuPermisos_Click);
            // 
            // subMenuUsuarios
            // 
            this.subMenuUsuarios.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuUsuarios.IconColor = System.Drawing.Color.Black;
            this.subMenuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuUsuarios.Name = "subMenuUsuarios";
            this.subMenuUsuarios.Size = new System.Drawing.Size(150, 26);
            this.subMenuUsuarios.Text = "Usuarios";
            this.subMenuUsuarios.Click += new System.EventHandler(this.subMenuUsuarios_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AutoSize = false;
            this.menuStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip2.Size = new System.Drawing.Size(1582, 90);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(33, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "PISCIMANT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(484, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Gestión de Inventario";
            // 
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(352, 90);
            this.contenedor.Margin = new System.Windows.Forms.Padding(4);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1230, 853);
            this.contenedor.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(1012, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bienvenido:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblUsuario.Location = new System.Drawing.Point(1187, 17);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(98, 25);
            this.lblUsuario.TabIndex = 6;
            this.lblUsuario.Text = "lblUsuario";
            // 
            // lblFechaSistema
            // 
            this.lblFechaSistema.AutoSize = true;
            this.lblFechaSistema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(41)))), ((int)(((byte)(127)))));
            this.lblFechaSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaSistema.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblFechaSistema.Location = new System.Drawing.Point(992, 47);
            this.lblFechaSistema.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaSistema.Name = "lblFechaSistema";
            this.lblFechaSistema.Size = new System.Drawing.Size(106, 20);
            this.lblFechaSistema.TabIndex = 7;
            this.lblFechaSistema.Text = "Fecha y hora";
            // 
            // fechaHora
            // 
            this.fechaHora.Enabled = true;
            this.fechaHora.Tick += new System.EventHandler(this.fechaHora_Tick);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1582, 943);
            this.Controls.Add(this.lblFechaSistema);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuPrincipal);
            this.Controls.Add(this.menuStrip2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INICIO";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private FontAwesome.Sharp.IconMenuItem MenuProductos;
        private FontAwesome.Sharp.IconMenuItem MenuInventarios;
        private FontAwesome.Sharp.IconMenuItem MenuSeguridad;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblFechaSistema;
        private System.Windows.Forms.Timer fechaHora;
        private FontAwesome.Sharp.IconMenuItem subMenuCategoria;
        private FontAwesome.Sharp.IconMenuItem subMenuProducto;
        private FontAwesome.Sharp.IconMenuItem subMenuConsultaInventario;
        private FontAwesome.Sharp.IconMenuItem subMenuLotes;
        private FontAwesome.Sharp.IconMenuItem subMenuAlmacenes;
        private FontAwesome.Sharp.IconMenuItem subMenuTipoMovimiento;
        private FontAwesome.Sharp.IconMenuItem subMenuTransacciones;
        private FontAwesome.Sharp.IconMenuItem subMenuUsuarios;
        private FontAwesome.Sharp.IconMenuItem subMenuRoles;
        private FontAwesome.Sharp.IconMenuItem subMenuPermisos;
    }
}

