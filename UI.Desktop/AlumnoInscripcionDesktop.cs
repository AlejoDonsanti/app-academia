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
    public partial class AlumnoInscripcionDesktop : ApplicationForm
    {
        public AlumnoInscripcion inscripcionActual;
        public AlumnoInscripcionDesktop()
        {
            InitializeComponent();
        }
        public AlumnoInscripcionDesktop(ModoForm modo, Persona personaActual) : this()
        {
            if (personaActual.TipoPersona == Persona.TiposPersona.Alumno)
            {
                this.txtCondicion.Visible = false;
                this.txtNota.Visible = false;
                this.lblCondicion.Visible = false;
                this.lblNota.Visible = false;
                this.cbAlumnos.Visible = false;
                this.label1.Visible = false;
            }

            Modo = modo;
            Personalogic p = new Personalogic();
            List<Persona> alumnos = p.GetAlumnos();
            this.cbAlumnos.DataSource = alumnos;

            CursoLogic c = new CursoLogic();
            List<Curso> cursos = c.GetCursosAñoActual();
            this.cbCurso.DataSource = cursos;
            this.btnAceptar.Text = "Guardar";
        }
        public AlumnoInscripcionDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            AlumnoInscripcionLogic ins = new AlumnoInscripcionLogic();
            inscripcionActual = ins.GetOne(ID);
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

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                inscripcionActual = new AlumnoInscripcion();
            }

            if (Session.Persona.TipoPersona==Persona.TiposPersona.Alumno)
            {
                inscripcionActual.IDAlumno =Session.Persona.ID;
            }
            else
            {
                inscripcionActual.IDAlumno = (int)this.cbAlumnos.SelectedValue;
            }
            
            inscripcionActual.IDCurso = (int)this.cbCurso.SelectedValue;
            inscripcionActual.Nota =  string.IsNullOrEmpty(this.txtNota.Text) ? default(int?) : int.Parse(this.txtNota.Text);
            if (Session.Persona.TipoPersona == Persona.TiposPersona.Alumno)
            {
                inscripcionActual.Condicion = "Inscripto";
            }
            else
            {
                inscripcionActual.Condicion = this.txtCondicion.Text;
            }
            switch (Modo)
            {
                case ModoForm.Alta:
                    inscripcionActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    inscripcionActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }

        public virtual void MapearDeDatos()
        {
            CursoLogic c = new CursoLogic();
            List<Curso> cursos = c.GetAll();
            Curso cursoInsc = c.GetOne(inscripcionActual.IDCurso);
            this.cbCurso.DataSource = cursos;
            this.cbCurso.SelectedIndex = cbCurso.FindStringExact(cursoInsc.IDMateria.ToString());
            this.txtCondicion.Text = inscripcionActual.Condicion.ToString();

            this.txtNota.Text = inscripcionActual.Nota.ToString();

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (Modo)
            {
                case ModoForm.Alta:
                case ModoForm.Modificacion:
                    {
                        if(Session.Persona.TipoPersona != Persona.TiposPersona.Alumno)
                        {
                            if (Validar())
                            {
                                GuardarCambios();
                                Close();
                            };
                        }
                        else
                        {
                            
                            GuardarCambios();
                            Close();
                        }
                    }
                    break;
                case ModoForm.Baja:
                    //Eliminar();
                    Close();
                    break;
                case ModoForm.Consulta:
                    Close();
                    break;
            }
        }

        public override bool Validar()
        {
            if (string.IsNullOrWhiteSpace(this.txtCondicion.Text))
            {
                Notificar("Error", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else { return true; }
        }

        public override void GuardarCambios()
        {
            AlumnoInscripcionLogic c = new AlumnoInscripcionLogic();
            MapearADatos();
            c.Save(inscripcionActual);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
