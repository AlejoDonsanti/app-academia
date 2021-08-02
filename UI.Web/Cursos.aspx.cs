using System;
using System.Web.DynamicData;
using System.Web.UI;
using Business.Entities;
using Business.Logic;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace UI.Web
{
    public partial class Cursos : System.Web.UI.Page
    {
        CursoLogic cl;

        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
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

        private void LoadForm(int id)
        {
            cl = new CursoLogic();
            this.Entity = this.cl.GetOne(id);
            this.descTB.Text = this.Entity.Descripcion;
            this.cupoTB.Text = this.Entity.Cupo.ToString();
            this.anioTB.Text = this.Entity.AnioCalendario.ToString();
            this.LoadCombo();
            materiaDD.ClearSelection(); comisionDD.ClearSelection();
            System.Web.UI.WebControls.ListItem listacomi = comisionDD.Items.FindByValue(this.Entity.IDComision.ToString());
            if (listacomi != null) { comisionDD.ClearSelection(); listacomi.Selected = true; }

            System.Web.UI.WebControls.ListItem listamateria = materiaDD.Items.FindByValue(this.Entity.IDMateria.ToString());
            if (listamateria != null) { materiaDD.ClearSelection(); listamateria.Selected = true; }
            
        }
        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanel.Visible = false;
                this.formActionsPanel.Visible = true;
                this.formPanel.Visible = true;
                this.FormMode = FormModes.Modificacion;
                LoadForm((int)this.ViewState["SelectedID"]);
                EnableForm(true);
            }
        }
        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.gridActionsPanel.Visible = false;
            this.formActionsPanel.Visible = true;
            this.formPanel.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            //this.gridPanel.Visible = false;
        }
        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanel.Visible = false;
                this.formActionsPanel.Visible = true;
                this.formPanel.Visible = true;
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm((int)this.ViewState["SelectedID"]);
            }
        }
        private void ClearForm()
        {
            this.descTB.Text = string.Empty;
            this.anioTB.Text = string.Empty;
            this.cupoTB.Text = string.Empty;
            this.comisionDD.ClearSelection();
            this.materiaDD.ClearSelection();
        }
        private void EnableForm(bool enable) 
        {
            this.anioTB.Enabled = enable;
            this.cupoTB.Enabled = enable;
            this.descTB.Enabled = enable;
            this.comisionDD.Enabled = enable;
            this.materiaDD.Enabled = enable;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            this.ValidationSummary1.Enabled = false;
            this.formPanel.Visible = false;
            this.gridActionsPanel.Visible = true;
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

        private void LoadCombo() 
        {
            ComisionLogic Coml = new ComisionLogic();
            MateriaLogic ml = new MateriaLogic();
            List<Comision> comisiones = Coml.GetAll();
            List<Materia> materias = ml.GetAll();
            this.comisionDD.DataSource = comisiones;
            this.comisionDD.DataBind();
            this.materiaDD.DataSource = materias;
            this.materiaDD.DataBind();
        }

        protected void Page_LoadCurso(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
                LoadCombo();
            }

        }

        private void LoadEntitiy(Curso curso) 
        {
            curso.Descripcion = this.descTB.Text;
            curso.Cupo = int.Parse( this.cupoTB.Text);
            curso.AnioCalendario = int.Parse(this.anioTB.Text);
            curso.IDComision = int.Parse(this.comisionDD.SelectedItem.Value);
            curso.IDMateria = int.Parse(this.materiaDD.SelectedItem.Value);
        }
        private void SaveEntity(Curso curso) 
        {
            cl = new CursoLogic();
            cl.Save(curso);
        }
        private void DeleteEntity(int ID) 
        {
            cl = new CursoLogic();
            cl.Delete(ID);
        }
        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Modificacion:

                    this.Entity = new Curso();
                    this.Entity.ID = (int)this.ViewState["SelectedID"];
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanel.Visible = true;
                    this.formActionsPanel.Visible = false;
                    this.formPanel.Visible = false;

                    break;
                case FormModes.Alta:

                    this.Entity = new Curso();
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanel.Visible = true;
                    this.gridPanel.Visible = true;
                    this.gridView.Visible = true;
                    this.formActionsPanel.Visible = false;
                    this.formPanel.Visible = false;
                    break;
                case FormModes.Baja:
                    this.DeleteEntity((int)this.ViewState["SelectedID"]);
                    this.LoadGrid();
                    this.gridActionsPanel.Visible = true;
                    this.formActionsPanel.Visible = false;
                    this.formPanel.Visible = false;
                    break;
                default:
                    break;
            }
            
            
        }

        private void ExportGridToPDF()
        {
            gridView.Columns[6].Visible = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteCursos.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridView.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.AddTitle("Cursos");
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gridView.AllowPaging = true;
            gridView.Columns[6].Visible = true;
            gridView.DataBind();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            return;
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }
    }
}