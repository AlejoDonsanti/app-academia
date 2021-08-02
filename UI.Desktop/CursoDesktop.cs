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
    public partial class CursoDesktop : ApplicationForm
    {
        public Curso cursoActual;
        public CursoDesktop()
        {
            InitializeComponent();
        }
        public CursoDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MateriaLogic m = new MateriaLogic();
            List<Materia> materias = m.GetAll();
            this.cbMaterias.DataSource = materias;


            ComisionLogic c = new ComisionLogic();
            List<Comision> comisiones = c.GetAll();
            this.cbComisiones.DataSource = comisiones;
            this.btnAceptar.Text = "Guardar";
        }

        public CursoDesktop(int ID,ModoForm modo) : this()
        {
            Modo = modo;
            CursoLogic c = new CursoLogic();
            cursoActual = c.GetOne(ID);
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

        //private DataTable enumerarMateria(List<Materia> materias)
        //{
        //    DataTable items = new DataTable();
        //    using (var reader = ObjectReader.Create(materias)) { items.Load(reader); }
        //    return items;
        //}
        //private DataTable enumerarComision(List<Comision> comisiones)
        //{
        //    DataTable items = new DataTable();
        //    using (var reader = ObjectReader.Create(comisiones)) { items.Load(reader); }
        //    return items;
        //}

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                cursoActual = new Curso();
            }
            cursoActual.Cupo = int.Parse(this.txtCupo.Text);
            cursoActual.IDComision = (int)this.cbComisiones.SelectedValue;
            cursoActual.IDMateria = (int)this.cbMaterias.SelectedValue;
            cursoActual.AnioCalendario = int.Parse(this.txtAño.Text);
            switch (Modo)
            {
                case ModoForm.Alta:
                    cursoActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    cursoActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }

        public virtual void MapearDeDatos()
        {
            MateriaLogic m = new MateriaLogic();
            List<Materia> materias = m.GetAll();
            Materia materia = m.GetOne(cursoActual.IDMateria);
            this.cbMaterias.DataSource = materias;
            //this.cbMaterias.DataSource = enumerarPlan(materias).DefaultView;
            this.cbMaterias.SelectedIndex = cbMaterias.FindStringExact(materia.Descripcion);

            ComisionLogic c = new ComisionLogic();
            List<Comision> comisiones = c.GetAll();
            Comision comision = c.GetOne(cursoActual.IDComision);
            this.cbComisiones.DataSource = comisiones;
            //this.cbComisiones.DataSource = enumerarComision(comisiones).DefaultView;
            this.cbComisiones.SelectedIndex = cbComisiones.FindStringExact(comision.Descripcion);

            this.txtCupo.Text = cursoActual.Cupo.ToString();
            this.txtAño.Text = cursoActual.AnioCalendario.ToString();
            this.txtID.Text = cursoActual.ID.ToString();

        }
        public virtual void Eliminar()
        {
            CursoLogic c = new CursoLogic();
            try
            {
                c.Delete(cursoActual.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public virtual void GuardarCambios()
        {
            CursoLogic c= new CursoLogic();
            MapearADatos();
            c.Save(cursoActual);
        }
        public void Notificar(string titulo, string mensaje, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }
        public void Notificar(string mensaje, MessageBoxButtons botones,MessageBoxIcon icono)
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
        public override bool Validar()
        {
            if (string.IsNullOrWhiteSpace(this.txtCupo.Text))
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
    }
}
