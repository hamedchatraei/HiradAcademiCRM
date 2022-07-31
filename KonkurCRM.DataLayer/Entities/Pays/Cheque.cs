using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Pays
{
    public class Cheque
    {
        public Cheque()
        {

        }

        [Key]
        public int ChequeId { get; set; }
        public int RegisterId { get; set; }

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

        #region Relations

        public virtual Register.Register Register { get; set; }

        #endregion

    }
}
