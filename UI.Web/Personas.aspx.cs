using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;
using Microsoft.Ajax.Utilities;
using static Business.Entities.Persona;

namespace UI.Web
{
    public partial class Personas : System.Web.UI.Page
    {
        Personalogic _logic;

        public Persona Entity { get; set; }
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }
        public bool IsEntitySelected
        {
            get
            {
                return ((int)this.ViewState["SelectedID"] != 0);
            }

        }
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
        private Personalogic Logic
        {

            get
            {
                if (_logic == null)
                {
                    _logic = new Personalogic();
                }
                return _logic;
            }
        }
        public FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormModes"]; }
            set { this.ViewState["FormModes"] = value; }
        }

        private void LoadGrid()
        {
            List<Persona> personas = Logic.GetAll();
            this.gvPersona.DataSource = personas;
            this.gvPersona.DataBind();

        }

        protected void gvPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gvPersona.SelectedValue;
        }

        private void LoadForm(int id)
        {
            this.Entity = Logic.GetOne(id);
            tbNombre.Text = Entity.Nombre;
            tbApellido.Text = Entity.Apellido;
            tbEmail.Text = Entity.Email;
            tbLegajo.Text = Entity.Legajo.ToString();
            calendarFechaNac.SelectedDate = Entity.FechaNacimiento;
            calendarFechaNac.VisibleDate = Entity.FechaNacimiento;
            tbTel.Text = Entity.Telefono;
            tbDir.Text = Entity.Direccion;
            DDTipoPersona.ClearSelection();
            var tipo = Entity.TipoPersona;
            switch (tipo)
            {
                case TiposPersona.Alumno:
                    {
                        ListItem lista = DDTipoPersona.Items.FindByValue("1");
                        if (lista != null) { lista.Selected = true; }
                        break;
                    }
                case TiposPersona.Profesor:
                    {
                        ListItem lista = DDTipoPersona.Items.FindByValue("2");
                        if (lista != null) { lista.Selected = true; }
                        break;
                    }
                case TiposPersona.Administrativo:
                    {
                        ListItem lista = DDTipoPersona.Items.FindByValue("3");
                        if (lista != null) { lista.Selected = true; }
                        break;
                    }
            }

            //DDTipoPersona.SelectedItem.Value = tipo.ToString() ;
            //ListItem lista = DDTipoPersona.SelectedValue;
            //lista.Selected = true;
            planesDD.ClearSelection();
            ListItem listaPlan = planesDD.Items.FindByValue(this.Entity.IDPlan.ToString());
            if (listaPlan != null) { listaPlan.Selected = true; }

        }
        private void EnableForm(bool enable)
        {
            tbNombre.Enabled = enable;
            tbApellido.Enabled = enable;
            tbEmail.Enabled = enable;
            tbLegajo.Enabled = enable;
            calendarFechaNac.Enabled = enable;
            tbTel.Enabled = enable;
            tbDir.Enabled = enable;
        }
        private void LoadCombo()
        {
            PlanLogic pl = new PlanLogic();
            List<Plan> planes = pl.GetAll();
            this.planesDD.DataSource = planes;
            this.planesDD.DataBind();
        }
        protected void editarLinkButtonPersona_Click(object sender, EventArgs e)
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
        

        private void LoadEntity(Persona Entity)
        {
            Entity.Nombre = tbNombre.Text;
            Entity.Apellido = tbApellido.Text;
            Entity.Email= tbEmail.Text;
            Entity.Legajo = Convert.ToInt32( tbLegajo.Text);
            Entity.FechaNacimiento = calendarFechaNac.SelectedDate;
            Entity.Telefono = tbTel.Text;
            Entity.Direccion = tbDir.Text;
            switch(DDTipoPersona.SelectedItem.Value)
            {
                case "1": 
                    {
                        Entity.TipoPersona = TiposPersona.Alumno;
                        break; 
                    }
                case "2": 
                    {
                        Entity.TipoPersona = TiposPersona.Profesor;
                        break; 
                    }
                case "3": 
                    {
                        Entity.TipoPersona = TiposPersona.Administrativo;
                        break; 
                    }
            }
            Entity.IDPlan = Int32.Parse(this.planesDD.SelectedItem.Value);

        }
        private void SaveEntity(Persona persona)
        {
            this.Logic.Save(persona);
        }

        protected void eliminarLinkButtonMat_Click(object sender, EventArgs e)
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
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }

        protected void nuevoLinkButtonMat_Click(object sender, EventArgs e)
        {
            this.gridActionsPanel.Visible = false;
            this.formActionsPanel.Visible = true;
            this.formPanel.Visible = true;
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }
        private void ClearForm()
        {
            tbNombre.Text = string.Empty;
            tbApellido.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbLegajo.Text = string.Empty;
            calendarFechaNac.SelectedDate = DateTime.Now;
            tbTel.Text = string.Empty;
            tbDir.Text = string.Empty;
            DDTipoPersona.ClearSelection();
        }

        protected void aceptarLinkButtonMat_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Modificacion:

                    this.Entity = new Persona();
                    this.Entity.ID = (int)this.ViewState["SelectedID"];
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanel.Visible = true;
                    this.gvPersona.Visible = true;
                    this.formActionsPanel.Visible = false;
                    this.formPanel.Visible = false;
                    break;
                case FormModes.Alta:

                    this.Entity = new Persona();
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.gridActionsPanel.Visible = true;
                    this.gvPersona.Visible = true;
                    this.formPanel.Visible = false;
                    this.formActionsPanel.Visible = false;
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
            this.formPanel.Visible = false;
        }

        protected void cancelarLinkButtonMat_Click(object sender, EventArgs e)
        {
            this.ValidationSummary1.Enabled = false;
            this.formPanel.Visible = false;
            this.gridActionsPanel.Visible = true;
            this.SelectedID = 0;
            this.gvPersona.SelectedIndex = -1;
            this.gvPersona.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
                LoadCombo();
            }
        }
    }
}