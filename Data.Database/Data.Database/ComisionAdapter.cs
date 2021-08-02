using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Data.Database
{
    public class ComisionAdapter : Adapter
    {
        public List<Comision> GetAll()
        {
            List<Comision> comisiones = new List<Comision>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdComision = new SqlCommand("Select * from comisiones", sqlConn);

                SqlDataReader drComision = cmdComision.ExecuteReader();

                while (drComision.Read())
                {
                    Comision comision = new Comision
                    {
                        ID = (int)drComision["id_comision"],
                        Descripcion = (string)drComision["desc_comision"],
                        AnioEspecialidad = (int)drComision["anio_especialidad"],
                        IDPlan = (int)drComision["id_plan"]
                    };
                    comisiones.Add(comision);
                }
                drComision.Close();
            }catch(Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de Comisiones", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return comisiones;
        }
        public Comision GetOne(int ID)
        {
            Comision comision = new Comision();
            try
            {
                this.OpenConnection();
                SqlCommand cmdComision = new SqlCommand("Select * from comisiones where id_comision=@id",sqlConn);
                cmdComision.Parameters.Add("@id",SqlDbType.Int).Value = ID;
                SqlDataReader drComision = cmdComision.ExecuteReader();

                if (drComision.Read())
                {
                    comision.ID = (int)drComision["id_comision"];
                    comision.Descripcion = (string)drComision["desc_comision"];
                    comision.AnioEspecialidad = (int)drComision["anio_especialidad"];
                    comision.IDPlan = (int)drComision["id_plan"];
                }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar la comision", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return comision;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("delete comisiones where id_comision=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(Comision c)
        {
            try 
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE comisiones set desc_comision=@desc,anio_especialidad=@anio,id_plan=@id_plan where id_comision=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = c.ID;
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = c.Descripcion;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = c.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = c.IDPlan;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos de la comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

        }

        public void Insert(Comision c)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT into comisiones (desc_comision,anio_especialidad,id_plan) VALUES(@desc,@anio,@id_plan) SELECT @@identity", sqlConn);
                cmdSave.Parameters.Add("@desc", SqlDbType.VarChar, 50).Value = c.Descripcion;
                cmdSave.Parameters.Add("@anio", SqlDbType.Int).Value = c.AnioEspecialidad;
                cmdSave.Parameters.Add("@id_plan", SqlDbType.Int).Value = c.IDPlan;
                c.ID = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear comision", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Comision comision)
        {
            if (comision.State == BusinessEntity.States.New)
            {
                this.Insert(comision);
            }
            else if (comision.State == BusinessEntity.States.Deleted)
            {
                this.Delete(comision.ID);
            }
            else if (comision.State == BusinessEntity.States.Modified)
            {
                this.Update(comision);
            }
            comision.State = BusinessEntity.States.Unmodified;
        }


    }
}
