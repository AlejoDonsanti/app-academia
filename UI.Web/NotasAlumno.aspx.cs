using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;


namespace UI.Web
{
    public partial class NotasAlumno : System.Web.UI.Page
    {
        CursoLogic cl;
        AlumnoInscripcionLogic al;


        private void LoadGrid()
        {
            cl = new CursoLogic();
            al = new AlumnoInscripcionLogic();
            List<Curso> cursos = cl.GetAll();
            List < AlumnoInscripcion > alumnoInscripcions = al.GetAll();
            Usuario user = (Usuario)Session["User"];
            var query = from a in alumnoInscripcions
                        join c in cursos
                        on a.IDCurso equals c.ID
                        where a.IDAlumno == user.IDPersona
                        select new
                        {
                            Curso = c.Descripcion,
                            Nota = a.Nota,
                            Condicion = a.Condicion
                        };
            gvNotasAlumno.DataSource = query.ToList();
            gvNotasAlumno.DataBind();
            //var query = alumnoInscripcions.Where(a => a.IDAlumno == user.IDPersona);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}