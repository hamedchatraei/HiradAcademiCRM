using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Settings
{
    public class StartDayOfMonth
    {
        public StartDayOfMonth()
        {

        }

        [Key]
        public int SdmId { get; set; }
        public int SdmValue { get; set; }

    }
}
