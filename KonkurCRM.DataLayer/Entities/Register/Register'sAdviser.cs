using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;

namespace KonkurCRM.DataLayer.Entities.Register
{
    public class Register_sAdviser
    {
        [Key]
        public int RAId { get; set; }
        public int RegisterId { get; set; }
        public int AdviserId { get; set; }
        public int AdviserTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }


        #region Relations

        public Adviser Adviser { get; set; }
        public Register Register { get; set; }
        public AdviserType AdviserType { get; set; }

        #endregion
    }
}
