using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    class TimeMachine
    {
        SqlConnection connection;
        public TimeMachine(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public List<Employee> GetLateComerList(DateTime dt)
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                string sql = "select E.EMPID,E.EMPNAME from EMPMASTER E where E.EMPID in (select T.EMPID from TIMEMACHINE T where CAST(DATETRANS as DATE) = @date and OPERATION='Entry' and convert(time,DATETRANS) > '9:30 AM')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("date", dt);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        EmpID = Convert.ToInt32(reader["EMPID"]),
                        EmpName = reader["EMPNAME"].ToString()
                    };
                    empList.Add(emp);

                }
                return empList;

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
        public List<Employee> GetLateDeptartureList(DateTime dt)
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                string sql = "select E.EMPID,E.EMPNAME from EMPMASTER E where E.EMPID in (select T.EMPID from TIMEMACHINE T where CAST(DATETRANS as DATE) = @date and OPERATION='Exit' and convert(time,DATETRANS) > '7:30 PM')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("date", dt);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        EmpID = Convert.ToInt32(reader["EMPID"]),
                        EmpName = reader["EMPNAME"].ToString()
                    };
                    empList.Add(emp);

                }
                return empList;

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
        public List<Employee> GetAbsenteeList(DateTime dt)
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                string sql = "select E.EMPID,E.EMPNAME from EMPMASTER E where E.EMPID not in (select T.EMPID from TIMEMACHINE T where CAST(DATETRANS as DATE) = @date and OPERATION='Entry')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("date", dt);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        EmpID = Convert.ToInt32(reader["EMPID"]),
                        EmpName = reader["EMPNAME"].ToString()
                    };
                    empList.Add(emp);

                }
                return empList;

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
        public bool InsertTransaction(Transact t)
        {
            try
            {
                List<Employee> empList = new List<Employee>();
                string sql = "insert into TIMEMACHINE(GATENO,TRANSNO,EMPID,DATETRANS,OPERATION) values(@gtno,@trno,@empid,@date,@opt)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("gtno", t.GateNo);
                command.Parameters.AddWithValue("trno", t.TransactNo);
                command.Parameters.AddWithValue("empid", t.EmpID);
                command.Parameters.AddWithValue("date", t.DateTrans);
                command.Parameters.AddWithValue("opt", t.Operation);

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
