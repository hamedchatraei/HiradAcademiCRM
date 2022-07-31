using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Plans
{
    public class AttributePlan
    {
        public AttributePlan()
        {
            
        }

        [Key]
        public int APId { get; set; }
        public int PlanId { get; set; }
        public int PlanAttributeId { get; set; }


        #region Relations

        public Plan Plan { get; set; }
        public PlanAttribute PlanAttribute { get; set; }

        #endregion
    }
}
