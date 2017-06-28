using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance_RolicGoronok
{
    class Garbs
    {
        public int MalfunId;
        public string Malfun { get; set; }

        public int EmployeeId;
        public string Employee { get; set; }

        public int ServecId;
        public string Servec { get; set; }


        public Garbs(int MalfunId, string Malfun, int EmployeeId, string Employee, int ServecId, string Servec)
        {
            this.MalfunId = MalfunId;
            this.Malfun = Malfun;
            this.EmployeeId = EmployeeId;
            this.Employee = Employee;
            this.ServecId = ServecId;
            this.Servec = Servec;
        }//Garbs
    }
}
