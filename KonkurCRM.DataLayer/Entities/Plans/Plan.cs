using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Plans
{
    public class Plan
    {
        public Plan()
        {

        }


        [Key]
        public int PlanId { get; set; }

        [Display(Name = "عنوان طرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PlanTitle { get; set; }

        [Display(Name = "قیمت طرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "مدت زمان طرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Length { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        public virtual List<Register.Register> Registers { get; set; }
        public List<AttributePlan> AttributePlans { get; set; }

        #endregion

    }
}
