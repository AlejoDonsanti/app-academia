using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class CargarNotas : System.Web.UI.Page
    {
        CursoLogic cl;
        AlumnoInscripcionLogic al;
        Personalogic pl;
        DocenteCursoLogic dcl;
        public AlumnoInscripcion a;
        public int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }

        public bool IsEntitySelected
        {
            get
            {
                return ((int)this.ViewState["SelectedID"] != 0);
            }

        }

        public void LoadCombo()
        {
            dcl = new DocenteCursoLogic();
            cl = new CursoLogic();
            Usuario user = (Usuario)Session["user"];
            List<DocenteCurso> docenteCursos = dcl.GetCursosPorDocente(Convert.ToInt32(user.IDPersona));
            List<Curso> cursos = cl.GetAll();
            var query = from d in docenteCursos
                        join c in cursos
                        on d.IDCurso equals c.ID
                        select new
                        {
                            ID = c.ID,
                            Descripcion = c.Descripcion
                        };
            ddCursos.DataSource = query.ToList();
            ddCursos.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { LoadCombo(); }
            
        }
        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SelectedID = (int)this.gridView.SelectedValue;
            this.SelectedID = (int)this.gvAlumnosInscriptos.SelectedDataKey.Value;
            
        }
        private void LoadForm(int id)
        {
            al = new AlumnoInscripcionLogic();
            a = new AlumnoInscripcion();
            a = al.GetOne(id);
            this.tbNota.Text = a.Nota.ToString();
            this.tbCondicion.Text = a.Condicion;

        }
        private void LoadEntity(int id)
        {
            al = new AlumnoInscripcionLogic();
            a = new AlumnoInscripcion();
            a = al.GetOne(id);
            a.Nota = Convert.ToInt32(tbNota.Text);
            a.Condicion = tbCondicion.Text;
        }

        //protected void ddCursos_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int ID = Convert.ToInt32(ddCursos.SelectedValue);
        //    al = new AlumnoInscripcionLogic();
        //    pl = new Personalogic();
        //    List<Persona> personas = pl.GetAll();
        //    List<AlumnoInscripcion> alumnoInscripcions = al.GetAll();
        //    var query = from a in alumnoInscripcions
        //                join p in personas on a.IDAlumno equals p.ID
        //                where a.IDCurso == 2
        //                select new
        //                {
        //                    Alumno = p.Apellido +", "+ p.Nombre,
        //                    Condicion = a.Condicion,
        //                    Nota = a.Nota
        //                };
        //    gvAlumnosInscriptos.DataSource = query.ToList();
        //    gvAlumnosInscriptos.DataBind();
        //    gvAlumnosInscriptos.Visible = true;
        //}
        private void LoadGrid()
        {
            int ID = Convert.ToInt32(ddCursos.SelectedValue);
            al = new AlumnoInscripcionLogic();
            pl = new Personalogic();
            List<Persona> personas = pl.GetAll();
            List<AlumnoInscripcion> alumnoInscripcions = al.GetAll();
            var query = from a in alumnoInscripcions
                        join p in personas on a.IDAlumno equals p.ID
                        where a.IDCurso == ID
                        select new
                        {
                            ID = a.ID,
                            Alumno = p.Apellido + ", " + p.Nombre,
                            Condicion = a.Condicion,
                            Nota = a.Nota
                        };
            gvAlumnosInscriptos.DataSource = null;
            gvAlumnosInscriptos.DataSource = query.ToList();
            gvAlumnosInscriptos.DataBind();
            gvAlumnosInscriptos.Visible = true;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadGrid();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                LoadForm(SelectedID);
            }
        }
        private void ClearForm()
        {
            tbCondicion.Text = null;
            tbNota.Text = null;
        }
        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            LoadEntity(this.SelectedID);
            a.State = BusinessEntity.States.Modified;
            al = new AlumnoInscripcionLogic();
            al.Save(a);
            LoadGrid();
            ClearForm();

        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}