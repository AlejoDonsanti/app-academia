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
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class EspecialidadesDesktop : ApplicationForm
    {
        public Especialidad espActual;
        public EspecialidadesDesktop()
        {
            InitializeComponent();
        }
        public EspecialidadesDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public EspecialidadesDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            EspecialidadLogic e = new EspecialidadLogic();
            espActual = e.GetOne(ID);
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
            this.txtID.Text = espActual.ID.ToString();
            this.txtDesc.Text = espActual.Descripcion;
        }
        public virtual void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                espActual = new Especialidad();
            }
            espActual.Descripcion = this.txtDesc.Text;
            switch (Modo)
            {
                case ModoForm.Alta:
                    espActual.State = BusinessEntity.States.New;
                    break;
                case ModoForm.Modificacion:
                    espActual.State = BusinessEntity.States.Modified;
                    break;
            }
        }

        public virtual void Eliminar()
        {
            EspecialidadLogic e = new EspecialidadLogic();
            e.Delete(espActual.ID);
        }

        public virtual void GuardarCambios()
        {
            EspecialidadLogic e = new EspecialidadLogic();
            MapearADatos();
            e.Save(espActual);
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
    }
}
