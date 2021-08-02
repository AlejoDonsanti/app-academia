using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using System.Data.SqlClient;

namespace Data.Database
{
    public class DocenteCursoAdapter : Adapter
    {

            public void Save(DocenteCurso docCur)
            {
                if (docCur.State == BusinessEntity.States.New)
                {
                    this.Insert(docCur);
                }
                else if (docCur.State == BusinessEntity.States.Deleted)
                {
                    this.Delete(docCur.ID);
                }
                else if (docCur.State == BusinessEntity.States.Modified)
                {
                    this.Update(docCur);
                }
            }

            public List<DocenteCurso> GetDocentesPorCurso(int idCurso)
            {
                List<DocenteCurso> docentesCurso = new List<DocenteCurso>();
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdDocentes = new SqlCommand("SELECT * FROM docentes_cursos WHERE id_curso = @id", sqlConn);
                    cmdDocentes.Parameters.AddWithValue("@id", idCurso);
                    SqlDataReader drDocente = cmdDocentes.ExecuteReader();

                    while (drDocente.Read())
                    {
                        DocenteCurso doccur = new DocenteCurso()
                        {
                            ID = (int)drDocente["id_dictado"],
                            IDCurso = (int)drDocente["id_curso"],
                            IDDocente = (int)drDocente["id_docente"],
                            Cargo = (string)drDocente["cargo"]
                        };

                        docentesCurso.Add(doccur);
                    }

                    drDocente.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.CloseConnection();
                }

                return docentesCurso;

            }

            public DocenteCurso GetOne(int ID)
            {
                DocenteCurso docCurso = new DocenteCurso();

                try
                {
                    this.OpenConnection();

                    SqlCommand cmdDocCursos = new SqlCommand("SELECT * FROM docentes_cursos WHERE id_dictado=@id", sqlConn);
                    cmdDocCursos.Parameters.AddWithValue("id", ID);

                    SqlDataReader drDocCursos = cmdDocCursos.ExecuteReader();

                    if (drDocCursos.Read())
                    {
                        docCurso.ID = (int)drDocCursos["id_dictado"];
                        docCurso.IDCurso = (int)drDocCursos["id_curso"];
                        docCurso.IDDocente = (int)drDocCursos["id_docente"];
                        docCurso.Cargo = (string)drDocCursos["cargo"];
                    }

                    drDocCursos.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.CloseConnection();
                }

                return docCurso;
            }

            public List<DocenteCurso> GetCursosPorDocente(int idDocente)
            {
                List<DocenteCurso> docentesCurso = new List<DocenteCurso>();
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdDocentes = new SqlCommand("SELECT * FROM docentes_cursos WHERE id_docente = @id", sqlConn);
                    cmdDocentes.Parameters.AddWithValue("@id", idDocente);
                    SqlDataReader drDocente = cmdDocentes.ExecuteReader();

                    while (drDocente.Read())
                    {
                        DocenteCurso doccur = new DocenteCurso()
                        {
                            ID = (int)drDocente["id_dictado"],
                            IDCurso = (int)drDocente["id_curso"],
                            IDDocente = (int)drDocente["id_docente"],
                            Cargo = (string)drDocente["cargo"]
                        };

                        docentesCurso.Add(doccur);
                    }

                    drDocente.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.CloseConnection();
                }

                return docentesCurso;

            }


            protected void Insert(DocenteCurso docCurso)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdSave = new SqlCommand("INSERT INTO docentes_cursos(id_curso, id_docente, cargo) " +
                                                                      "VALUES(@id_curso, @id_docente, @cargo)", sqlConn);

                    cmdSave.Parameters.AddWithValue("@id_curso", docCurso.IDCurso);
                    cmdSave.Parameters.AddWithValue("@id_docente", docCurso.IDDocente);
                    cmdSave.Parameters.AddWithValue("@cargo", docCurso.Cargo);
                    docCurso.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteNonQuery());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.CloseConnection();
                }
            }

            protected void Delete(int Id)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdDelete = new SqlCommand("DELETE FROM docentes_cursos WHERE id_dictado = @id", sqlConn);
                    cmdDelete.Parameters.AddWithValue("@id", Id);
                    cmdDelete.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.CloseConnection();
                }

            }

            protected void Update(DocenteCurso docCurso)
            {
                try
                {
                    this.OpenConnection();
                    SqlCommand cmdSave = new SqlCommand("UPDATE docentes_cursos SET id_curso = @id_curso," +
                                                                                " id_docente = @id_docente, " +
                                                                                " cargo = @cargo " +
                                                                                "WHERE id_dictado = @id", sqlConn);

                    cmdSave.Parameters.AddWithValue("@id_curso", docCurso.IDCurso);
                    cmdSave.Parameters.AddWithValue("@id_docente", docCurso.IDDocente);
                    cmdSave.Parameters.AddWithValue("@cargo", docCurso.Cargo);
                    cmdSave.Parameters.AddWithValue("@id", docCurso.ID);
                    cmdSave.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.CloseConnection();
                }
            }
        }
    }



