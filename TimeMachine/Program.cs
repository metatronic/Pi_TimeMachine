using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TimeMachine
{
    class Program
    {
        LogTable logTable = new LogTable(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString);
        TimeMachine timeMachine = new TimeMachine(ConfigurationManager.ConnectionStrings["TrainingConnection"].ConnectionString);
        List<Transact> transactionList = new List<Transact>();

        static void Main(string[] args)
        {
            DateTime dt;
            List<string> paths = new List<string>(ConfigurationManager.AppSettings["paths"].Split(new char[] { ';' }));
            Program p = new Program();
            Console.WriteLine("Enter Choice: \n1)Late Commers \n2)Late Departures \n3)Absentees \n4)Run Update");
            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter date : (yyyy-mm-dd)");
                    dt = Convert.ToDateTime(Console.ReadLine());
                    List<Employee> empList = p.timeMachine.GetLateComerList(dt);
                    Console.WriteLine($"Late comers on : {dt}");
                    foreach (Employee employee in empList)
                    {
                        Console.WriteLine(employee);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter date : (yyyy-mm-dd)");
                    dt = Convert.ToDateTime(Console.ReadLine());
                    List<Employee> empList1 = p.timeMachine.GetLateDeptartureList(dt);
                    Console.WriteLine($"Late departures on: {dt}");
                    foreach (Employee employee in empList1)
                    {
                        Console.WriteLine(employee);
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter date : (yyyy-mm-dd)");
                    dt = Convert.ToDateTime(Console.ReadLine());
                    List<Employee> empList2 = p.timeMachine.GetAbsenteeList(dt);
                    foreach (Employee employee in empList2)
                    {
                        Console.WriteLine(employee);
                    }
                    break;
                case 4:
                    try
                    {
                        p.RunCollector(paths);
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("File update failed, file already updated");
                    }
                    Console.WriteLine("Done");
                    break;
                default:
                    Console.WriteLine("enter correct option");
                    break;
            }
            Console.ReadLine();

        }
        void RunCollector(List<string> paths)
        {
            if (DeserializeDate() != DateTime.Today)
            {
                foreach (string path in paths)
                {
                    ReadGateFile.GetTransactList(ref transactionList, path, ref logTable);
                }
                foreach (Transact item in transactionList)
                {
                    timeMachine.InsertTransaction(item);
                }
            }
            SerializeDate(DateTime.Today);
        }
        void SerializeDate(DateTime dt)
        {
            FileStream f = new FileStream("LastRunDate.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer x = new XmlSerializer(typeof(DateTime));
            x.Serialize(f, dt);
            f.Close();
        }
        DateTime DeserializeDate()
        {
            if (File.Exists("LastRunDate.xml"))
            {
                FileStream f = new FileStream("LastRunDate.xml", FileMode.Open, FileAccess.Read);
                XmlSerializer x = new XmlSerializer(typeof(DateTime));
                DateTime dt;
                dt = (DateTime)x.Deserialize(f);
                f.Close();
                return dt;
            }
            else
            {
                return default;
            }
        }
    }
}
