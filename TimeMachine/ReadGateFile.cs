using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    static class ReadGateFile
    {
        static public void GetTransactList(ref List<Transact> tList, string filename, ref LogTable lt)
        {
            string line;

            StreamReader file = new StreamReader(filename);
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    string[] words = line.Split(' ');
                    tList.Add(new Transact()
                    {
                        GateNo = Convert.ToInt32(words[0]),//0 is gateno
                        TransactNo = Convert.ToInt32(words[1]),//1 is transactionno
                        EmpID = Convert.ToInt32(words[2]),//2 is employee id
                        DateTrans = DateTime.Parse(string.Format($"{words[3]} {words[4]} {words[5]}")),//3 4 5 makes the date and time seperated by space
                        Operation = words[6]//6 is operation type
                    });
                }
                catch (Exception)
                {
                    lt.InsertLog(line);
                }
            }
        }
    }
}
