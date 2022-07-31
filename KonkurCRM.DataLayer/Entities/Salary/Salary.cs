using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;

namespace KonkurCRM.DataLayer.Entities.Salary
{
    public class Salary
    {
        public Salary()
        {
            
        }

        public int SalaryId { get; set; }
        public int SalaryTypeId { get; set; }
        public int AdviserId { get; set; }
        public int Amount { get; set; }
        public DateTime PaySalaryDate { get; set; }
        public string Description { get; set; }

        #region Relations

        public SalaryType SalaryType { get; set; }
        public Adviser Adviser { get; set; }

        #endregion
    }
}
