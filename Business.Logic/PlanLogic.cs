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
    public class PlanLogic : BusinessLogic
    {
        public PlanAdapter planAdapter;

        public PlanLogic() 
        {
            planAdapter = new PlanAdapter();
        }

        public List<Plan> GetAll() 
        {
            try
            {
                List<Plan> planes = planAdapter.GetAll();
                return planes;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de planes", Ex);
                throw ExcepcionManejada;
            }
        }

        public Plan GetOne(int ID)
        {
            Plan plan = planAdapter.GetOne(ID);
            return plan;
        }

        public void Delete(int ID)
        {
            planAdapter.Delete(ID);
        }
        public void Save(Plan plan)
        {
            planAdapter.Save(plan);
        }


    }
}
