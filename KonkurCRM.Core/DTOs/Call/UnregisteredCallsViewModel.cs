﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Calls;

namespace KonkurCRM.Core.DTOs.Call
{
    public class UnregisteredCallsViewModel
    {
        public List<UnregisteredCalls> UnregisteredCalls { get; set; }
        public List<InformationUnregisteredCalls> InformationUnregisteredCalls { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationUnregisteredCalls
    {
        public int UnregisteredCallsId { get; set; }
        public int AdviserId { get; set; }
        public int StudentId { get; set; }
        public DateTime CallDate { get; set; }
        public TimeSpan CallTime { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsCall { get; set; }
        public string AdviserNameFamily { get; set; }
        public string StudentNameFamily { get; set; }

    }

    public class AddUnRegisteredCallViewModel
    {
        public int AdviserId { get; set; }
        public int StudentId { get; set; }

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
        public int UnRegisteredFollowUpId { get; set; }
    }
}
