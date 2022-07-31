using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Calls;

namespace KonkurCRM.Core.DTOs.Call
{
    public class UnRegisteredCallAndStudentComplexViewModel
    {
        public DataLayer.Entities.Students.Student Student { get; set; }
        public UnregisteredCalls UnregisteredCalls { get; set; }
        public NotCalled NotCalled { get; set; }
    }
}
