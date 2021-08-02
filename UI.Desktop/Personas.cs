using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class Personas : FormButtons
    {
        private Form _activeForm = null;
        public Personas()
        {
            InitializeComponent();
            this.SetearPermisos("Personas");
        }
        public void Listar()
        {
            Personalogic logica = new Personalogic();
            List<Persona> personas = logica.GetAll();
            PlanLogic plogic = new PlanLogic();
            try
            {
                List<Plan> planes = plogic.GetAll();
                var listapersona = from persona in personas
                                   join plan in planes
                                   on persona.IDPlan equals plan.ID
                                   select new
                                   {
                                       persona.ID,
                                       Plan = plan.Descripcion,
                                       persona.Nombre,
                                       persona.Apellido,
                                       persona.Direccion,
                                       persona.Telefono,
                                       persona.FechaNacimiento,
                                       persona.Legajo,
                                       persona.TipoPersona,
                                       persona.IDPlan
                                   };
                dgvPersonas.DataSource = listapersona.ToList();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de personas", Ex);
                MessageBox.Show("Error al recuperar lista de personas", "personas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ExcepcionManejada;
            }
        }

        private void Personas_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }
        public override void tsbNuevo_Click(object sender, EventArgs e)
        {
            PersonaDesktop formPersona = new PersonaDesktop(ApplicationForm.ModoForm.Alta);
            formPersona.ShowDialog();
            this.Listar();
        }

        public override void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvPersonas.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(this.dgvPersonas.SelectedRows[0].Cells["ID"].Value);
                PersonaDesktop formPersona = new PersonaDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formPersona.ShowDialog();
                this.Listar();
            }
            else
            {
                MessageBox.Show("Seleccionar una fila en la grilla para poder editar");
            }
        }

        public override void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvPersonas.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(this.dgvPersonas.SelectedRows[0].Cells["ID"].Value);
                PersonaDesktop formPersona = new PersonaDesktop(ID, ApplicationForm.ModoForm.Baja);
                formPersona.ShowDialog();
                this.Listar();
            }
        }
    }
}
