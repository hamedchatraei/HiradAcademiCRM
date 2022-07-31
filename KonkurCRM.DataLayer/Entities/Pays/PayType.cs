using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Pays
{
    public class PayType
    {
        public PayType()
        {
            
        }

        [Key]
        public int PayTypeId { get; set; }

        [Display(Name = "روش پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PayTypeTitle { get; set; }

        #region Relations

        public List<Register.Register> Registers { get; set; }

        #endregion
    }
}
