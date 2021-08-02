using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Logic;
using Data.Database;

namespace Business.Logic
{
    public class MateriaLogic : BusinessLogic
    {
        public MateriaAdapter materiaAdapter;

        public MateriaLogic() 
        {
            materiaAdapter = new MateriaAdapter();
        }

        public List<Materia> GetAll() 
        {
            try 
            {
                List<Materia> materias = materiaAdapter.GetAll();
                return materias;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de materias", Ex);
                throw ExcepcionManejada;
            }
        }
        public Materia GetOne(int ID) 
        {
            Materia materia = materiaAdapter.GetOne(ID);
            return materia;
        }

        public void Delete(int ID) 
        {
            materiaAdapter.Delete(ID);
        }
        public void Save(Materia materia)
        {
            materiaAdapter.Save(materia);
        }

    }
}
