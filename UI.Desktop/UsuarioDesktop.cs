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
using UI.Desktop;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        public Persona Persona { get; set; }
        public UsuarioDesktop()
        {
            InitializeComponent();
        }
        public UsuarioDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public UsuarioDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            UsuarioLogic u = new UsuarioLogic();
            UsuarioActual = u.GetOne(ID);
            MapearDeDatos();
            switch (Modo) 
            {
                case ModoForm.Alta:
                    this.btnAceptar.Text = "Guardar";
                    break;
                case ModoForm.Modificacion:
                    this.btnAceptar.Text = "Guardar";
                    break;
                case ModoForm.Baja:
                    this.btnAceptar.Text = "Eliminar";
                    break;
                case ModoForm.Consulta:
                    this.btnAceptar.Text = "Aceptar";
                    break;
            }
        }


        public Usuario UsuarioActual;

        public virtual void MapearDeDatos()
        {
            this.txtNombreUsuario.Text = UsuarioActual.Nombre;
            this.txtApellido.Text = UsuarioActual.Apellido;
            this.txtClave.Text = UsuarioActual.Clave;
            this.txtUsuario.Text = UsuarioActual.NombreUsuario;
            this.txtEmail.Text = UsuarioActual.EMail;
            this.chkHabilitado.Checked = UsuarioActual.Habilitado;
            this.txtID.Text = UsuarioActual.ID.ToString();
        }
        public virtual void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                UsuarioActual = new Usuario();
            }
            UsuarioActual.Apellido = this.txtApellido.Text;
            UsuarioActual.Nombre = this.txtNombreUsuario.Text;
            UsuarioActual.Clave = this.txtClave.Text;
            UsuarioActual.NombreUsuario = this.txtUsuario.Text;
            UsuarioActual.EMail = this.txtEmail.Text;
            UsuarioActual.Habilitado = this.chkHabilitado.Checked;
            UsuarioActual.IDPersona = Persona.ID;
            switch (Modo)
            {
                case ModoForm.Alta:
                    UsuarioActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    UsuarioActual.State = BusinessEntity.States.Modified;
                    break;
            }

        }
        public virtual void Eliminar() 
        {
            UsuarioLogic u = new UsuarioLogic();
            u.Delete(UsuarioActual.ID);
            
        }
        public virtual void GuardarCambios() 
        {
            UsuarioLogic u = new UsuarioLogic();
            MapearADatos();
            u.Save(UsuarioActual);
        }
        public virtual bool Validar() 
        {
            if (string.IsNullOrWhiteSpace(this.txtApellido.Text) || string.IsNullOrWhiteSpace(this.txtNombreUsuario.Text) || string.IsNullOrWhiteSpace(this.txtUsuario.Text) || string.IsNullOrWhiteSpace(this.txtLegajo.Text))
            {
                Notificar("Error", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (this.txtClave.Text.Length < 8)
            {
                Notificar("Error", "La clave debe tener 8 o más caracteres", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (this.txtClave.Text != this.txtConfirmaClave.Text)
            {
                Notificar("Error", "La clave no coincide con la confirmación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!this.txtEmail.Text.Contains("@") || !this.txtEmail.Text.Contains("."))
            {
                Notificar("Error", "El Email no es valido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!string.IsNullOrEmpty(this.txtLegajo.Text))
            {
                Personalogic logica = new Personalogic();
                Persona = logica.GetOneByLegajo(txtLegajo.Text);
                if (Persona==null)
                {
                    Notificar("Error", "La persona no existe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else { return true; }

        }

        public void Notificar(string titulo, string mensaje, MessageBoxButtons
botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones,
        MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (Modo) 
            {
                case ModoForm.Alta:
                case ModoForm.Modificacion:
                    if (Validar())
                    {
                        GuardarCambios();
                        Close();
                    };
                    break;
                case ModoForm.Baja:
                    Eliminar();
                    Close();
                    break;
                case ModoForm.Consulta:
                    Close();
                    break;

            }





        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
