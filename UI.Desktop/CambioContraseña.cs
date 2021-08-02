using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class CambioContraseña : Form
    {
        public CambioContraseña()
        {
            InitializeComponent();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session.Usuario.Clave==Convert.ToString(txtContraseñaVieja.Text))
            {
                
                if (txtContraseñaNueva.Text==txtContraseñaNuevaRepetir.Text && !string.IsNullOrEmpty(txtContraseñaNueva.Text))
                {
                    UsuarioLogic logica = new UsuarioLogic();
                    Session.Usuario.Clave = Convert.ToString(txtContraseñaNueva.Text);
                    Session.Usuario.State = BusinessEntity.States.Modified;
                    logica.Save(Session.Usuario);
                }
                else
                {
                    MessageBox.Show("La nueva contraseña y la confirmacion no coinciden");
                }
            }
            else
            {
                MessageBox.Show("Contraseña Actual Erronea");
            };
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtContraseñaNueva.PasswordChar == '\0')
            {
                txtContraseñaNueva.PasswordChar = '*';
                txtContraseñaNuevaRepetir.PasswordChar = '*';
                txtContraseñaVieja.PasswordChar = '*';

            }
            else
            {
                txtContraseñaNueva.PasswordChar = '\0';
                txtContraseñaNuevaRepetir.PasswordChar = '\0';
                txtContraseñaVieja.PasswordChar = '\0';
            }
        }
    }
}
