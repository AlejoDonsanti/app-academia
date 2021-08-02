using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class Materias : FormButtons
    {
        public Materias()
        {
            InitializeComponent();
            this.SetearPermisos("Materias");
        }

        public void Listar() 
        {
            MateriaLogic ml = new MateriaLogic();
            PlanLogic pl = new PlanLogic();
            try
            {
                List<Materia> materias = ml.GetAll();
                List<Plan> planes = pl.GetAll();

                var query =
                        from m in materias
                        join p in planes
                        on m.IDPlan equals p.ID
                        select new
                        {
                            ID = m.ID,
                            Descripcion = m.Descripcion,
                            Hs_Semanales = m.HSSemanales,
                            Hs_Totales = m.HSTotales,
                            Plan = p.Descripcion
                        };
                dgvMateria.DataSource = query.ToList();

            }
            catch(Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de Materias", Ex);
                MessageBox.Show("Error al recuperar lista de Materias", "Materia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ExcepcionManejada;
            }
        }

        private void Materias_Load(object sender, EventArgs e)
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
            MateriaDesktop formMateria = new MateriaDesktop(ApplicationForm.ModoForm.Alta);
            formMateria.ShowDialog();
            this.Listar();
        }

        public override void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvMateria.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(this.dgvMateria.SelectedRows[0].Cells["ID"].Value);
                MateriaDesktop formMateria = new MateriaDesktop(ID,ApplicationForm.ModoForm.Modificacion);
                formMateria.ShowDialog();
                this.Listar();
            }
            else
            {
                MessageBox.Show("Seleccionar una fila en la grilla para poder editar");
            }
        }

        public override void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvMateria.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(this.dgvMateria.SelectedRows[0].Cells["ID"].Value);
                MateriaDesktop formMateria = new MateriaDesktop(ID, ApplicationForm.ModoForm.Baja);
                formMateria.ShowDialog();
                this.Listar();
            }
        }
    }
}
