using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMachine
{
    class Transact
    {
        public int GateNo { get; set; }
        public int TransactNo { get; set; }
        public int EmpID { get; set; }
        public DateTime DateTrans { get; set; }
        public string Operation { get; set; }

        public override string ToString()
        {
            return string.Format($"Gateno {GateNo} TransactNo {TransactNo} EmpID {EmpID} Date {DateTrans} OPT {Operation}");
        }
    }
}
