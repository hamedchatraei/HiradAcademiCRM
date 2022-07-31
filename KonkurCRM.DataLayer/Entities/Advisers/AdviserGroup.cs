using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Advisers
{
    public class AdviserGroup
    {
        public AdviserGroup()
        {

        }


        [Key]
        public int AdviserGroupId { get; set; }
        public int AdviserId { get; set; }
        public int GroupId { get; set; }

        #region Relations

        public virtual Adviser Adviser { get; set; }
        public virtual GroupAdviser GroupAdviser { get; set; }

        #endregion
    }
}
