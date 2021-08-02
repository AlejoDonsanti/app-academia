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
using static Business.Entities.Persona;

namespace UI.Desktop
{
    public partial class PersonaDesktop : ApplicationForm
    {
        public Persona personaActual;
        public PersonaDesktop()
        {
            InitializeComponent();
        }
        public PersonaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            PlanLogic plan = new PlanLogic();
            List<Plan> planes= plan.GetAll();
            this.comboIdPlan.DataSource = planes;
            this.btnAceptar.Text = "Guardar";
            comboTipoPersona.DataSource = Enum.GetValues(typeof(TiposPersona));
        }
        public PersonaDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            Personalogic persona = new Personalogic();
            personaActual = persona.GetOne(ID);
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
        public override void MapearDeDatos()
        {
            PlanLogic p = new PlanLogic();
            List<Plan> planes = p.GetAll();
            Plan plan = p.GetOne(personaActual.IDPlan);
            this.comboIdPlan.DataSource = planes;
            //this.cbMaterias.DataSource = enumerarPlan(materias).DefaultView;
            this.comboIdPlan.SelectedIndex = comboIdPlan.FindStringExact(plan.Descripcion);

            comboTipoPersona.DataSource = Enum.GetValues(typeof(TiposPersona));
            this.comboTipoPersona.SelectedIndex = comboTipoPersona.FindStringExact(personaActual.TipoPersona.ToString());

            this.dtFechaNacimiento.Value = personaActual.FechaNacimiento;
            this.txtApellido.Text = personaActual.Apellido;
            this.txtNombre.Text = personaActual.Nombre;
            this.txtDireccion.Text = personaActual.Direccion;
            this.txtEmail.Text = personaActual.Email;
            this.txtLegajo.Text = personaActual.Legajo.ToString();
            this.txtTelefono.Text = personaActual.Telefono;
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                personaActual = new Persona();
            }
            personaActual.Nombre = (this.txtNombre.Text);
            personaActual.Apellido = (this.txtApellido.Text);
            personaActual.Direccion = (this.txtDireccion.Text);
            personaActual.Email = (this.txtEmail.Text);
            personaActual.Telefono = (this.txtTelefono.Text);
            personaActual.IDPlan = (int)this.comboIdPlan.SelectedValue;
            personaActual.Legajo = int.Parse(this.txtLegajo.Text);
            personaActual.TipoPersona = (Persona.TiposPersona)this.comboTipoPersona.SelectedValue;
            personaActual.FechaNacimiento = this.dtFechaNacimiento.Value;
            switch (Modo)
            {
                case ModoForm.Alta:
                    personaActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    personaActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }
        public virtual void Eliminar()
        {
            Personalogic p = new Personalogic();
            p.Delete(personaActual.ID);
        }
        public virtual void GuardarCambios()
        {
            Personalogic c = new Personalogic();
            MapearADatos();
            c.Save(personaActual);
        }
        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
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
        public override bool Validar()
        {
            if ((string.IsNullOrWhiteSpace(this.txtNombre.Text)) ||
                (string.IsNullOrWhiteSpace(this.txtApellido.Text)) ||
                (this.dtFechaNacimiento.Value == null) ||
                (String.IsNullOrWhiteSpace(this.comboTipoPersona.Text)) ||
                (String.IsNullOrWhiteSpace(this.comboIdPlan.Text)))
            {
                Notificar("Error", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else { return true; }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btmPermisos_Click(object sender, EventArgs e)
        {

        }
    }
}
