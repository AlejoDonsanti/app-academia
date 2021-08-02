using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;

namespace Business.Logic
{
    public class AlumnoInscripcionLogic
    {
        public AlumnoInscripcionAdapter inscripcionAdapter { get; set; }
        public AlumnoInscripcionLogic()
        {
            inscripcionAdapter = new AlumnoInscripcionAdapter();
        }
        public List<AlumnoInscripcion> GetAll()
        {
            try
            {
                List<AlumnoInscripcion> inscripcion = inscripcionAdapter.GetAll();
                return inscripcion;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de Inscriptos", Ex);
                throw ExcepcionManejada;
            }
        }

        //public List<AlumnoInscripcion> GetAllAlumnos(int id)
        //{
        //   return inscripcionAdapter.GetAllAlumnos(id);
        //}
        public AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion inscripcion = inscripcionAdapter.GetOne(ID);
            return inscripcion;
        }
        public void Delete(int ID)
        {
            inscripcionAdapter.Delete(ID);
        }
        public void Save(AlumnoInscripcion inscripcionActual)
        {
            try
            {
                inscripcionAdapter.Save(inscripcionActual);
                CursoLogic cursoLogic = new CursoLogic();
                Curso cursoDB = cursoLogic.GetOne(inscripcionActual.IDCurso);
                cursoDB.Cupo = cursoDB.Cupo - 1;
                cursoDB.State = BusinessEntity.States.Modified;
                cursoLogic.Save(cursoDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update (AlumnoInscripcion inscripcionActual)
        {
            inscripcionAdapter.Update(inscripcionActual);
        }

        public List<AlumnoInscripcion> GetAllById(int id)
        {
            return inscripcionAdapter.GetAllById(id);
        }

        internal bool GetAlumnoInscriptoPorCurso(int idCurso)
        {
            return inscripcionAdapter.ExisteAlumnoInscriptoPorCurso(idCurso);
        }
    }
}
