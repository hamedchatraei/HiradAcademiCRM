using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.DataLayer.Entities.Register;

namespace KonkurCRM.Core.DTOs.Student
{
    public class StudentViewModel
    {
        public List<DataLayer.Entities.Students.Student> Students { get; set; }
        public List<InformationStudentViewModel> InformationStudents { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationStudentViewModel
    {
        public int StudentId { get; set; }
        public int GradeId { get; set; }
        public int StudyId { get; set; }
        public string Grade { get; set; }
        public string Study { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public int RegisterId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsCancel { get; set; }
        public int AdviserId { get; set; }
    }

    public class StudentComparer : IEqualityComparer<InformationStudentViewModel>
    {
        public bool Equals(InformationStudentViewModel x, InformationStudentViewModel y)
        {
            //آیا دقیقا یک وهله هستند؟
            if (Object.ReferenceEquals(x, y)) return true;

            //آیا یکی از وهله‌ها نال است؟
            if (Object.ReferenceEquals(x, null) ||
                Object.ReferenceEquals(y, null))
                return false;

            return x.StudentId == y.StudentId;
        }
        public int GetHashCode([DisallowNull] InformationStudentViewModel obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashTextual = obj.StudentId == null ? 0 : obj.StudentId.GetHashCode();
            int hashDigital = obj.StudentId.GetHashCode();
            return hashTextual ^ hashDigital;
        }
    }

    public class EditStudentViewModel
    {
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
    }

    public class EditStudentAndRegisterComplexViewModel
    {
        public EditStudentViewModel EditStudent { get; set; }
        public EditRegisterViewModel EditRegister { get; set; }
        public Register_sAdviser RegisterSAdviser { get; set; }
    }
}
