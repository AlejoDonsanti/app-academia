using System;
using System.Web.DynamicData;
using System.Web.UI;
using Business.Entities;
using Business.Logic;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace UI.Web
{
    public partial class Comisiones : System.Web.UI.Page
    {
        ComisionLogic _logic;
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public Comision Entity { get; set; }

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



        private ComisionLogic Logic
        {

            get
            {
                if (_logic == null)
                {
                    _logic = new ComisionLogic();
                }
                return _logic;
            }
        }

        private void LoadGrid()
        {
            ComisionLogic cl = new ComisionLogic();
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = pl.GetAll();
            List<Comision> comisiones = cl.GetAll();
            var query = from c in comisiones
                        join p in planes
                        on c.IDPlan equals p.ID
                        select new
                        {
                            ID = c.ID,
                            Descripcion = c.Descripcion,
                            AnioEspecialidad = c.AnioEspecialidad,
                            Plan = p.Descripcion
                        };
            this.gridViewCom.DataSource = query.ToList();
            //this.gridViewCom.DataSource = Logic.GetAll();
            this.gridViewCom.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }

        }

        protected void gridViewCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridViewCom.SelectedValue;
        }
        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.anioTextBox.Text = this.Entity.AnioEspecialidad.ToString();
            this.planTextBox.Text = this.Entity.IDPlan.ToString();
        }

        protected void editarLinkButtonCom_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelCom.Visible = false;
                this.formPanelCom.Visible = true;
                this.FormMode = FormModes.Modificacion;
                LoadForm((int)this.ViewState["SelectedID"]);
                EnableForm(true);
            }
        }

        private void LoadEntitiy(Comision comision)
        {
            comision.Descripcion = this.descripcionTextBox.Text;
            comision.AnioEspecialidad = Int32.Parse(this.anioTextBox.Text);
            comision.IDPlan = Int32.Parse(this.planTextBox.Text);
           
        }

        private void SaveEntity(Comision comision)
        {
            this.Logic.Save(comision);
        }

        protected void aceptarLinkButtonCom_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Modificacion:

                    this.Entity = new Comision();
                    this.Entity.ID = (int)this.ViewState["SelectedID"];
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelCom.Visible = true;
                    break;
                case FormModes.Alta:

                    this.Entity = new Comision();
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelCom.Visible = true;
                    break;
                case FormModes.Baja:
                    this.DeleteEntity((int)this.ViewState["SelectedID"]);
                    this.LoadGrid();
                    break;
                default:
                    break;
            }
            this.formPanelCom.Visible = false;
        }

        protected void cancelarLinkButtonCom_Click(object sender, EventArgs e)
        {
            this.ValidationSummaryCom1.Enabled = false;
            this.formPanelCom.Visible = false;
            this.gridActionsPanelCom.Visible = true;
            this.SelectedID = 0;
            this.gridViewCom.SelectedIndex = -1;
            this.gridPanelCom.Visible = true;
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.anioTextBox.Enabled = enable;
            this.planTextBox.Enabled = enable;
        }

        protected void eliminarLinkButtonCom_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelCom.Visible = false;
                this.formPanelCom.Visible = true;
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm((int)this.ViewState["SelectedID"]);
            }
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }

        protected void nuevoLinkButtonCom_Click(object sender, EventArgs e)
        {
            this.gridActionsPanelCom.Visible = false;
            this.formPanelCom.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridPanelCom.Visible = false;
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.anioTextBox.Text = string.Empty;
            this.planTextBox.Text = string.Empty;
        }

    }
}