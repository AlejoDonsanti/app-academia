using System;
using System.Windows.Forms;
using Business.Logic;
namespace UI.Desktop
{
    public partial class Especialidades : FormButtons
    {
        public Especialidades()
        {
            InitializeComponent();
            this.SetearPermisos("Especialidades");
        }
        public void Listar()
        {
            EspecialidadLogic el = new EspecialidadLogic();
            try
            {
                this.dgvEspecialidades.DataSource = el.GetAll();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de Especialidades", Ex);
                MessageBox.Show("Error al recuperar lista de Especialidades", "Especialidades", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ExcepcionManejada;

            }
            finally
            {
            }
        }

        private void Especialidades_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public override void tsbNuevo_Click(object sender, EventArgs e)
        {
            EspecialidadesDesktop formEsp = new EspecialidadesDesktop(ApplicationForm.ModoForm.Alta);
            formEsp.ShowDialog();
            this.Listar();
        }

        public override void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvEspecialidades.SelectedRows.Count > 0)
            {
                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadesDesktop formEsp = new EspecialidadesDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formEsp.ShowDialog();
                this.Listar();
            }
            else
            {
                MessageBox.Show("Seleccionar una fila en la grilla para poder editar");
            }
        }
        public override void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvEspecialidades.SelectedRows.Count > 0)
            {
                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadesDesktop formEsp = new EspecialidadesDesktop(ID, ApplicationForm.ModoForm.Baja);
                formEsp.ShowDialog();
                this.Listar();
            }
        }
    }
}
