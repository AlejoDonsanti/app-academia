using Business.Entities;
using Business.Entities.Dto;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class Permisos : Form
    {
        List<ModuloUsuario> permisos = new List<ModuloUsuario>();
        public Permisos()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            int c = 0;

            foreach (DataGridViewRow Datarow in dgvPermisos.Rows)
            {
                
                ModuloUsuario permiso = new ModuloUsuario();
                permiso.PermiteAlta = (bool)Datarow.Cells[0].Value;
                permiso.PermiteBaja = (bool)Datarow.Cells[1].Value;
                permiso.PermiteModificacion = (bool)Datarow.Cells[2].Value;
                permiso.PermiteConsulta = (bool)Datarow.Cells[3].Value;
                permiso.IdModulo = c;
                c++;
                permisos.Add(permiso);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Permisos_Load(object sender, EventArgs e)
        {
            ModuloUsuarioLogic logica = new ModuloUsuarioLogic();
            dgvPermisos.DataSource= logica.GetAll();
        }
    }
}
