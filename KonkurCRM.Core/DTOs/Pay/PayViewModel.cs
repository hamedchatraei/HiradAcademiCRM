using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Pays;

namespace KonkurCRM.Core.DTOs.Pay
{
    public class PayViewModel
    {
        public List<DataLayer.Entities.Pays.Pay> Pays { get; set; }
        public List<InformationPayViewModel> InformationPay { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationPayViewModel
    {
        public int PayId { get; set; }
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }
        public string StudentNameFamily { get; set; }
        public string AdviserNameFamily { get; set; }
        public int Amount { get; set; }
        public DateTime PayDate { get; set; }
        public string PursuitCode { get; set; }
        public string Description { get; set; }

    }

    public class InformationPayByRegisterViewModel
    {
        public int RegisterId { get; set; }
        public int AdviserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int AdviserTypeId { get; set; }
        public int PayId { get; set; }
        public int Amount { get; set; }
        public DateTime PayDate { get; set; }
        public string PursuitCode { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public int PlanId { get; set; }
        public int PercentAmount { get; set; }

    }

    public class AddPayViewModel
    {
        public int RegisterId { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime PayDate { get; set; }

        [Display(Name = "کد پیگیری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PursuitCode { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }
    }

    public class EditPayViewModel
    {
        public int PayId { get; set; }
        public int RegisterId { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime PayDate { get; set; }

        [Display(Name = "کد پیگیری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PursuitCode { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }
    }

    public class ChequeViewModel
    {
        public List<Cheque> Cheques { get; set; }
        public List<InformationChequeViewModel> InformationCheque { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationChequeViewModel
    {
        public int ChequeId { get; set; }
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }
        public string StudentNameFamily { get; set; }
        public string AdviserNameFamily { get; set; }
        public int Amount { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Owner { get; set; }
        public string ChequeNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ForWho { get; set; }
        public string Description { get; set; }
        public bool IsPass { get; set; }
    }

    public class InformationChequeByRegisterViewModel
    {
        public int RegisterId { get; set; }
        public int AdviserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int AdviserTypeId { get; set; }
        public int ChequeId { get; set; }
        public int Amount { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Owner { get; set; }
        public string ChequeNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ForWho { get; set; }
        public string Description { get; set; }
        public bool IsPass { get; set; }
        public int CourseId { get; set; }
        public int PlanId { get; set; }
        public int PercentAmount { get; set; }
    }

    public class AddChequeViewModel
    {
        public int RegisterId { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "تاریخ چک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime ChequeDate { get; set; }

        [Display(Name = "صاحب حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Owner { get; set; }

        [Display(Name = "شماره چک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ChequeNumber { get; set; }

        [Display(Name = "شماره حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string AccountNumber { get; set; }

        [Display(Name = "در وجه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ForWho { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }
        public bool IsPass { get; set; }
    }

    public class InstallmentViewModel
    {
        public List<Installment> Installments { get; set; }
        public List<InformationInstallmentViewModel> InformationInstallment { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationInstallmentViewModel
    {
        public int InstallmentId { get; set; }
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }
        public string StudentNameFamily { get; set; }
        public string AdviserNameFamily { get; set; }
        public DateTime InstallmentDate { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public bool IsPayed { get; set; }
    }

    public class AddInstallmentViewModel
    {
        public int RegisterId { get; set; }
        public int StudentId { get; set; }

        [Display(Name = "تاریخ قسط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime InstallmentDate { get; set; }

        [Display(Name = "مبلغ قسط")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        public bool IsPayed { get; set; }
    }
}
