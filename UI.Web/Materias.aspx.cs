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

namespace UI.Web
{
    public partial class Materias : System.Web.UI.Page
    {
        MateriaLogic _logic;
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public Materia Entity { get; set; }

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



        private MateriaLogic Logic
        {

            get
            {
                if (_logic == null)
                {
                    _logic = new MateriaLogic();
                }
                return _logic;
            }
        }

        private void LoadGrid()
        {
            MateriaLogic ml = new MateriaLogic();
            PlanLogic pl = new PlanLogic();
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
                        HsSemanales = m.HSSemanales,
                        HsTotales = m.HSTotales,
                        Plan = p.Descripcion
                    };
            this.gridViewMat.DataSource = query.ToList();
            //this.gridViewMat.DataSource = Logic.GetAll();
            this.gridViewMat.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
                LoadCombo();
            }

        }

        protected void gridViewMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridViewMat.SelectedValue;
        }
        private void LoadCombo()
        {
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = pl.GetAll();
            this.planesDD.DataSource = planes;
            this.planesDD.DataBind();
        }
        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.hsemanalesTextBox.Text = this.Entity.HSSemanales.ToString();
            this.htotalesTextBox.Text = this.Entity.HSTotales.ToString();
           
            planesDD.ClearSelection();
            ListItem listaPlan = planesDD.Items.FindByValue(this.Entity.IDPlan.ToString());
            if (listaPlan != null) { listaPlan.Selected = true; }
        }

        protected void editarLinkButtonMat_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelMat.Visible = false;
                this.formPanelMat.Visible = true;
                this.FormMode = FormModes.Modificacion;
                LoadForm((int)this.ViewState["SelectedID"]);
                EnableForm(true);
            }
        }

        private void LoadEntitiy(Materia materia)
        {
            materia.Descripcion = this.descripcionTextBox.Text;
            materia.HSSemanales = Int32.Parse(this.hsemanalesTextBox.Text);
            materia.HSTotales = Int32.Parse(this.htotalesTextBox.Text);
            
            materia.IDPlan = Int32.Parse(this.planesDD.SelectedItem.Value);


        }

        private void SaveEntity(Materia materia)
        {
            this.Logic.Save(materia);
        }

        protected void aceptarLinkButtonMat_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Modificacion:

                    this.Entity = new Materia();
                    this.Entity.ID = (int)this.ViewState["SelectedID"];
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelMat.Visible = true;
                    this.gridPanelMat.Visible = true;
                    this.formActionsPanelMat.Visible = false;
                    this.formPanelMat.Visible = false;
                    break;
                case FormModes.Alta:

                    this.Entity = new Materia();
                    this.LoadEntitiy(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanelMat.Visible = true;
                    this.gridPanelMat.Visible = true;
                    this.formPanelMat.Visible = false;
                    this.formActionsPanelMat.Visible = false;
                    break;
                case FormModes.Baja:
                    this.DeleteEntity((int)this.ViewState["SelectedID"]);
                    this.LoadGrid();
                    this.gridActionsPanelMat.Visible = true;
                    this.formActionsPanelMat.Visible = false;
                    this.formPanelMat.Visible = false;

                    break;
                default:
                    break;
            }
            this.formPanelMat.Visible = false;
        }

        protected void cancelarLinkButtonMat_Click(object sender, EventArgs e)
        {
            this.ValidationSummaryMat1.Enabled = false;
            this.formPanelMat.Visible = false;
            this.gridActionsPanelMat.Visible = true;
            this.SelectedID = 0;
            this.gridViewMat.SelectedIndex = -1;
            this.gridPanelMat.Visible = true;
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.hsemanalesTextBox.Enabled = enable;
            this.htotalesTextBox.Enabled = enable;
            
        }

        protected void eliminarLinkButtonMat_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.gridActionsPanelMat.Visible = false;
                this.formActionsPanelMat.Visible = true;
                this.formPanelMat.Visible = true;
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm((int)this.ViewState["SelectedID"]);
            }
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }

        protected void nuevoLinkButtonMat_Click(object sender, EventArgs e)
        {
            this.gridActionsPanelMat.Visible = false;
            this.formPanelMat.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
            this.gridPanelMat.Visible = false;
        }
        private void ClearForm()
        {
            this.descripcionTextBox.Text = string.Empty;
            this.hsemanalesTextBox.Text = string.Empty;
            this.htotalesTextBox.Text = string.Empty;
            
        }

    }
}