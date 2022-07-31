using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.DataLayer.Entities.Courses
{
    public class Course
    {
        public Course()
        {

        }


        [Key]
        public int CourseId { get; set; }

        [Display(Name = "عنوان برنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string CourseTitle { get; set; }

        [Display(Name = "مبدا برنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Origin { get; set; }

        [Display(Name = "تاریخ برنامه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime CourseDate { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public bool IsDelete { get; set; }
        public int PercentAmount { get; set; }

        #region Relations

        public virtual List<Register.Register> Registers { get; set; }

        #endregion
    }
}
