using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;

namespace KonkurCRM.DataLayer.Entities.Salary
{
    public class SalaryType
    {
        public SalaryType()
        {
            
        }

        public int SalaryTypeId { get; set; }
        public string Title { get; set; }

        #region Relations

        public List<AdviserInvoice> AdviserInvoices { get; set; }

        #endregion
    }
}
