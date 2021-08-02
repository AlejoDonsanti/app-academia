using Business.Entities;
using Business.Logic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class AlumnoInscripciones : Form
    {
        public Persona PersonaActual { get; set; }
        public AlumnoInscripciones()
        {
            InitializeComponent();
        }

        public void Listar()
        {
            Personalogic logicaPersona = new Personalogic();
            var alumnos = logicaPersona.GetAlumnos();
            CursoLogic logicaCurso = new CursoLogic();
            var cursos = logicaCurso.GetCursosAñoActual();
            AlumnoInscripcionLogic inscripcion = new AlumnoInscripcionLogic();
            try
            {
                var inscripciones = inscripcion.GetAll();
                var query = from inscrip in inscripciones
                            join alumno in alumnos on inscrip.IDAlumno equals alumno.ID
                            join curso in cursos on inscrip.IDCurso equals curso.ID
                            select new
                            {
                                inscrip.Nota,
                                inscrip.Condicion,
                                inscrip.ID,
                                Alumno = alumno.Apellido + ", " + alumno.Nombre,
                                Curso = curso.Descripcion

                            };
                this.dgvInscripciones.DataSource = query.ToList();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de alumnos inscriptos", Ex);
                MessageBox.Show("Error al recuperar lista de alumnos inscriptos", "alumnos inscriptos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ExcepcionManejada;

            }
            finally
            {
                if (Session.Persona.TipoPersona == Persona.TiposPersona.Alumno)
                {
                    dgvInscripciones.Visible = false;
                }
            }
        }
        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            AlumnoInscripcionDesktop formInscipciones = new AlumnoInscripcionDesktop(ApplicationForm.ModoForm.Alta, this.PersonaActual);
            formInscipciones.ShowDialog();
            this.Listar();
        }

        private void Inscripciones_Load(object sender, EventArgs e)
        {
            Personalogic logica = new Personalogic();
            this.PersonaActual = logica.GetOne(Session.Usuario.IDPersona.Value);
            Listar();
        }

        private void tsbPDF_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dgvInscripciones.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgvInscripciones.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgvInscripciones.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }

    }
}
