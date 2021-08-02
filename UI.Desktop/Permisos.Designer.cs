namespace UI.Desktop
{
    partial class Permisos
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
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnAceptar = new FontAwesome.Sharp.IconButton();
            this.dgvPermisos = new System.Windows.Forms.DataGridView();
            this.permiteAltaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.permiteBajaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.permiteModificacionDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.permiteConsultaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.descModuloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.permisoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.permisoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnCancelar.IconColor = System.Drawing.Color.Black;
            this.btnCancelar.IconSize = 16;
            this.btnCancelar.Location = new System.Drawing.Point(763, 329);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Rotation = 0D;
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnAceptar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnAceptar.IconColor = System.Drawing.Color.Black;
            this.btnAceptar.IconSize = 16;
            this.btnAceptar.Location = new System.Drawing.Point(682, 329);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Rotation = 0D;
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // dgvPermisos
            // 
            this.dgvPermisos.AllowUserToAddRows = false;
            this.dgvPermisos.AllowUserToDeleteRows = false;
            this.dgvPermisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPermisos.AutoGenerateColumns = false;
            this.dgvPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPermisos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.permiteAltaDataGridViewCheckBoxColumn,
            this.permiteBajaDataGridViewCheckBoxColumn,
            this.permiteModificacionDataGridViewCheckBoxColumn,
            this.permiteConsultaDataGridViewCheckBoxColumn,
            this.descModuloDataGridViewTextBoxColumn});
            this.dgvPermisos.DataSource = this.permisoBindingSource;
            this.dgvPermisos.Location = new System.Drawing.Point(13, 13);
            this.dgvPermisos.Name = "dgvPermisos";
            this.dgvPermisos.Size = new System.Drawing.Size(825, 310);
            this.dgvPermisos.TabIndex = 12;
            // 
            // permiteAltaDataGridViewCheckBoxColumn
            // 
            this.permiteAltaDataGridViewCheckBoxColumn.DataPropertyName = "PermiteAlta";
            this.permiteAltaDataGridViewCheckBoxColumn.HeaderText = "PermiteAlta";
            this.permiteAltaDataGridViewCheckBoxColumn.Name = "permiteAltaDataGridViewCheckBoxColumn";
            this.permiteAltaDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // permiteBajaDataGridViewCheckBoxColumn
            // 
            this.permiteBajaDataGridViewCheckBoxColumn.DataPropertyName = "PermiteBaja";
            this.permiteBajaDataGridViewCheckBoxColumn.HeaderText = "PermiteBaja";
            this.permiteBajaDataGridViewCheckBoxColumn.Name = "permiteBajaDataGridViewCheckBoxColumn";
            this.permiteBajaDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // permiteModificacionDataGridViewCheckBoxColumn
            // 
            this.permiteModificacionDataGridViewCheckBoxColumn.DataPropertyName = "PermiteModificacion";
            this.permiteModificacionDataGridViewCheckBoxColumn.HeaderText = "PermiteModificacion";
            this.permiteModificacionDataGridViewCheckBoxColumn.Name = "permiteModificacionDataGridViewCheckBoxColumn";
            this.permiteModificacionDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // permiteConsultaDataGridViewCheckBoxColumn
            // 
            this.permiteConsultaDataGridViewCheckBoxColumn.DataPropertyName = "PermiteConsulta";
            this.permiteConsultaDataGridViewCheckBoxColumn.HeaderText = "PermiteConsulta";
            this.permiteConsultaDataGridViewCheckBoxColumn.Name = "permiteConsultaDataGridViewCheckBoxColumn";
            this.permiteConsultaDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // descModuloDataGridViewTextBoxColumn
            // 
            this.descModuloDataGridViewTextBoxColumn.DataPropertyName = "DescModulo";
            this.descModuloDataGridViewTextBoxColumn.HeaderText = "Modulo";
            this.descModuloDataGridViewTextBoxColumn.Name = "descModuloDataGridViewTextBoxColumn";
            this.descModuloDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // permisoBindingSource
            // 
            this.permisoBindingSource.DataSource = typeof(Business.Entities.Dto.Permiso);
            // 
            // Permisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 364);
            this.Controls.Add(this.dgvPermisos);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Name = "Permisos";
            this.Text = "Permisos";
            this.Load += new System.EventHandler(this.Permisos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermisos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.permisoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnAceptar;
        private System.Windows.Forms.DataGridView dgvPermisos;
        private System.Windows.Forms.BindingSource permisoBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn permiteAltaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn permiteBajaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn permiteModificacionDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn permiteConsultaDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descModuloDataGridViewTextBoxColumn;
    }
}