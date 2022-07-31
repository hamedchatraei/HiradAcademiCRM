using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Calls;
using KonkurCRM.DataLayer.Entities.Register;

namespace KonkurCRM.DataLayer.Entities.Advisers
{
    public class Adviser
    {
        public Adviser()
        {

        }

        [Key]
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

        public int UserId { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public virtual List<Adviser> Advisers { get; set; }
        public virtual List<AdviserGroup> AdviserGroups { get; set; }
        public virtual List<Register.Register> Registers { get; set; }
        public List<Register_sAdviser> RegisterSAdvisers { get; set; }
        public List<UnregisteredCalls> UnregisteredCalls { get; set; }
        public List<UnregisteredFollowUp> UnregisteredFollowUps { get; set; }
        public List<AdviserInvoice> AdviserInvoices { get; set; }
        public List<NotCalled> NotCalleds { get; set; }

        #endregion
    }
}
