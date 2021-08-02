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
    public partial class MateriaDesktop : ApplicationForm
    {
        public Materia materiaActual;
        public MateriaDesktop()
        {
            InitializeComponent();
        }

        private DataTable enumerarPlan(List<Plan> planes)
        {
            DataTable items = new DataTable();
            using (var reader = ObjectReader.Create(planes)){ items.Load(reader); }
            return items;
        }
        public MateriaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            PlanLogic p = new PlanLogic();
            List<Plan> planes = p.GetAll();
            this.cbPlanes.DataSource = enumerarPlan(planes);
            this.cbPlanes.SelectedIndex = 0;
            this.btnAceptar.Text = "Guardar";

        }

        public MateriaDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            MateriaLogic m = new MateriaLogic();
            materiaActual = m.GetOne(ID);
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
            Plan plan = p.GetOne(materiaActual.IDPlan);
            this.cbPlanes.DataSource = enumerarPlan(planes).DefaultView;
            this.cbPlanes.SelectedIndex = cbPlanes.FindStringExact(plan.Descripcion);
            this.txtID.Text = materiaActual.ID.ToString();
            this.txtDesc.Text = materiaActual.Descripcion;
            this.txtHSS.Text = materiaActual.HSSemanales.ToString();
            this.txtHST.Text = materiaActual.HSTotales.ToString();
        }
        public virtual void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                materiaActual = new Materia();
            }
            materiaActual.Descripcion = this.txtDesc.Text;
            materiaActual.HSSemanales = int.Parse(this.txtHSS.Text);
            materiaActual.HSTotales = int.Parse(this.txtHST.Text);
            materiaActual.IDPlan = (int)this.cbPlanes.SelectedValue;

            switch (Modo)
            {
                case ModoForm.Alta:
                    materiaActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    materiaActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }
        public virtual bool Validar()
        {
            if (string.IsNullOrWhiteSpace(this.txtDesc.Text) && string.IsNullOrWhiteSpace(this.txtHSS.Text) && string.IsNullOrWhiteSpace(this.txtHST.Text))
            {
                Notificar("Error", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else { return true; }
        }
        public virtual void Eliminar()
        {
            MateriaLogic m = new MateriaLogic();
            m.Delete(materiaActual.ID);
        }
        public virtual void GuardarCambios()
        {
            MateriaLogic m = new MateriaLogic();
            MapearADatos();
            m.Save(materiaActual);
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

        private void txtHSS_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtHSS.Text, "[^0-9]"))
            {
                Notificar("Materia","Ingrese solo caracteres numericos",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtHSS.Text = txtHSS.Text.Remove(txtHSS.Text.Length - 1);
            }
        }

        private void txtHST_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtHST.Text, "[^0-9]"))
            {
                Notificar("Materia", "Ingrese solo caracteres numericos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHST.Text = txtHST.Text.Remove(txtHST.Text.Length - 1);
            }
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
    }
}
