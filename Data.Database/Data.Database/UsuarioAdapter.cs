using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class UsuarioAdapter:Adapter
    {
        #region DatosEnMemoria
        // Esta regi?n solo se usa en esta etapa donde los datos se mantienen en memoria.
        // Al modificar este proyecto para que acceda a la base de datos esta ser? eliminada
        private static List<Usuario> _Usuarios;

        private static List<Usuario> Usuarios
        {
            get
            {
                if (_Usuarios == null)
                {
                    _Usuarios = new List<Business.Entities.Usuario>();
                    Business.Entities.Usuario usr;
                    usr = new Business.Entities.Usuario();
                    usr.ID = 1;
                    usr.State = Business.Entities.BusinessEntity.States.Unmodified;
                    usr.Nombre = "Casimiro";
                    usr.Apellido = "Cegado";
                    usr.NombreUsuario = "casicegado";
                    usr.Clave = "miro";
                    usr.EMail = "casimirocegado@gmail.com";
                    usr.Habilitado = true;
                    _Usuarios.Add(usr);

                    usr = new Business.Entities.Usuario();
                    usr.ID = 2;
                    usr.State = Business.Entities.BusinessEntity.States.Unmodified;
                    usr.Nombre = "Armando Esteban";
                    usr.Apellido = "Quito";
                    usr.NombreUsuario = "aequito";
                    usr.Clave = "carpintero";
                    usr.EMail = "armandoquito@gmail.com";
                    usr.Habilitado = true;
                    _Usuarios.Add(usr);

                    usr = new Business.Entities.Usuario();
                    usr.ID = 3;
                    usr.State = Business.Entities.BusinessEntity.States.Unmodified;
                    usr.Nombre = "Alan";
                    usr.Apellido = "Brado";
                    usr.NombreUsuario = "alanbrado";
                    usr.Clave = "abrete sesamo";
                    usr.EMail = "alanbrado@gmail.com";
                    usr.Habilitado = true;
                    _Usuarios.Add(usr);

                }
                return _Usuarios;
            }
        }
        #endregion

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("Select * from usuarios", sqlConn);

                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();

                while (drUsuarios.Read())
                {
                    Usuario usr = new Usuario();

                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    if (drUsuarios["id_persona"] != System.DBNull.Value)
                    {
                        usr.IDPersona = (int?)drUsuarios["id_persona"];
                    }
                    if (drUsuarios["email"] != System.DBNull.Value)
                    {
                        usr.EMail = (string)drUsuarios["email"];
                    }

                    usuarios.Add(usr);
                }
                drUsuarios.Close();
                
                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar lista de usuarios", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return usuarios;

        }

        public Business.Entities.Usuario GetOne(int ID)
        {
            Usuario usr = new Usuario();
            try
            {
               
                this.OpenConnection();

                SqlCommand cmdUsuarios = new SqlCommand("Select * from usuarios where id_usuario=@id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();

                if (drUsuarios.Read())
                {
                    

                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    if (drUsuarios["id_persona"] != System.DBNull.Value)
                    {
                        usr.IDPersona = (int?)drUsuarios["id_persona"];
                    }
                    if (drUsuarios["email"] != System.DBNull.Value)
                    {
                        usr.EMail = (string)drUsuarios["email"];
                    }


                }
                drUsuarios.Close();
                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return usr;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("delete usuarios where id_usuario=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }


        }

        protected void Update(Usuario usuario)
        {
            try 
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE usuarios SET nombre_usuario=@nombre_usuario, clave=@clave, habilitado=@habilitado, nombre=@nombre, apellido=@apellido, email=@email, id_persona=@id_persona WHERE id_usuario=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = usuario.ID;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar,50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar,50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.EMail;
                cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = usuario.IDPersona;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Usuario usuario)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT into usuarios (nombre_usuario,clave,habilitado,nombre,apellido,email,id_persona) VALUES(@nombre_usuario, @clave, @habilitado, @nombre, @apellido, @email,@id_persona) Select @@identity", sqlConn);
                
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("@apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.EMail;
                cmdSave.Parameters.Add("@id_persona", SqlDbType.Int).Value = usuario.IDPersona;
                usuario.ID = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
                
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Usuario usuario)
        {
            if (usuario.State == BusinessEntity.States.New)
            {
                this.Insert(usuario);
            }
            else if (usuario.State == BusinessEntity.States.Deleted)
            {
                this.Delete(usuario.ID);
            }
            else if (usuario.State == BusinessEntity.States.Modified)
            {
                this.Update(usuario);
            }
            usuario.State = BusinessEntity.States.Unmodified;            
        }

        public Usuario Login(string user, string pass) 
        {
            try
            {
                Usuario usr = new Usuario();
                this.OpenConnection();
                SqlCommand getkey = new SqlCommand("SELECT * FROM usuarios WHERE nombre_usuario=@user and clave=@clave", sqlConn);
                getkey.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = user;
                getkey.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = pass;
                SqlDataReader drClave = getkey.ExecuteReader();
                if (drClave.Read())
                {
                    usr.ID = (int)drClave["id_usuario"];
                    usr.NombreUsuario = (string)drClave["nombre_usuario"];
                    usr.Clave = (string)drClave["clave"];
                    usr.Habilitado = (bool)drClave["habilitado"];
                    usr.Nombre = (string)drClave["nombre"];
                    usr.Apellido = (string)drClave["apellido"];
                    if (drClave["id_persona"] != System.DBNull.Value)
                    {
                        usr.IDPersona = (int?)drClave["id_persona"];
                    }
                    if (drClave["email"] != System.DBNull.Value)
                    {
                        usr.EMail = (string)drClave["email"];
                    }
                    return usr;
                } 
                else { return null; }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar usuario", Ex);
                throw ExcepcionManejada;
            }
            finally 
            {
                this.CloseConnection();
            }
        }
    }
}
