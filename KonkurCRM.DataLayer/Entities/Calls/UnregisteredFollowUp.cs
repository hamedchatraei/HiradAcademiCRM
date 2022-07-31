using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Students;

namespace KonkurCRM.DataLayer.Entities.Calls
{
    public class UnregisteredFollowUp
    {
        public UnregisteredFollowUp()
        {
            
        }

        [Key]
        public int UnregisteredFollowUpId { get; set; }
        public int AdviserId { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "زمان پیگیری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime FollowUpDateTime { get; set; }

        [Display(Name = "موضوع پیگیری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Subject { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }
        public bool IsFollowedUp { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        public Adviser Adviser { get; set; }
        public Student Student { get; set; }

        #endregion

    }
}
