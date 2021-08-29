using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }

        public override string ToString()
        {
            return string.Format($"EmpNO: {EmpID} EmpName: {EmpName}");
        }
    }
}
