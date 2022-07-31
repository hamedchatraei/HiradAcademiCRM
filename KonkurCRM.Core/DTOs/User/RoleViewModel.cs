using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.Core.DTOs.User
{
    public class EditRoleViewModel
    {
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public List<int> RolePermissions { get; set; }
    }
}
