using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Students
{
    public class Grade
    {
        public Grade()
        {

        }


        [Key]
        public int GradeId { get; set; }

        [Display(Name = "مقطع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string GradeTitle { get; set; }

        #region Relations

        public virtual List<Student> Students { get; set; }

        #endregion

    }
}
