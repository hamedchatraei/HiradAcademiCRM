using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;

namespace KonkurCRM.DataLayer.Entities.Calls
{
    public class NotCalled
    {
        public NotCalled()
        {
            
        }

        [Key]
        public int NotCalledId { get; set; }
        public int AdviserId { get; set; }

        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Number { get; set; }

        [Display(Name = "تاریخ اضافه شدن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime AddDate { get; set; }

        public bool IsDedicated { get; set; }

        #region Relations

        public Adviser Adviser { get; set; }

        #endregion

    }
}
