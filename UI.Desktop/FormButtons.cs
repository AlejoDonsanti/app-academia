using Business.Entities;
using Business.Logic;
using System;

namespace UI.Desktop
{
    public partial class FormButtons : ApplicationForm
    {
        public FormButtons()
        {
            InitializeComponent();
        }

        protected void SetearPermisos(string modulo)
        {
            //ModuloUsuarioLogic logica = new ModuloUsuarioLogic();
            //ModuloUsuario moduloPermiso = logica.GetPermiso(modulo);
            if (Session.Persona.TipoPersona==Persona.TiposPersona.Administrativo)
            {
                this.tsbNuevo.Visible = true;
                this.tsbEditar.Visible = true;
                this.tsbEliminar.Visible = true;
                if (modulo=="Cursos" || modulo=="Planes")
                {
                    this.tsbExportar.Visible = true;
                }
                else
                {
                    this.tsbExportar.Visible = false;
                }
                
            }

        }

        public virtual void tsbNuevo_Click(object sender, EventArgs e)
        {

        }

        public virtual void tsbEditar_Click(object sender, EventArgs e)
        {

        }

        public virtual void tsbEliminar_Click(object sender, EventArgs e)
        {

        }

        public virtual void tsbExportar_Click(object sender, EventArgs e)
        {

        }
    }
}
