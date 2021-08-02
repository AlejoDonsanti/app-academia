using Business.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class AlumnoInscripcionAdapter : Adapter
    {
        public List<AlumnoInscripcion> GetAll()
        {
            List<AlumnoInscripcion> inscripciones = new List<AlumnoInscripcion>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdInscripcion = new SqlCommand("Select * from alumnos_inscripciones", sqlConn);

                SqlDataReader drInscripcion = cmdInscripcion.ExecuteReader();

                while (drInscripcion.Read())
                {
                    AlumnoInscripcion inscripcion = new AlumnoInscripcion
                    {
                        ID = (int)drInscripcion["id_inscripcion"],
                        Condicion = (string)drInscripcion["condicion"],
                        IDAlumno = (int)drInscripcion["id_alumno"],
                        IDCurso = (int)drInscripcion["id_curso"],
                        Nota = drInscripcion["nota"] != System.DBNull.Value ? (int?)drInscripcion["nota"] : default(int?)
                    };
                    inscripciones.Add(inscripcion);
                }
                drInscripcion.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Alumnos Inscriptos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return inscripciones;
        }

        public AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion ins = new AlumnoInscripcion();
            try
            {
                this.OpenConnection();

                SqlCommand cmdInscripcion = new SqlCommand("SELECT * from alumnos_inscripciones WHERE id_inscripcion=@id", sqlConn);
                cmdInscripcion.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drInscripcion = cmdInscripcion.ExecuteReader();

                if (drInscripcion.Read())
                {
                    ins.ID = (int)drInscripcion["id_inscripcion"];
                    ins.IDAlumno = (int)drInscripcion["id_alumno"];
                    ins.IDCurso = (int)drInscripcion["id_curso"];
                    ins.Condicion = (string)drInscripcion["condicion"];
                    ins.Nota = drInscripcion["nota"] != System.DBNull.Value ? (int?)drInscripcion["nota"] : default(int?);


                }
                drInscripcion.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de los alumnos inscriptos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return ins;
        }

        public List<AlumnoInscripcion> GetAllById(int id)
        {
            List<AlumnoInscripcion> inscripciones = new List<AlumnoInscripcion>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdInscripcion = new SqlCommand("Select * from alumnos_inscripciones where id_curso=@id", sqlConn);
                cmdInscripcion.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader drInscripcion = cmdInscripcion.ExecuteReader();

                while (drInscripcion.Read())
                {
                    AlumnoInscripcion inscripcion = new AlumnoInscripcion
                    {
                        ID = (int)drInscripcion["id_inscripcion"],
                        Condicion = (string)drInscripcion["condicion"],
                        IDAlumno = (int)drInscripcion["id_alumno"],
                        IDCurso = (int)drInscripcion["id_curso"],
                        Nota = drInscripcion["nota"] != System.DBNull.Value ? (int?)drInscripcion["nota"] : default(int?)
                    };
                    inscripciones.Add(inscripcion);
                }
                drInscripcion.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Alumnos Inscriptos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return inscripciones;
        }

        //public List<AlumnoInscripcion> GetAllAlumnos(int id)
        //{
        //    List<AlumnoInscripcion> inscripciones = new List<AlumnoInscripcion>();

        //    try
        //    {
        //        this.OpenConnection();

        //        SqlCommand cmdInscripcion = new SqlCommand(@"select a.condicion,a.nota, m.desc_materia, com.desc_comision from cursos a
        //                                                    innerjoin materias m on a.id_materia = m.id_materia
        //                                                    innerjoin comisiones com on com.id_comision = a.id_comision
        //                                                    wherewhere id_alumno =@id", sqlConn);
        //        cmdInscripcion.Parameters.Add("@id", SqlDbType.Int).Value = id;
        //        SqlDataReader drInscripcion = cmdInscripcion.ExecuteReader();

        //        while (drInscripcion.Read())
        //        {
        //            AlumnoInscripcion inscripcion = new AlumnoInscripcion
        //            {
        //                ID = (int)drInscripcion["id_inscripcion"],
        //                Condicion = (string)drInscripcion["condicion"],
        //                IDAlumno = (int)drInscripcion["id_alumno"],
        //                IDCurso = (int)drInscripcion["id_curso"],
        //                Nota = drInscripcion["nota"] != System.DBNull.Value ? (int?)drInscripcion["nota"] : default(int?)
        //            };
        //            inscripciones.Add(inscripcion);
        //        }
        //        drInscripcion.Close();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Exception ExcepcionManejada =
        //       new Exception("Error al recuperar lista de Alumnos Inscriptos", Ex);
        //        throw ExcepcionManejada;
        //    }
        //    finally { this.CloseConnection(); }
        //    return inscripciones;
        //}
        public bool ExisteAlumnoInscriptoPorCurso(int idCurso)
        {
            AlumnoInscripcion ins = new AlumnoInscripcion();
            bool existeAlumnoInscripto;
            try
            {
                this.OpenConnection();

                SqlCommand cmdInscripcion = new SqlCommand("SELECT * from alumnos_inscripciones WHERE id_curso=@idCurso", sqlConn);
                cmdInscripcion.Parameters.Add("@idCurso", SqlDbType.Int).Value = idCurso;
                SqlDataReader drInscripcion = cmdInscripcion.ExecuteReader();

                existeAlumnoInscripto = drInscripcion.HasRows;
                drInscripcion.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al verificar si existen alumnos inscriptos al curso", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return existeAlumnoInscripto;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("delete alumnos_inscripciones where id_inscripcion=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(AlumnoInscripcion esp)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE alumnos_inscripciones SET condicion=@cond,id_alumno=@alumno,id_curso=@curso,nota=@nota WHERE id_inscripcion=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = esp.ID;
                cmdSave.Parameters.Add("@alumno", SqlDbType.Int).Value = esp.IDAlumno;
                cmdSave.Parameters.Add("@curso", SqlDbType.Int).Value = esp.IDCurso;
                cmdSave.Parameters.Add("@cond", SqlDbType.VarChar, 50).Value = esp.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = esp.Nota;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos de una inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(AlumnoInscripcion ins)
        {
            if (ins.State == BusinessEntity.States.New)
            {
                this.Insert(ins);
            }
            else if (ins.State == BusinessEntity.States.Deleted)
            {
                this.Delete(ins.ID);
            }
            else if (ins.State == BusinessEntity.States.Modified)
            {
                this.Update(ins);
            }
            ins.State = BusinessEntity.States.Unmodified;
        }
        public void Insert(AlumnoInscripcion ins)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT into alumnos_inscripciones (id_alumno,id_curso,condicion,nota) VALUES (@alumno,@curso,@cond,@nota) SELECT @@identity", sqlConn);
                cmdSave.Parameters.Add("@alumno", SqlDbType.Int).Value = ins.IDAlumno;
                cmdSave.Parameters.Add("@curso", SqlDbType.Int).Value = ins.IDCurso;
                cmdSave.Parameters.Add("@cond", SqlDbType.VarChar, 50).Value = ins.Condicion;
                cmdSave.Parameters.Add("@nota", SqlDbType.Int).Value = (object)ins.Nota ?? DBNull.Value;
                ins.ID = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear la inscripcion", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

        }
    }
}
