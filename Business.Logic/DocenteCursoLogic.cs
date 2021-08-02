
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class DocenteCursoLogic
    {
        public DocenteCursoAdapter DocenteCursoData { get; set; }

        public DocenteCursoLogic()
        {
            DocenteCursoData = new DocenteCursoAdapter();
        }

        public DocenteCurso GetOne(int ID)
        {
            try
            {
                return DocenteCursoData.GetOne(ID);
            }
            catch
            {
                throw new Exception("Error al recuperar datos, intente nuevamente");
            }
        }

        public void Save(DocenteCurso docCurso)
        {
            try
            {
                DocenteCursoData.Save(docCurso);
            }
            catch
            {
                throw new Exception("Error al registrar los datos, intente nuevamente");
            }
        }


        public List<DocenteCurso> GetDocentesPorCurso(int idCurso)
        {
            try
            {
                return DocenteCursoData.GetDocentesPorCurso(idCurso);
            }
            catch
            {
                throw new Exception("Error al recuperar datos, intente nuevamente");
            }
        }

        public List<DocenteCurso> GetCursosPorDocente(int idDocente)
        {
            try
            {
                return DocenteCursoData.GetCursosPorDocente(idDocente);
            }
            catch
            {
                throw new Exception("Error al recuperar datos, intente nuevamente");
            }
        }
    }
}
