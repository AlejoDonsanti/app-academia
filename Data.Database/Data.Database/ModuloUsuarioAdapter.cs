using Business.Entities;
using Business.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    public class ModuloUsuarioAdapter:Adapter
    {
        public ModuloUsuario GetPermiso(string modulo)
        {
            ModuloUsuario modulosusuario = new ModuloUsuario();
            try
            {
                this.OpenConnection();

                SqlCommand cmdModuloUsuario = new SqlCommand(@"select * from modulos_usuarios mu
                                                         inner join modulos m on m.id_modulo = mu.id_modulo
                                                         where m.desc_modulo = @modulo", sqlConn);
                cmdModuloUsuario.Parameters.Add("@modulo", SqlDbType.VarChar).Value = modulo;
                SqlDataReader drModulo = cmdModuloUsuario.ExecuteReader();

                if (drModulo.Read())
                {
                    modulosusuario.PermiteBaja = (bool)drModulo["baja"];
                    modulosusuario.PermiteAlta = (bool)drModulo["alta"];
                    modulosusuario.PermiteConsulta = (bool)drModulo["consulta"];
                    modulosusuario.PermiteModificacion = (bool)drModulo["modificacion"];
                }
                else
                {
                    throw new Exception("No se configuraron los permisos");
                }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
               new Exception("Error al recuperar datos de permisos", Ex);
                throw ExcepcionManejada;
            }
            finally { this.CloseConnection(); }
            return modulosusuario;

        }

        public List<Permiso> GetAll()
        {
            var listaUsuario = new List<Permiso>();
            try
            {


                this.OpenConnection();
                SqlCommand command = new SqlCommand(@"select m.id_modulo, m.desc_modulo, mu.id_modulo_usuario, mu.alta, mu.baja, mu.modificacion, mu.consulta from modulos m
                                                      left join modulos_usuarios mu on m.id_modulo = mu.id_modulo", sqlConn);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var permiso = new Permiso();
                    permiso.DescModulo = (string)dr["desc_modulo"];
                    if(dr["id_modulo_usuario"] != System.DBNull.Value)
                    {
                        permiso.idModuloUsuario = (int)dr["id_modulo_usuario"];
                    }
                    permiso.IdModulo = (int)dr["id_modulo"];
                    if (dr["alta"] != System.DBNull.Value)
                    {
                        permiso.PermiteAlta = (bool)dr["alta"];
                    }
                    if (dr["baja"] != System.DBNull.Value)
                    {
                        permiso.PermiteBaja = (bool)dr["baja"];
                    }
                    if (dr["modificacion"] != System.DBNull.Value)
                    {
                        permiso.PermiteModificacion = (bool)dr["modificacion"];
                    }
                    if (dr["consulta"] != System.DBNull.Value)
                    {
                        permiso.PermiteConsulta = (bool)dr["consulta"];
                    }
                    listaUsuario.Add(permiso);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { this.CloseConnection();}
            return listaUsuario;
        }

        protected void Insert(Permiso permiso)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT into modulos_usuarios (id_modulo,id_usuario,alta,baja,consulta,modificacion) VALUES(@idModulo,@idUsuario,@permiteAlta,@permiteBaja,@permiteConsulta,@permiteModificacion) Select @@identity", sqlConn);

                cmdSave.Parameters.Add("@idModulo", SqlDbType.VarChar, 50).Value = permiso.IdModulo;
                cmdSave.Parameters.Add("@idUsuario", SqlDbType.Int).Value = permiso.IdUsuario;
                cmdSave.Parameters.Add("@permiteAlta", SqlDbType.Bit).Value = permiso.PermiteAlta;
                cmdSave.Parameters.Add("@permiteBaja", SqlDbType.Bit).Value = permiso.PermiteBaja;
                cmdSave.Parameters.Add("@permiteConsulta", SqlDbType.Bit).Value = permiso.PermiteConsulta;
                cmdSave.Parameters.Add("@permiteModificacion", SqlDbType.Bit).Value = permiso.PermiteModificacion;
                permiso.idModuloUsuario = decimal.ToInt32((decimal)cmdSave.ExecuteScalar());

            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear Permiso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Update(Permiso permiso)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE usuarios SET id_modulo=@idModulo, id_usuario=@idUsuario, alta=@permiteAlta, baja=@permiteBaja, consulta=@permiteConsulta, modificacion=@permiteModificacion WHERE id_modulo_usuario=@id", sqlConn);
                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = permiso.idModuloUsuario;
                cmdSave.Parameters.Add("@idModulo", SqlDbType.VarChar, 50).Value = permiso.IdModulo;
                cmdSave.Parameters.Add("@idUsuario", SqlDbType.Int).Value = permiso.IdUsuario;
                cmdSave.Parameters.Add("@permiteAlta", SqlDbType.Bit).Value = permiso.PermiteAlta;
                cmdSave.Parameters.Add("@permiteBaja", SqlDbType.Bit).Value = permiso.PermiteBaja;
                cmdSave.Parameters.Add("@permiteConsulta", SqlDbType.Bit).Value = permiso.PermiteConsulta;
                cmdSave.Parameters.Add("@permiteModificacion", SqlDbType.Bit).Value = permiso.PermiteModificacion;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos de permiso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }


    }
}
