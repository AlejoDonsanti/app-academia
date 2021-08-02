using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Data.Database
{
    public class Adapter
    {
        const string consKeyDefaultCnnString = "ConnStringLocal";
        //private SqlConnection sqlConnection = new SqlConnection("ConnectionString;");
        public SqlConnection sqlConn;
        protected void OpenConnection()
        {
            var conexion = ConfigurationManager.ConnectionStrings[consKeyDefaultCnnString].ConnectionString;
            this.sqlConn = new SqlConnection(conexion);
            this.sqlConn.Open();
            
        }

        protected void CloseConnection()
        {
            sqlConn.Close();
            sqlConn = null;
        }

        protected SqlDataReader ExecuteReader(String commandText)
        {
            throw new Exception("Metodo no implementado");
        }
    }
}
