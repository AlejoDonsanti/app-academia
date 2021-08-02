using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class Notas : Form
    {
        public bool NoCargar { get; set; }
        int Id = 0;
        AlumnoInscripcionLogic alumnoInscripcionLogica = new AlumnoInscripcionLogic();

        public Notas()
        {
            InitializeComponent();
            Visibilidad();
                MapearCursos();            
                       
        }
        public void Visibilidad()
        {
            NoCargar = false;
            dgvCursos.Visible = false;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            lbCurso.Visible = false;
            lbMensaje.Visible = false;
            if (Session.Persona.TipoPersona==Persona.TiposPersona.Alumno)
            {
                lbApellido.Visible = false;
                lbNombre.Visible = false;
                lbNota.Visible = false;
                label2.Visible = false;
                txtApellido.Visible = false;
                txtNombre.Visible = false;
                txtCondicion.Visible = false;
                txtNota.Visible = false;
            }

        }
        public void MapearCursos()
        {

            CursoLogic cursoLogic = new CursoLogic();
            cbCurso.DataSource= cursoLogic.GetCursosAñoActual();
            cbCurso.SelectedIndex = -1;
            NoCargar = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NoCargar)
            {
                
                List<AlumnoInscripcion> listaInscripciones = alumnoInscripcionLogica.GetAllById((int)this.cbCurso.SelectedValue);
                if (Session.Persona.TipoPersona==Persona.TiposPersona.Alumno)
                {
                    dgvAlumnoSolo.AutoGenerateColumns = false;
                    var lista = from a in listaInscripciones
                                where a.IDAlumno == Session.Persona.ID
                                select a;
                    dgvAlumnoSolo.DataSource = lista.ToList();
                    lbMensaje1.Visible = false;
                    lbMensaje2.Visible = false;
                }
                else 
                {
                dgvAlumnoSolo.Visible = false;
                dgvCursos.Visible = true;
                btnGuardar.Visible = true;
                btnCancelar.Visible = true;
                lbCurso.Visible = true;
                lbMensaje.Visible = true;
                lbMensaje1.Visible = false;
                lbMensaje2.Visible = false;
                dgvCursos.AutoGenerateColumns = false;
                Personalogic personaLogic = new Personalogic();
                List<Persona> listaPersonas = personaLogic.GetAll();
                var query = from a in listaInscripciones
                            join p in listaPersonas
                            on a.IDAlumno equals p.ID
                            select new
                            {
                                Condicion = a.Condicion,
                                Nota = a.Nota,
                                Nombre = p.Nombre,
                                Apellido = p.Apellido,
                                ID = a.ID
                            };
                dgvCursos.DataSource = query.ToList();
                }

            }

        }

        private void dgvCursos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCursos_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvCursos.SelectedRows.Count > 0)
            {
                Id = Convert.ToInt32(this.dgvCursos.SelectedRows[0].Cells["ID"].Value);
                txtNombre.Text = Convert.ToString(this.dgvCursos.SelectedRows[0].Cells["Nombre"].Value);
                txtApellido.Text = Convert.ToString(this.dgvCursos.SelectedRows[0].Cells["Apellido"].Value);
                txtCondicion.Text = Convert.ToString(this.dgvCursos.SelectedRows[0].Cells["Condicion"].Value);
                lbMensaje.Visible = false;

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Id!=0)
            {
                AlumnoInscripcion asd = alumnoInscripcionLogica.GetOne(Id);
                asd.Nota = Convert.ToInt32(this.txtNota.Text);
                asd.Condicion = Convert.ToString(this.txtCondicion.Text);
                alumnoInscripcionLogica.Update(asd);
                MessageBox.Show("Nota Subida con exito");
            }
            
        }
    }
}
