using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EnvioEmailSMS
{
    class MCCommand
    {
        public SqlCommand command = new SqlCommand();
        public SqlConnection connection = new SqlConnection();

        public string CommandText
        {
            get { return command.CommandText; }
            set { command.CommandText = value; }
        }
        
        private IDbConnection Connection
        {
            get { return command.Connection; }
            set { connection = command.Connection; }
        }

        public int ExecuteNonQuery()
        {
            return command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            if (command.Connection == null)
            {
                throw new InvalidOperationException("La conexión no se ha inicializado.");
            }
            return command.ExecuteScalar();
        }
    }
}
