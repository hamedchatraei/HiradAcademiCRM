using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Calls;

namespace KonkurCRM.Core.DTOs.Call
{
    public class FollowUpViewModel
    {
        public List<FollowUp> FollowUps { get; set; }
        public List<InformationFollowUpViewModel> InformationFollowUp { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationFollowUpViewModel
    {
        public int FollowUpId { get; set; }
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }

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
        public string StudentNameFamily { get; set; }

    }

    public class UnRegisterFollowUpViewModel
    {
        public List<UnregisteredFollowUp> UnregisteredFollowUps { get; set; }
        public List<InformationUnRegisteredFollowUpViewModel> InformationUnRegisteredFollowUp { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationUnRegisteredFollowUpViewModel
    {
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
        public string StudentNameFamily { get; set; }
    }

    public class AllFollowUpViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentNameFamily { get; set; }
        public DateTime FollowUpDateTime { get; set; }
        public string Subject { get; set; }
        public bool IsFollowedUp { get; set; }
        public string Description { get; set; }
        public bool IsRegister { get; set; }
    }
}
