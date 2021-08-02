using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Data.Database
{
    public class CursoAdapter : Adapter
    {
        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdCursos = new SqlCommand("Select * from cursos", sqlConn);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso curso = new Curso
                    {
                        ID = (int)drCursos["id_curso"],
                        Descripcion = (string)drCursos["desc_curso"],
                        IDMateria = (int)drCursos["id_materia"],
                        IDComision = (int)drCursos["id_comision"],
                        AnioCalendario = (int)drCursos["anio_calendario"],
                        Cupo = (int)drCursos["cupo"]
                    };
                    cursos.Add(curso);
                }
                drCursos.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Cursos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return cursos;
        }

        public List<Curso> CursosAñoActual()
        {
            List<Curso> cursos = new List<Curso>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdCursos = new SqlCommand(@"select c.*, m.desc_materia, com.desc_comision from cursos c 
                                                        inner join materias m on c.id_materia = m.id_materia 
                                                        inner join comisiones com on com.id_comision = c.id_comision 
                                                        where anio_calendario = YEAR(GETDATE()) 
                                                        AND c.cupo > 0", sqlConn);
                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso curso = new Curso
                    {
                        ID = (int)drCursos["id_curso"],
                        IDMateria = (int)drCursos["id_materia"],
                        IDComision = (int)drCursos["id_comision"],
                        AnioCalendario = (int)drCursos["anio_calendario"],
                        Cupo = (int)drCursos["cupo"],
                        Descripcion = (string)drCursos["desc_materia"] + " - " + (string)drCursos["desc_comision"]
                    };
                    cursos.Add(curso);
                }
                drCursos.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Cursos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return cursos;
        }

        public Curso GetOne(int ID)
        {
            Curso curso = new Curso();

            try
            {
                this.OpenConnection();

                SqlCommand cmdCurso = new SqlCommand("Select * from cursos where id_curso=@id", sqlConn);
                cmdCurso.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drCurso = cmdCurso.ExecuteReader();

                if (drCurso.Read())
                {
                    curso.ID = (int)drCurso["id_curso"];
                    curso.Descripcion = (string)drCurso["desc_curso"];
                    curso.IDComision = (int)drCurso["id_comision"];
                    curso.IDMateria = (int)drCurso["id_materia"];
                    curso.AnioCalendario = (int)drCurso["anio_calendario"];
                    curso.Cupo = (int)drCurso["cupo"];
                }

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar la comision", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return curso;
        }
        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("delete cursos where id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Update(Curso c)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE cursos set desc_curso=@desc,id_materia=@id_materia,id_comision=@id_comision, anio_calendario=@anio,cupo=@cupo where id_curso=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = c.ID;
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = c.Descripcion;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = c.AnioCalendario;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = c.IDComision;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = c.IDMateria;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = c.Cupo;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos del curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

        }
        public void Insert(Curso c)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT into cursos (id_materia,id_comision,anio_calendario,cupo) VALUES (@id_materia,@id_comision,@anio,@cupo) SELECT @@identity", sqlConn);
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = c.Descripcion;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = c.AnioCalendario;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = c.IDComision;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = c.IDMateria;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = c.Cupo;
                c.ID = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear el curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

        }

        public void Save(Curso curso)
        {
            if (curso.State == BusinessEntity.States.New)
            {
                this.Insert(curso);
            }
            else if (curso.State == BusinessEntity.States.Deleted)
            {
                this.Delete(curso.ID);
            }
            else if (curso.State == BusinessEntity.States.Modified)
            {
                this.Update(curso);
            }
            curso.State = BusinessEntity.States.Unmodified;
        }

    }
}
