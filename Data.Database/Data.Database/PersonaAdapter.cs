using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Business.Entities;
using static Business.Entities.Persona;

namespace Data.Database
{
    public class PersonaAdapter:Adapter
    {
        public Persona GetOne(int ID)
        {
            Persona persona = new Persona();
            try
            {
                this.OpenConnection();

                SqlCommand cmdPersona = new SqlCommand("Select * from personas where id_persona = @id", sqlConn);
                cmdPersona.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drPersona = cmdPersona.ExecuteReader();

                if (drPersona.Read())
                {
                    persona.ID = (int)drPersona["id_persona"];
                    persona.Nombre = (string)drPersona["nombre"];
                    persona.Apellido = (string)drPersona["apellido"];
                    persona.Direccion = (string)drPersona["direccion"];
                    persona.Email = (string)drPersona["email"];
                    persona.Telefono = (string)drPersona["telefono"];
                    persona.FechaNacimiento = (DateTime)drPersona["fecha_nac"];
                    persona.Legajo = (int)drPersona["legajo"];
                    persona.TipoPersona = (Persona.TiposPersona)(int)drPersona["tipo_persona"];
                    persona.IDPlan = (int)drPersona["id_plan"];
                }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de la persona", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return persona;
        }

        public Persona GetOneByLegajo(string legajo)
        {
            Persona persona = new Persona();
            try
            {
                this.OpenConnection();

                SqlCommand cmdPersona = new SqlCommand("Select * from personas where legajo = @legajo", sqlConn);
                cmdPersona.Parameters.Add("@legajo", SqlDbType.Int).Value = legajo;
                SqlDataReader drPersona = cmdPersona.ExecuteReader();

                if (drPersona.Read())
                {
                    persona.ID = (int)drPersona["id_persona"];
                    persona.Nombre = (string)drPersona["nombre"];
                    persona.Apellido = (string)drPersona["apellido"];
                    persona.Direccion = (string)drPersona["direccion"];
                    persona.Email = (string)drPersona["email"];
                    persona.Telefono = (string)drPersona["telefono"];
                    persona.FechaNacimiento = (DateTime)drPersona["fecha_nac"];
                    persona.Legajo = (int)drPersona["legajo"];
                    persona.TipoPersona = (Persona.TiposPersona)(int)drPersona["tipo_persona"];
                    persona.IDPlan = (int)drPersona["id_plan"];
                }
                else
                {
                    persona = null;
                }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de la persona", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return persona;
        }

        public List<Persona> GetAlumnos()
        {
            List<Persona> personas = new List<Persona>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdPersona = new SqlCommand("select * from personas where tipo_persona = @tipoPersona", sqlConn);
                cmdPersona.Parameters.Add("@tipoPersona", SqlDbType.Int).Value = TiposPersona.Alumno;
                SqlDataReader drPersonas = cmdPersona.ExecuteReader();

                while (drPersonas.Read())
                {
                    Persona persona = new Persona
                    {
                        ID = (int)drPersonas["id_persona"],
                        Nombre = (string)drPersonas["nombre"],
                        Apellido = (string)drPersonas["apellido"],
                        Direccion = (string)drPersonas["direccion"],
                        Email = (string)drPersonas["email"],
                        Telefono = (string)drPersonas["telefono"],
                        FechaNacimiento = (DateTime)drPersonas["fecha_nac"],
                        Legajo = (int)drPersonas["legajo"],
                        TipoPersona = (Persona.TiposPersona)(int)drPersonas["tipo_persona"],
                        IDPlan = (int)drPersonas["id_plan"]
                    };
                    personas.Add(persona);
                }
                drPersonas.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Alumnos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return personas;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("delete personas where id_persona=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar persona", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(Persona persona)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE personas SET id_plan=@plan,tipo_persona=@t_persona,legajo=@legajo,telefono=@telefono,email=@email,direccion=@direccion,apellido=@apellido,nombre=@nombre,fecha_nac=@fecha_nac WHERE id_persona=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = persona.ID;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdSave.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdSave.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdSave.Parameters.Add("@fecha_nac", SqlDbType.DateTime).Value = persona.FechaNacimiento;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = persona.Legajo;
                cmdSave.Parameters.Add("@t_persona", SqlDbType.Int).Value = persona.TipoPersona;
                cmdSave.Parameters.Add("@plan", SqlDbType.Int).Value = persona.IDPlan;

                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos de la persona", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        protected void Insert(Persona persona)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT into personas (nombre,apellido,direccion,email,telefono,fecha_nac,legajo,tipo_persona,id_plan) VALUES(@nombre,@apellido,@direccion,@email,@telefono,@fecha_nac,@legajo,@tipo_persona,@id_plan) Select @@identity", sqlConn);

                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = persona.Apellido;
                cmdSave.Parameters.Add("@direccion", SqlDbType.VarChar, 50).Value = persona.Direccion;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = persona.Email;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = persona.Nombre;
                cmdSave.Parameters.Add("@telefono", SqlDbType.VarChar, 50).Value = persona.Telefono;
                cmdSave.Parameters.Add("@fecha_nac", SqlDbType.DateTime).Value = persona.FechaNacimiento;
                cmdSave.Parameters.Add("@legajo", SqlDbType.Int).Value = persona.Legajo;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = persona.IDPlan;
                cmdSave.Parameters.Add("@tipo_persona", SqlDbType.Int).Value = persona.TipoPersona;

                persona.ID = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear persona", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        public void Save(Persona persona)
        {
            if (persona.State == BusinessEntity.States.New)
            {
                this.Insert(persona);
            }
            else if (persona.State == BusinessEntity.States.Deleted)
            {
                this.Delete(persona.ID);
            }
            else if (persona.State == BusinessEntity.States.Modified)
            {
                this.Update(persona);
            }
            persona.State = BusinessEntity.States.Unmodified;
        }
        public List<Persona> GetAll()
        {
            List<Persona> personas = new List<Persona>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdPersonas = new SqlCommand("Select * from personas", sqlConn);
                SqlDataReader drPersonas = cmdPersonas.ExecuteReader();

                while (drPersonas.Read())
                {
                    Persona persona= new Persona
                    {
                        ID = (int)drPersonas["id_persona"],
                        Nombre = (string)drPersonas["nombre"],
                        Apellido = (string)drPersonas["apellido"],
                        Direccion = (string)drPersonas["direccion"],
                        Email = (string)drPersonas["email"],
                        Telefono = (string)drPersonas["telefono"],
                        FechaNacimiento = (DateTime)drPersonas["fecha_nac"],
                        Legajo = (int)drPersonas["legajo"],
                        TipoPersona = (Persona.TiposPersona)(int)drPersonas["tipo_persona"],
                        IDPlan = (int)drPersonas["id_plan"]
                    };
                    personas.Add(persona);
                }
                drPersonas.Close();

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Personas", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return personas;
        }
    }
}
