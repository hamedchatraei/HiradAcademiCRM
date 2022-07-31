using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Salary;

namespace KonkurCRM.DataLayer.Entities.Advisers
{
    public class AdviserInvoice
    {
        public AdviserInvoice()
        {
            
        }

        public int AdviserInvoiceId { get; set; }
        public int AdviserId { get; set; }
        public int SalaryTypeId { get; set; }
        public int Amount { get; set; }
        public int TotalAmount { get; set; }
        public DateTime PaySalaryDate { get; set; }
        public string Description { get; set; }

        #region Relations

        public Adviser Adviser { get; set; }
        public SalaryType SalaryType { get; set; }

        #endregion
    }
}
