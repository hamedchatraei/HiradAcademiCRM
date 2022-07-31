using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Calls;

namespace KonkurCRM.DataLayer.Entities.Students
{
    public class Student
    {
        public Student()
        {

        }


        [Key]
        public int StudentId { get; set; }
        public int StudyId { get; set; }
        public int GradeId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Family { get; set; }

        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string FatherName { get; set; }

        [Display(Name = "موبایل دانش آموز")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile1 { get; set; }

        [Display(Name = "موبایل پدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile2 { get; set; }

        [Display(Name = "موبایل مادر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(11, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile3 { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string City { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        public virtual Study Study { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual List<Register.Register> Registers { get; set; }
        public List<UnregisteredCalls> UnregisteredCalls { get; set; }
        public List<UnregisteredFollowUp> UnregisteredFollowUps { get; set; }

        #endregion

    }
}
