using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Advisers
{
    public class GroupAdviser
    {
        public GroupAdviser()
        {

        }


        [Key]
        public int GroupId { get; set; }
        public int? ParentId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string GroupTitle { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public virtual List<GroupAdviser> GroupAdvisers { get; set; }
        public virtual List<AdviserGroup> AdviserGroups { get; set; }

        #endregion
    }
}
