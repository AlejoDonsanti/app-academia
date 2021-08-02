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
    public class ComisionLogic : BusinessLogic
    {
        public ComisionAdapter comisionAdapter;

        public ComisionLogic()
        {
            comisionAdapter = new ComisionAdapter();
        }
        public List<Comision> GetAll()
        {
            try
            {
                List<Comision> comisiones = comisionAdapter.GetAll();
                return comisiones;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de comisiones", Ex);
                throw ExcepcionManejada;
            }
        }
        public Comision GetOne(int ID)
        {
            Comision comision = comisionAdapter.GetOne(ID);
            return comision;
        }
        public void Delete(int ID)
        {
            comisionAdapter.Delete(ID);
        }
        public void Save(Comision comision)
        {
            comisionAdapter.Save(comision);
        }
    }
}
