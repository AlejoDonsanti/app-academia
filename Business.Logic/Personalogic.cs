using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class Personalogic:BusinessLogic
    {
        public PersonaAdapter personaAdapter;
        public Personalogic()
        {
            personaAdapter = new PersonaAdapter();
        }
        public void Save(Persona persona)
        {
            personaAdapter.Save(persona);
        }
        public List<Persona> GetAll()
        {
            try
            {
                List<Persona> persona = personaAdapter.GetAll();
                return persona;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de personas", Ex);
                throw ExcepcionManejada;
            }
        }

        public List<Persona> GetAlumnos()
        {
            try
            {
                List<Persona> persona = personaAdapter.GetAlumnos();
                return persona;
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de personas", Ex);
                throw ExcepcionManejada;
            }
        }

        public Persona GetOne(int ID)
        {
            Persona persona = personaAdapter.GetOne(ID);
            return persona;
        }
        public void Delete(int ID)
        {
            personaAdapter.Delete(ID);
        }

        public Persona GetOneByLegajo(string legajo)
        {
            return personaAdapter.GetOneByLegajo(legajo);
        }
    }
}
