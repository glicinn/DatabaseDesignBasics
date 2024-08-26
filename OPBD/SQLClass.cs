using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace OPBD
{
    class SQLClass
    {
        public SqlConnection connection = new SqlConnection("Data Source = DESKTOP-KLPPS5B\\SQLEXPRESS; Initial Catalog = Ministry_Of_Internal_Affairs; Integrated Security = true; ");
        SqlCommand command = new SqlCommand();
        public DataTable table = new DataTable();
        public SqlDependency dependancy = new SqlDependency();
        public enum act { select, manipulation };

        public void SQLExecute(string SQLQuery, act act)
        {
            command.Connection = connection;
            command.CommandText = SQLQuery;
            command.Notification = null;
            switch (act)
            {
                case act.select:
                    dependancy.AddCommandDependency(command);
                    SqlDependency.Start(connection.ConnectionString);
                    connection.Open();
                    table.Load(command.ExecuteReader());
                    connection.Close();
                    break;
                case act.manipulation:
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    break;
            }
        }


    }
}

