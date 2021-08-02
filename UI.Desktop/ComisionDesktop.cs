using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FastMember;

namespace UI.Desktop
{
    public partial class ComisionDesktop : ApplicationForm
    {
        public Comision comisionActual;
        public ComisionDesktop()
        {
            InitializeComponent();
        }

        private DataTable enumerarPlan(List<Plan> planes)
        {
            DataTable items = new DataTable();
            using (var reader = ObjectReader.Create(planes)) { items.Load(reader); }
            return items;
        }

        public ComisionDesktop(ModoForm modo):this()
        {
            Modo = modo;
            PlanLogic p = new PlanLogic();
            List<Plan> planes = p.GetAll();
            this.cbPlanes.DataSource = enumerarPlan(planes);
            this.cbPlanes.SelectedIndex = 0;
            this.btnAceptar.Text = "Guardar";
        }
        public ComisionDesktop(int ID,ModoForm modo) : this()
        {
            Modo = modo;
            ComisionLogic c = new ComisionLogic();
            comisionActual = c.GetOne(ID);
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
        public virtual void MapearDeDatos() 
        {
            PlanLogic p = new PlanLogic();
            List<Plan> planes = p.GetAll();
            Plan plan = p.GetOne(comisionActual.IDPlan);
            this.cbPlanes.DataSource = enumerarPlan(planes).DefaultView;
            this.cbPlanes.SelectedIndex = cbPlanes.FindStringExact(plan.Descripcion);
            this.txtID.Text = comisionActual.ID.ToString();
            this.txtDesc.Text = comisionActual.Descripcion;
            this.txtAnioEsp.Text = comisionActual.AnioEspecialidad.ToString();
        }
        public virtual void MapearADatos()
        {
            if(Modo == ModoForm.Alta)
            {
                comisionActual = new Comision();
            }
            comisionActual.Descripcion = this.txtDesc.Text;
            comisionActual.AnioEspecialidad = int.Parse(this.txtAnioEsp.Text);
            comisionActual.IDPlan = (int)this.cbPlanes.SelectedValue;
            switch (Modo)
            {
                case ModoForm.Alta:
                    comisionActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    comisionActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }
        public virtual bool Validar()
        {
            if (string.IsNullOrWhiteSpace(this.txtDesc.Text) && string.IsNullOrWhiteSpace(this.txtAnioEsp.Text))
            {
                Notificar("Error", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else { return true; }
        }
        public virtual void Eliminar()
        {
            ComisionLogic c = new ComisionLogic();
            c.Delete(comisionActual.ID);
        }
        public virtual void GuardarCambios()
        {
            ComisionLogic c = new ComisionLogic();
            MapearADatos();
            c.Save(comisionActual);
        }
        public void Notificar(string titulo, string mensaje, MessageBoxButtons
botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones,
        MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            switch (Modo)
            {
                case ModoForm.Alta:
                case ModoForm.Modificacion:
                    if (Validar())
                    {
                        GuardarCambios();
                        Close();
                    };
                    break;
                case ModoForm.Baja:
                    Eliminar();
                    Close();
                    break;
                case ModoForm.Consulta:
                    Close();
                    break;

            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAnioEsp_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtAnioEsp.Text, "[^0-9]"))
            {
                Notificar("Comision", "Ingrese solo caracteres numericos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnioEsp.Text = txtAnioEsp.Text.Remove(txtAnioEsp.Text.Length - 1);
            }
        }
    }
}
