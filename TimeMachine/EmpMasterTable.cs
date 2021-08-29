using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    class EmpMasterTable
    {
        SqlConnection connection;
        public EmpMasterTable(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public bool IsEmployeeExist(int empNo)
        {
            try
            {
                string sql = "select EMPNO from EMPMASTER E where E.EMPID = @eid";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("eid", empNo);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int? empNoDb = command.ExecuteScalar() as int?;
                if (empNoDb is null)
                {
                    return false;
                }
                else
                {
                    return true;
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
