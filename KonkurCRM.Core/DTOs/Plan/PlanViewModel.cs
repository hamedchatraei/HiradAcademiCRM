using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Plans;

namespace KonkurCRM.Core.DTOs.Plan
{
    public class PlanViewModel
    {
        public List<DataLayer.Entities.Plans.Plan> Plans { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class PlanAttributeViewModel
    {
        public List<PlanAttribute> PlanAttributes { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class EditPlanViewModel
    {
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

        public List<int> AttributePlans { get; set; }
    }
}
