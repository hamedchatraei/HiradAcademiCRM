using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;
using Microsoft.AspNetCore.Http;

namespace KonkurCRM.Core.DTOs.Adviser
{
    public class AdviserViewModel
    {
        public List<DataLayer.Entities.Advisers.Adviser> Advisers { get; set; }
        public List<InformationAdviserViewModel> InformationAdviser { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationAdviserViewModel
    {
        public int AdviserId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string JobMobile { get; set; }
        public string PersonalMobile { get; set; }
        public string Study { get; set; }
        public string AccountNumber { get; set; }
        public bool IsDelete { get; set; }
        public int ParentId { get; set; }
    }

    public class AdviserComparer : IEqualityComparer<InformationAdviserViewModel>
    {
        public bool Equals(InformationAdviserViewModel x, InformationAdviserViewModel y)
        {
            //آیا دقیقا یک وهله هستند؟
            if (Object.ReferenceEquals(x, y)) return true;

            //آیا یکی از وهله‌ها نال است؟
            if (Object.ReferenceEquals(x, null) ||
                Object.ReferenceEquals(y, null))
                return false;

            return x.AdviserId == y.AdviserId;
        }
        public int GetHashCode([DisallowNull] InformationAdviserViewModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashTextual = obj.AdviserId == null ? 0 : obj.AdviserId.GetHashCode();
            int hashDigital = obj.AdviserId.GetHashCode();
            return hashTextual ^ hashDigital;
        }
    }

    public class GroupAdviserViewModel
    {
        public List<GroupAdviser> GroupAdvisers { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InvoiceViewModel
    {
        public List<AdviserInvoice> AdviserInvoices { get; set; }
        public List<AdviserInvoiceViewModel> AdviserInvoiceViewModels { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class AdviserInvoiceViewModel
    {
        public int AdviserInvoiceId { get; set; }
        public int AdviserId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int TotalAmount { get; set; }
        public DateTime PaySalaryDate { get; set; }
        public int SalaryTypeId { get; set; }
        public string Title { get; set; }
        public string AdviserNameFamily { get; set; }
    }

    public class EditAdviserViewModel
    {
        public int AdviserId { get; set; }
        public int? ParentId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Family { get; set; }

        [Display(Name = "موبایل کاری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string JobMobile { get; set; }

        [Display(Name = "موبایل شخصی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PersonalMobile { get; set; }

        [Display(Name = "رشته")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Study { get; set; }

        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(16, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string AccountNumber { get; set; }
        public List<int> AdviserGroups { get; set; }
        public int UserId { get; set; }
    }

    public class EditAdviserInvoiceViewModel
    {
        public int AdviserInvoiceId { get; set; }
        public int AdviserId { get; set; }
        public int SalaryTypeId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int TotalAmount { get; set; }
        public DateTime PaySalaryDate { get; set; }
    }
}
