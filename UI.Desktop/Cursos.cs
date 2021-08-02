using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace UI.Desktop
{
    public partial class Cursos : FormButtons
    {
        public Cursos()
        {
            InitializeComponent();
            this.SetearPermisos("Cursos");
        }

        public void Listar()
        {
            CursoLogic cl = new CursoLogic();
            ComisionLogic Coml = new ComisionLogic();
            MateriaLogic ml = new MateriaLogic();
            try
            {
                List<Curso> cursos = cl.GetAll();
                List<Comision> comisiones = Coml.GetAll();
                List<Materia> materias = ml.GetAll();
                var query = from c in cursos
                            join cm in comisiones
                            on c.IDComision equals cm.ID
                            join ma in materias
                            on c.IDMateria equals ma.ID
                            select new
                            {
                                ID = c.ID,
                                AnioCalendario = c.AnioCalendario,
                                Comision = cm.Descripcion,
                                Materia = ma.Descripcion,
                                Cupo = c.Cupo
                            };
                dgvCursos.DataSource = query.ToList();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Cursos", Ex);
                MessageBox.Show("Error al recuperar lista de Cursos", "Cursos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ExcepcionManejada;
            }

        }

        private void Cursos_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }
        public override void tsbNuevo_Click(object sender, EventArgs e)
        {
            CursoDesktop formCur = new CursoDesktop(ApplicationForm.ModoForm.Alta);
            formCur.ShowDialog();
            this.Listar();
        }

        public override void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvCursos.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(this.dgvCursos.SelectedRows[0].Cells["ID"].Value);
                CursoDesktop formCurso = new CursoDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formCurso.ShowDialog();
                this.Listar();
            }
            else
            {
                MessageBox.Show("Seleccionar una fila en la grilla para poder editar");
            }
        }

        public override void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvCursos.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(this.dgvCursos.SelectedRows[0].Cells["ID"].Value);
                CursoDesktop formCurso = new CursoDesktop(ID, ApplicationForm.ModoForm.Baja);
                formCurso.ShowDialog();
                this.Listar();
            }
        }

        public override void tsbExportar_Click(object sender, EventArgs e)
        {
            if (dgvCursos.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(dgvCursos.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgvCursos.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgvCursos.Rows)
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

                            MessageBox.Show("Cursos exportados", "Info");
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
                MessageBox.Show("No existen cursos para exportar", "Info");
            }
        }
    }
}
