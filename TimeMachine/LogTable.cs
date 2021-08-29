using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    class LogTable
    {
        SqlConnection connection;
        public LogTable(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public bool InsertLog(string logEntry)
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                string sql = "insert into [LOG](RECDETAILS) values(@recval)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("recval", logEntry);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int rownumber = command.ExecuteNonQuery();
                if (rownumber > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
