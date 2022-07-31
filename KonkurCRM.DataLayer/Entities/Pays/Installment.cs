using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Pays
{
    public class Installment
    {
        public Installment()
        {
            
        }

        [Key]
        public int InstallmentId { get; set; }
        public int RegisterId { get; set; }

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


        #region Relations

        public Register.Register Register { get; set; }

        #endregion

    }
}
