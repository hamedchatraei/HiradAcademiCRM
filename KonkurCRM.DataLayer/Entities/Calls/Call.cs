using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Calls
{
    public class Call
    {
        public Call()
        {

        }


        [Key]
        public int CallId { get; set; }
        public int RegisterId { get; set; }
        public int AdviserId { get; set; }

        [Display(Name = "زمان تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CallDate { get; set; }

        [Display(Name = "تایم تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan CallTime { get; set; }

        [Display(Name = "موضوع تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Subject { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }
        public bool IsCall { get; set; }
        public bool IsDelete { get; set; }

        #region Relations

        public virtual Register.Register Register { get; set; }

        #endregion

    }
}
