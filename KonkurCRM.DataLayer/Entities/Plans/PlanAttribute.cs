using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Plans
{
    public class PlanAttribute
    {
        public PlanAttribute()
        {

        }


        [Key]
        public int PlanAttributeId { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }
        

        #region Relations

        public List<AttributePlan> AttributePlans { get; set; }

        #endregion
    }
}
