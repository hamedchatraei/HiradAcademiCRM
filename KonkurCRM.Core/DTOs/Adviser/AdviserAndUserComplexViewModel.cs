using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.User;

namespace KonkurCRM.Core.DTOs.Adviser
{
    public class AdviserAndUserComplexViewModel
    {
        public EditAdviserViewModel EditAdviserViewModel { get; set; }
        public EditUserViewModel EditUserViewModel { get; set; }
    }
}
