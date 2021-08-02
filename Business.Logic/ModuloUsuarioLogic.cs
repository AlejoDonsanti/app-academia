using Business.Entities;
using Business.Entities.Dto;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class ModuloUsuarioLogic
    {
        public ModuloUsuarioAdapter moduloAdapter;
        public ModuloUsuarioLogic()
        {
            moduloAdapter = new ModuloUsuarioAdapter();
        }
        
        public ModuloUsuario GetPermiso(string modulo)
        {
            return moduloAdapter.GetPermiso(modulo);
        }

        public List<Permiso> GetAll()
        {
            return moduloAdapter.GetAll();
        }

        public void Update(Permiso permiso)
        {
            this.Update(permiso);
        }

        protected void Insert(Permiso permiso)
        {
            this.Insert(permiso);
        }
    }
}
