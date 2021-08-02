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
    public partial class InscripcionAlumno : System.Web.UI.Page
    {
        CursoLogic cl;
        AlumnoInscripcionLogic il;
        public Usuario usuarioSession;
        public Persona alumno;
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public AlumnoInscripcion Entity2 { get; set; }
        public Curso Entity { get; set; }

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

        public FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormModes"]; }
            set { this.ViewState["FormModes"] = value; }
        }
        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SelectedID = (int)this.gridView.SelectedValue;
            this.SelectedID = (int)this.gridView.SelectedDataKey.Value;
        }



        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            this.ValidationSummary1.Enabled = false;
            this.SelectedID = 0;
            this.gridView.SelectedIndex = -1;
            this.gridPanel.Visible = true;
        }


        private void LoadGrid()
        {
            cl = new CursoLogic();
            ComisionLogic Coml = new ComisionLogic();
            MateriaLogic ml = new MateriaLogic();

            List<Curso> cursos = cl.GetAll();
            List<Comision> comisiones = Coml.GetAll();
            List<Materia> materias = ml.GetAll();
            var query = from c in cursos
                        join cm in comisiones
                        on c.IDComision equals cm.ID
                        join ma in materias
                        on c.IDMateria equals ma.ID
                        select new
                        {
                            ID = c.ID,
                            Descripcion = c.Descripcion,
                            AnioCalendario = c.AnioCalendario,
                            Comision = cm.Descripcion,
                            Materia = ma.Descripcion,
                            Cupo = c.Cupo
                        };
            this.gridView.DataSource = query.ToList();
            this.gridView.DataBind();


        }



        protected void Page_LoadInscripcion(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            
                
            }

        }
        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            if (IsEntitySelected)
            {
                Personalogic pl = new Personalogic();
                usuarioSession = (Usuario)Session["User"];
                alumno = pl.GetOne(Convert.ToInt32(usuarioSession.IDPersona));
                cl = new CursoLogic();
                this.Entity2 = new AlumnoInscripcion();
                this.Entity2.State = BusinessEntity.States.New;
                Curso c = cl.GetOne(this.SelectedID);
                if (c.Cupo > 0)
                {
                    if (alumno.TipoPersona == Persona.TiposPersona.Alumno)
                    {
                        Entity2.IDAlumno = alumno.ID;
                    }
                    il = new AlumnoInscripcionLogic();
                    this.LoadEntity(this.Entity2, c.ID);
                    List<AlumnoInscripcion> inscripciones = il.GetAll();
                    bool val = true;
                    foreach (var ins in inscripciones)
                    {
                        if (ins.IDAlumno == Entity2.IDAlumno && ins.IDCurso == Entity2.IDCurso)
                        {
                            //El alumno ya esta anotado a este curso
                            val = false;

                        }
                    }
                    if (val)
                    {
                        il.Save(Entity2);
                        c.Cupo = c.Cupo - 1;
                        c.State = BusinessEntity.States.Modified;
                        cl.Save(c);
                        LoadGrid();
                        string script = "alert(\"Se ha inscripto correctamente\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                    }
                    else
                    {
                        string script = "alert(\"Ya se encuentra inscripto a este curso\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                    }



                }
                else
                {
                    string script = "alert(\"Curso lleno\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                }
            }
            else
            {
                //SELECCIONA UN CURSO
            }

        }
        private void LoadEntity(AlumnoInscripcion ai, int idcurso)
        {
            ai.Condicion = "Inscripto";
            ai.Nota = default;
            ai.IDCurso = idcurso;
        }


    }
}