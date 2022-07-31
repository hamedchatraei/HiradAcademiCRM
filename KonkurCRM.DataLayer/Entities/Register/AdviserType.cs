using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KonkurCRM.DataLayer.Entities.Register
{
    public class AdviserType
    {
        public AdviserType()
        {
            
        }

        public int AdviserTypeId { get; set; }
        public string Type { get; set; }

        #region Relations

        public List<Register_sAdviser> RegisterSAdvisers { get; set; }

        #endregion
    }
}
