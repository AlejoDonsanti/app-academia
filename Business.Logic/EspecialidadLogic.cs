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
    public class EspecialidadLogic : BusinessLogic
    {
        public EspecialidadAdapter espData;

        public EspecialidadLogic() 
        {
            espData = new EspecialidadAdapter();
        }

        public List<Especialidad> GetAll()
        {
            try
            {
                List<Especialidad> especialidades = espData.GetAll();
                return especialidades;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de especialidades", Ex);
                throw ExcepcionManejada;
            }
        }
        public Especialidad GetOne(int ID) 
        {
            Especialidad esp = espData.GetOne(ID);
            return esp;
        }

        public void Delete(int ID) 
        {
            espData.Delete(ID);
        }
        public void Save(Especialidad esp) 
        {
            espData.Save(esp);
        }


    }
}
