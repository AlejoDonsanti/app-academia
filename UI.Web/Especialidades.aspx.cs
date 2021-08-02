using System;
using System.Web.DynamicData;
using System.Web.UI;
using Business.Entities;
using Business.Logic;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32;

namespace UI.Web
{
    public partial class Especialidades : System.Web.UI.Page
    {
        EspecialidadLogic _logic;
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public Especialidad Entity { get; set; }

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



        private EspecialidadLogic Logic
        {

            get
            {
                if (_logic == null)
                {
                    _logic = new EspecialidadLogic();
                }
                return _logic;
            }
        }

        private void LoadGrid()
        {
            this.gridViewEsp.DataSource = Logic.GetAll();
            this.gridViewEsp.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }

        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridViewEsp.SelectedValue;
        }
        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
        }

        protected void editarLinkButtonEsp_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelEsp.Visible = false;
                this.formPanelEsp.Visible = true;
                this.FormMode = FormModes.Modificacion;
                LoadForm((int)this.ViewState["SelectedID"]);
                EnableForm(true);
            }
        }

        private void LoadEntitiy(Especialidad especialidad)
        {
            especialidad.Descripcion = this.descripcionTextBox.Text;

        }

        private void SaveEntity(Especialidad especialidad)
        {
            this.Logic.Save(especialidad);
        }

        protected void aceptarLinkButtonEsp_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Modificacion:

                    this.Entity = new Especialidad();
                    this.Entity.ID = (int)this.ViewState["SelectedID"];
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelEsp.Visible = true;
                    break;
                case FormModes.Alta:

                    this.Entity = new Especialidad();
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelEsp.Visible = true;
                    break;
                case FormModes.Baja:
                    this.DeleteEntity((int)this.ViewState["SelectedID"]);
                    this.LoadGrid();
                    break;
                default:
                    break;
            }
            this.formPanelEsp.Visible = false;
        }

        protected void cancelarLinkButtonEsp_Click(object sender, EventArgs e)
        {
            this.ValidationSummaryEsp1.Enabled = false;
            this.formPanelEsp.Visible = false;
            this.gridActionsPanelEsp.Visible = true;
            this.SelectedID = 0;
            this.gridViewEsp.SelectedIndex = -1;
            this.gridPanelEsp.Visible = true;
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
        }

        protected void eliminarLinkButtonEsp_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelEsp.Visible = false;
                this.formPanelEsp.Visible = true;
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm((int)this.ViewState["SelectedID"]);
            }
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }

        protected void nuevoLinkButtonEsp_Click(object sender, EventArgs e)
        {
            this.gridActionsPanelEsp.Visible = false;
            this.formPanelEsp.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridPanelEsp.Visible = false;
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
        }

    }
}