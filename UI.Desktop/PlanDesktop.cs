using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Desktop;
using FastMember;

namespace UI.Desktop
{
    public partial class PlanDesktop :ApplicationForm
    {
        public Business.Entities.Plan planActual;
        public PlanDesktop()
        {
            InitializeComponent();
        }
        private DataTable enumerarEsp(List<Especialidad> especialidades)
        {
            
            DataTable items = new DataTable();
            using (var reader = ObjectReader.Create(especialidades)) { items.Load(reader); }
                
                return items;
        }
        public PlanDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            EspecialidadLogic e = new EspecialidadLogic();
            List<Especialidad> especialidades = e.GetAll();
            this.cbEspecialidades.DataSource = enumerarEsp(especialidades).DefaultView;
            this.cbEspecialidades.SelectedIndex = 0;

        }
        public PlanDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            PlanLogic p = new PlanLogic();

            planActual = p.GetOne(ID);
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
            EspecialidadLogic e = new EspecialidadLogic();
            //Trae todas las especialidades para cargar el combobox
            List<Especialidad> especialidades = e.GetAll();
            //Trae la especialidad del plan para seleccionarla en el combobox
            Especialidad esp = e.GetOne(planActual.IDEspecialidad);
            this.cbEspecialidades.DataSource = enumerarEsp(especialidades).DefaultView;
            this.cbEspecialidades.SelectedIndex = cbEspecialidades.FindStringExact(esp.Descripcion);
            this.txtID.Text = planActual.ID.ToString();
            this.txtDesc.Text = planActual.Descripcion;
            

        }

        public virtual void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                planActual = new Plan();
            }
            planActual.Descripcion = this.txtDesc.Text;
            planActual.IDEspecialidad = (int)this.cbEspecialidades.SelectedValue;

            switch (Modo)
            {
                case ModoForm.Alta:
                    planActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    planActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }
        public virtual void Eliminar() 
        {
            PlanLogic p = new PlanLogic();
            p.Delete(planActual.ID);
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
        public virtual void GuardarCambios()
        {
            PlanLogic p = new PlanLogic();
            MapearADatos();
            p.Save(planActual);
        }
        public virtual bool Validar()
        {
            if (string.IsNullOrWhiteSpace(this.txtDesc.Text))
            {
                Notificar("Error", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else { return true; }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
