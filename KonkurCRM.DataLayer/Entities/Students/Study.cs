using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Students
{
    public class Study
    {
        public Study()
        {

        }


        [Key]
        public int StudyId { get; set; }

        [Display(Name = "رشته")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string StudyTitle { get; set; }

        #region Relations

        public virtual List<Student> Students { get; set; }

        #endregion

    }
}
