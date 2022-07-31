using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Calls;
using KonkurCRM.DataLayer.Entities.Courses;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Plans;
using KonkurCRM.DataLayer.Entities.Students;

namespace KonkurCRM.DataLayer.Entities.Register
{
    public class Register
    {
        public Register()
        {

        }


        [Key]
        public int RegisterId { get; set; }
        public int StudentId { get; set; }
        public int AdviserId { get; set; }
        public int PlanId { get; set; }
        public int CourseId { get; set; }
        public int PayTypeId { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Discount { get; set; }

        [Display(Name = "هزینه ی نهایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int FinalAmount { get; set; }

        public bool IsDelete { get; set; }
        public bool IsCancel { get; set; }

        #region Relations

        public virtual List<Call> Calls { get; set; }
        public virtual List<FollowUp> FollowUps { get; set; }
        public virtual List<Pay> Pays { get; set; }
        public virtual List<Cheque> Cheques { get; set; }
        public virtual Student Student { get; set; }
        public virtual Adviser Adviser { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Course Course { get; set; }
        public List<Register_sAdviser> RegisterSAdvisers { get; set; }
        public PayType PayType { get; set; }
        public List<Installment> Installments { get; set; }

        #endregion

    }
}
