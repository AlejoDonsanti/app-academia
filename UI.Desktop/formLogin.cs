using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
            this.txtUsuario.Text = "Juan";
            this.txtPass.Text = "12345678";
        }


        private void btnIngresar_Click(object sender, EventArgs e)
        {
            UsuarioLogic u = new UsuarioLogic();
            //la propiedad Text de los TextBox contiene el texto escrito en ellos
            Usuario usuario = u.Login(this.txtUsuario.Text, this.txtPass.Text);
            if(usuario != null)
            {
                this.DialogResult = DialogResult.OK;
                Session.Usuario = usuario;
                Personalogic logica = new Personalogic();
                Session.Persona= logica.GetOne(Session.Usuario.IDPersona.Value);
                
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña incorrectos", "Login"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LnkOlvidaPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            MessageBox.Show("Es Ud. un usuario muy descuidado, haga memoria", "Olvidé mi contraseña",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
