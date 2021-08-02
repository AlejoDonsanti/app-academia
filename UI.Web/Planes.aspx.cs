using System;
using System.Web.DynamicData;
using System.Web.UI;
using Business.Entities;
using Business.Logic;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web;

namespace UI.Web
{
    public partial class Planes : System.Web.UI.Page
    {
        PlanLogic _logic;
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public Plan Entity { get; set; }

        public int SelectedID
        {
            get
            {
                if (this.ViewState["SlectedID"] != null)
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



        private PlanLogic Logic
        {

            get
            {
                if (_logic == null)
                {
                    _logic = new PlanLogic();
                }
                return _logic;
            }
        }

        private void LoadGrid()
        {
            PlanLogic pl = new PlanLogic();
            EspecialidadLogic el = new EspecialidadLogic();
            List<Plan> planes = pl.GetAll();
            List<Especialidad> especialidades = el.GetAll();
            var query =
                    from p in planes
                    join e in especialidades
                    on p.IDEspecialidad equals e.ID
                    select new
                    {
                        ID = p.ID,
                        Descripcion = p.Descripcion,
                        Especialidad = e.Descripcion
                    };
            this.gridViewPla.DataSource = query.ToList();
            //this.gridViewPla.DataSource = Logic.GetAll();
            this.gridViewPla.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }

        }

        protected void gridViewPla_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridViewPla.SelectedValue;
        }
        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.especialidadTextBox.Text = this.Entity.IDEspecialidad.ToString();
        }

        protected void editarLinkButtonPla_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelPla.Visible = false;
                this.formPanelPla.Visible = true;
                this.FormMode = FormModes.Modificacion;
                LoadForm((int)this.ViewState["SelectedID"]);
                EnableForm(true);
            }
        }

        private void LoadEntitiy(Plan plan)
        {
            plan.Descripcion = this.descripcionTextBox.Text;
            plan.IDEspecialidad = Int32.Parse(this.especialidadTextBox.Text);

        }

        private void SaveEntity(Plan plan)
        {
            this.Logic.Save(plan);
        }

        protected void aceptarLinkButtonPla_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Modificacion:

                    this.Entity = new Plan();
                    this.Entity.ID = (int)this.ViewState["SelectedID"];
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelPla.Visible = true;
                    break;
                case FormModes.Alta:

                    this.Entity = new Plan();
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelPla.Visible = true;
                    break;
                case FormModes.Baja:
                    this.DeleteEntity((int)this.ViewState["SelectedID"]);
                    this.LoadGrid();
                    break;
                default:
                    break;
            }
            this.formPanelPla.Visible = false;
        }

        protected void cancelarLinkButtonPla_Click(object sender, EventArgs e)
        {
            this.ValidationSummaryPla1.Enabled = false;
            this.formPanelPla.Visible = false;
            this.gridActionsPanelPla.Visible = true;
            this.SelectedID = 0;
            this.gridViewPla.SelectedIndex = -1;
            this.gridPanelPla.Visible = true;
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.especialidadTextBox.Enabled = enable;
        }

        protected void eliminarLinkButtonPla_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelPla.Visible = false;
                this.formPanelPla.Visible = true;
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm((int)this.ViewState["SelectedID"]);
            }
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }

        protected void nuevoLinkButtonPla_Click(object sender, EventArgs e)
        {
            this.gridActionsPanelPla.Visible = false;
            this.formPanelPla.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridPanelPla.Visible = false;
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.especialidadTextBox.Text = string.Empty;
        }


        private void ExportGridToPDF()
        {
            gridViewPla.Columns[2].Visible = false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReportePlanes.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridViewPla.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.AddTitle("Planes");
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            gridViewPla.AllowPaging = true;
            gridViewPla.Columns[2].Visible = true;
            gridViewPla.DataBind();
        }


        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportGridToPDF();
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            return;
        }
    }
}