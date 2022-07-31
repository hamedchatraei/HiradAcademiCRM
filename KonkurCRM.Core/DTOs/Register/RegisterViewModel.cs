using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Register;

namespace KonkurCRM.Core.DTOs.Register
{
    public class RegisterViewModel
    {
        public List<DataLayer.Entities.Register.Register> Registers { get; set; }
        public List<InformationRegisterViewModel> InformationRegister { get; set; }
        public List<InformationAdvisersRegisterViewModel> InformationAdvisersRegister { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationRegisterViewModel
    {
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }
        public int PlanId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Discount { get; set; }
        public int FinalAmount { get; set; }
        public string StudentNameFamily { get; set; }
        public string AdviserNameFamily { get; set; }
        public string PlanTitle { get; set; }
        public string CourseTitle { get; set; }
        public int PayTypeId { get; set; }
        public string PayTypeTitle { get; set; }
    }

    public class RegistersAdviserViewModel
    {
        public List<Register_sAdviser> RegisterSAdvisers { get; set; }
        public List<InformationRegistersAdviserViewModel> InformationRegistersAdviser { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationRegistersAdviserViewModel
    {
        public int RegisterId { get; set; }
        public int AdviserId { get; set; }
        public int ParentId { get; set; }
        public string AdviserNameFamily { get; set; }
        public string JobMobile { get; set; }
        public string PersonalMobile { get; set; }
        public string Study { get; set; }
        public string AccountNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int AdviserTypeId { get; set; }
        public string AdviserType { get; set; }

    }

    public class InformationAdvisersRegisterViewModel
    {
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string StudentNameFamily { get; set; }
        public int AdviserId { get; set; }
        public int AdviserTypeId { get; set; }
        public string Type { get; set; }
        public int PlanId { get; set; }
        public int CourseId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsCancel { get; set; }
    }

    public class AddRegisterViewModel
    {
        public int StudentId { get; set; }
        public int AdviserId { get; set; }

        [Display(Name = "طرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PlanId { get; set; }

        [Display(Name = "برنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseId { get; set; }

        [Display(Name = "روش پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PayTypeId { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Discount { get; set; }

        [Display(Name = "هزینه ی نهایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int FinalAmount { get; set; }

        [Display(Name = "تاریخ پایان قرارداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime RegisterEndDate { get; set; }

        public bool IsDelete { get; set; }
        public bool IsCancel { get; set; }
    }

    public class EditRegisterViewModel
    {
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }

        [Display(Name = "طرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PlanId { get; set; }

        [Display(Name = "برنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseId { get; set; }

        [Display(Name = "روش پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PayTypeId { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Discount { get; set; }

        [Display(Name = "هزینه ی نهایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int FinalAmount { get; set; }

        [Display(Name = "تاریخ پایان قرارداد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime RegisterEndDate { get; set; }
        
    }
    
}
