using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Calls;

namespace KonkurCRM.Core.DTOs.Call
{
    public class NotCalledViewModel
    {
        public List<NotCalled> NotCalleds { get; set; }
        public List<InformationNotCalledViewModel> InformationNotCalled { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationNotCalledViewModel
    {
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
    }
}
