using KonkurCRM.Core.Convertor;
using KonkurCRM.Core.DTOs.Adviser;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.DTOs.Student;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Calls;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Plans;
using KonkurCRM.DataLayer.Entities.Register;
using KonkurCRM.DataLayer.Entities.Students;
using KonkurCRM.DataLayer.Entities.User;
using KonkurCRM.DataLayer.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HiradAcademiCRM.Web.Controllers
{
    [Authorize]
    public class AdviserPanelController : Controller
    {
        private IStudentService _studentService;
        private IAdviserService _adviserService;
        private IUserService _userService;
        private IRegisterService _registerService;
        private ICallService _callService;
        private ICourseService _courseService;
        private IPlanService _planService;
        private IPayService _payService;

        public AdviserPanelController(IStudentService studentService, IAdviserService adviserService, IUserService userService, IRegisterService registerService, ICallService callService, ICourseService courseService, IPlanService planService, IPayService payService)
        {
            _studentService = studentService;
            _adviserService = adviserService;
            _userService = userService;
            _registerService = registerService;
            _callService = callService;
            _courseService = courseService;
            _planService = planService;
            _payService = payService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Students

        #region ShowStudents

        public IActionResult Students(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);

            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            StudentViewModel student = _studentService.GetAdviserPanelStudents(pageId, adviserId, family, mobile, phone, city, studyId, gradeId);

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            return View(student);
        }

        public IActionResult DeletedStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);

            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            StudentViewModel student = _studentService.GetAdviserPanelDeletedStudents(pageId, adviserId, family, mobile, phone, city, studyId, gradeId);

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            return View(student);
        }

        public IActionResult CanceledStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);

            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            StudentViewModel student = _studentService.GetAdviserPanelCanceledStudents(pageId, adviserId, family, mobile, phone, city, studyId, gradeId);

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            return View(student);
        }

        public IActionResult UnregisteredStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "",
            string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);

            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            StudentViewModel student = _studentService.GetAdviserPanelUnregisteredStudents(pageId, adviserId, family, mobile, phone, city, studyId, gradeId);

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            return View(student);
        }

        [Route("adviserPanel/StudentDetail/{id}/{position}")]
        public IActionResult StudentDetail(int id, int position)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            List<int> studentIds = new List<int>();
            List<InformationAdvisersRegisterViewModel> registerSAdvisers = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviser.AdviserId);
            Student student = _studentService.GetStudentById(id);

            ViewData["Position"] = position;

            if (adviser.ParentId == null)
            {
                List<Adviser> listAdvisers = _adviserService.getAdvisersByParentId(adviser.AdviserId);

                foreach (var listAdviser in listAdvisers)
                {
                    var registersParentAdvisers = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(listAdviser.AdviserId);

                    foreach (var item in registersParentAdvisers)
                    {
                        studentIds.Add(item.StudentId);
                    }
                }

                foreach (var item in registerSAdvisers)
                {
                    studentIds.Add(item.StudentId);
                }

                if (studentIds.Any(s => s == id))
                    return View(student);
            }
            else
            {
                foreach (var item in registerSAdvisers)
                {
                    studentIds.Add(item.StudentId);
                }

                if (studentIds.Any(s => s == id))
                    return View(student);
            }


            return Redirect("/404");
        }

        [Route("adviserPanel/UnRegisteredStudentDetail/{id}")]
        public IActionResult UnRegisteredStudentDetail(int id)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            List<int> studentIds = new List<int>();
            List<InformationStudentViewModel> students = _studentService.ShowUnRegisteredStudentByAdviser().Where(r => r.AdviserId == adviser.AdviserId).ToList();
            Student student = _studentService.GetStudentById(id);

            if (adviser.ParentId == null)
            {
                List<Adviser> listAdvisers = _adviserService.getAdvisersByParentId(adviser.AdviserId);

                foreach (var listAdviser in listAdvisers)
                {
                    List<InformationStudentViewModel> parentStudents = _studentService.ShowUnRegisteredStudentByAdviser().Where(r => r.AdviserId == listAdviser.AdviserId).ToList();

                    foreach (var item in parentStudents)
                    {
                        studentIds.Add(item.StudentId);
                    }
                }

                foreach (var item in students)
                {
                    studentIds.Add(item.StudentId);
                }

                if (studentIds.Any(s => s == id))
                    return View(student);
            }
            else
            {
                foreach (var item in students)
                {
                    studentIds.Add(item.StudentId);
                }

                if (studentIds.Any(s => s == id))
                    return View(student);
            }


            return Redirect("/404");

        }

        #endregion

        #region AddStudent

        [Route("/AdviserPanel/AddStudentViaAdviser/{number}/{isCall}")]
        public IActionResult AddStudentViaAdviser(string number, int isCall)
        {
            ViewData["Number"] = number;
            ViewData["isCall"] = isCall;

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/AddStudentViaAdviser/{number}/{isCall}")]
        public IActionResult AddStudentViaAdviser(UnRegisteredCallAndStudentComplexViewModel complexViewModel)
        {
            if (string.IsNullOrEmpty(complexViewModel.Student.Name))
            {
                complexViewModel.Student.Name = "ثبت نشده";
            }
            if (string.IsNullOrEmpty(complexViewModel.Student.Family))
            {
                complexViewModel.Student.Family = "-";
            }
            if (string.IsNullOrEmpty(complexViewModel.Student.FatherName))
            {
                complexViewModel.Student.FatherName = "-";
            }
            if (complexViewModel.Student.GradeId == 0)
            {
                complexViewModel.Student.GradeId = 1;
            }
            if (complexViewModel.Student.StudyId == 0)
            {
                complexViewModel.Student.StudyId = 1;
            }

            int studentId = _studentService.AddStudent(complexViewModel.Student);

            complexViewModel.UnregisteredCalls.StudentId = studentId;

            _callService.AddUnRegisteredCall(complexViewModel.UnregisteredCalls);

            _callService.DeleteNotCalled(complexViewModel.NotCalled.NotCalledId);

            return Redirect("/AdviserPanel/UnregisteredStudents");
        }

        #endregion

        #region RegisterStudent

        [Route("/AdviserPanel/RegisterStudent/{id}")]
        public IActionResult RegisterStudent(int id)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);

            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            int adviserId = adviser.AdviserId;

            List<SelectListItem> courses = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب برنامه",Value = ""}
            };
            courses.AddRange(_courseService.GetCourseListItems());
            ViewData["courses"] = new SelectList(courses, "Value", "Text", 0);

            List<SelectListItem> plans = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب طرح",Value = ""}
            };
            plans.AddRange(_planService.GetPlanListItem());
            ViewData["plans"] = new SelectList(plans, "Value", "Text", 0);

            List<SelectListItem> payTypes = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "روش پرداخت",Value = ""}
            };
            payTypes.AddRange(_payService.GetPayTypeListItem());
            ViewData["payTypes"] = new SelectList(payTypes, "Value", "Text", 0);

            ViewData["StudentId"] = id;
            ViewData["AdviserId"] = adviserId;

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/RegisterStudent/{id}")]
        public IActionResult RegisterStudent(AddRegisterViewModel addRegister)
        {
            Register register = new Register()
            {
                AdviserId = addRegister.AdviserId,
                StudentId = addRegister.StudentId,
                CourseId = addRegister.CourseId,
                PlanId = addRegister.PlanId,
                PayTypeId = addRegister.PayTypeId,
                RegisterDate = addRegister.RegisterDate,
                Discount = addRegister.Discount,
                FinalAmount = addRegister.FinalAmount,
                IsCancel = false,
                IsDelete = false,
            };
            int registerId = _registerService.AddRegister(register);

            Register_sAdviser registersAdviser = new Register_sAdviser()
            {
                AdviserId = register.AdviserId,
                RegisterId = registerId,
                AdviserTypeId = 2,
                StartDate = register.RegisterDate,
                EndDate = addRegister.RegisterEndDate,
                IsActive = true
            };
            _registerService.AddRegistersAdviser(registersAdviser);

            return Redirect($"/AdviserPanel/StudentDetail/{register.StudentId}/1");
        }

        public int GetAmountOfPlan(int id)
        {
            Plan plan = _planService.GetPlanById(id);

            return plan.Amount;
        }

        public string GetAmountOfPlanToRial(int id)
        {
            Plan plan = _planService.GetPlanById(id);

            return plan.Amount.ToRial();
        }

        #endregion

        #region CancelStudent

        [Route("/AdviserPanel/CancelStudent/{id}")]
        public IActionResult CancelStudent(int id)
        {
            _studentService.CancelStudent(id);

            return Redirect("/AdviserPanel/CanceledStudents");
        }

        #endregion

        #region DeleteStudent

        [Route("/AdviserPanel/DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);

            return Redirect("/AdviserPanel/DeletedStudents");
        }

        #endregion

        #region EditStudent

        #region EditRegisteredStudent

        [Route("/AdviserPanel/EditRegisteredStudent/{id}")]
        public IActionResult EditRegisteredStudent(int id)
        {
            EditStudentAndRegisterComplexViewModel studentAndRegister = new EditStudentAndRegisterComplexViewModel();
            Register register = _registerService.GetRegisterByStudentId(id);
            EditRegisterViewModel editRegister = _registerService.GetDataForEditRegister(register.RegisterId);
            EditStudentViewModel editStudent = _studentService.GetDataForEditStudent(id);
            Register_sAdviser registerSAdviser = _registerService.GetRegistersAdviserByRegisterId(register.RegisterId);
            Register_sAdviser editRegisterSAdviser = _registerService.GetDataForEditRegisterSAdviser(registerSAdviser.RAId);

            studentAndRegister.EditStudent = editStudent;
            studentAndRegister.EditRegister = editRegister;
            studentAndRegister.RegisterSAdviser = editRegisterSAdviser;

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            List<SelectListItem> courses = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب برنامه",Value = ""}
            };
            courses.AddRange(_courseService.GetCourseListItems());
            ViewData["courses"] = new SelectList(courses, "Value", "Text", 0);

            List<SelectListItem> plans = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب طرح",Value = ""}
            };
            plans.AddRange(_planService.GetPlanListItem());
            ViewData["plans"] = new SelectList(plans, "Value", "Text", 0);

            List<SelectListItem> payTypes = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "روش پرداخت",Value = ""}
            };
            payTypes.AddRange(_payService.GetPayTypeListItem());
            ViewData["payTypes"] = new SelectList(payTypes, "Value", "Text", 0);

            return View(studentAndRegister);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditRegisteredStudent/{id}")]
        public IActionResult EditRegisteredStudent(EditStudentAndRegisterComplexViewModel studentAndRegister)
        {
            studentAndRegister.EditRegister.RegisterEndDate = studentAndRegister.RegisterSAdviser.EndDate;
            studentAndRegister.RegisterSAdviser.StartDate = studentAndRegister.EditRegister.RegisterDate;

            if (!ModelState.IsValid)
                return View(studentAndRegister);

            _studentService.EditStudent(studentAndRegister.EditStudent);
            _registerService.EditRegister(studentAndRegister.EditRegister);
            _registerService.EditRegistersAdviser(studentAndRegister.RegisterSAdviser);

            int studentId = studentAndRegister.EditStudent.StudentId;

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }

        #endregion

        #region EditStudent

        [Route("/AdviserPanel/EditStudent/{id}")]
        public IActionResult EditStudent(int id)
        {
            EditStudentViewModel editStudent = _studentService.GetDataForEditStudent(id);

            List<SelectListItem> study = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب رشته",Value = ""}
            };
            study.AddRange(_studentService.GetStudyListItems());
            ViewData["study"] = new SelectList(study, "Value", "Text", 0);

            List<SelectListItem> grade = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مقطع",Value = ""}
            };
            grade.AddRange(_studentService.GetGradeListItems());
            ViewData["grade"] = new SelectList(grade, "Value", "Text", 0);

            return View(editStudent);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditStudent/{id}")]
        public IActionResult EditStudent(EditStudentViewModel editStudent)
        {
            if (!ModelState.IsValid)
                return View();

            _studentService.EditStudent(editStudent);

            int studentId = editStudent.StudentId;

            return Redirect($"/AdviserPanel/UnRegisteredStudentDetail/{studentId}");
        }

        #endregion

        #endregion

        #endregion

        #region Adviser

        public IActionResult Advisers(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string family = "", string mobile = "")
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            AdviserViewModel advisers =
                _adviserService.GetAdvisersForAdviserPanel(pageId, adviserId, filterAdviser, family, mobile);

            List<SelectListItem> adviserList = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            adviserList.AddRange(_adviserService.GetSubAdviserListItems(adviserId));
            ViewData["advisers"] = new SelectList(adviserList, "Value", "Text", 0);

            return View(advisers);
        }

        [Route("/AdviserPanel/AdviserDetail/{id}")]
        public IActionResult AdviserDetail(int id)
        {
            List<int> adviserIds = new List<int>();
            adviserIds.Add(id);
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            int adviserId = adviser.AdviserId;

            AdviserViewModel advisers =
                _adviserService.GetAdvisersForAdviserPanel(1, adviserId);

            Adviser adviserInfo = _adviserService.GetAdviserById(id);

            ViewData["adviserId"] = id;

            foreach (var item in advisers.InformationAdviser)
            {
                adviserIds.Add(item.AdviserId);
            }

            if (adviserIds.Any(a => a == id))
            {
                return View(adviserInfo);
            }

            return Redirect("/404");

        }

        [HttpPost]
        [Route("AdviserPanel/AdviserDetail/{id}")]

        public IActionResult AdviserDetail(int id, int planId, int courseId)
        {
            Adviser adviser = _adviserService.GetAdviserById(id);

            ViewData["adviserId"] = id;
            ViewData["sPlanId"] = planId;
            ViewData["sCourseId"] = courseId;

            return View(adviser);
        }

        [Route("/AdviserPanel/AdvisersStudent/{id}")]
        public IActionResult AdvisersStudent(int id, int pageId = 1, string family = "", int planId = 0, int courseId = 0)
        {
            RegisterViewModel register = _registerService.GetAdviserStudents(pageId, id, family, planId, courseId);

            List<int> adviserIds = new List<int>();
            adviserIds.Add(id);
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;
            AdviserViewModel advisers =
                _adviserService.GetAdvisersForAdviserPanel(1, adviserId);

            ViewData["AdviserId"] = id;

            foreach (var item in advisers.InformationAdviser)
            {
                adviserIds.Add(item.AdviserId);
            }

            List<SelectListItem> courses = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب برنامه",Value = ""}
            };
            courses.AddRange(_courseService.GetCourseListItems());
            ViewData["courses"] = new SelectList(courses, "Value", "Text", 0);

            List<SelectListItem> plans = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب طرح",Value = ""}
            };
            plans.AddRange(_planService.GetPlanListItem());
            ViewData["plans"] = new SelectList(plans, "Value", "Text", 0);

            if (adviserIds.Any(a => a == id))
            {
                return View(register);
            }

            return Redirect("/404");
        }

        #endregion

        #region Pay

        #region ShowPays

        public IActionResult ShowPays(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "", string payDate = "", string fromDate = "", string toDate = "",
            string pursuitCode = "", int fromAmount = 0, int toAmount = 0)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            PayViewModel pays = _payService.GetPaysForAdviserPanel(pageId, adviserId, filterAdviser, studentFamily, payDate, fromDate, toDate, pursuitCode, fromAmount, toAmount);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            advisers.AddRange(_adviserService.GetSubAdviserListItems(adviserId));
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(pays);
        }

        #endregion

        #region ShowCheque

        public IActionResult ShowCheque(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "", string chequeDate = "", string fromDate = "", string toDate = "", string owner = "", int fromAmount = 0,
            int toAmount = 0, string chequeNumber = "")
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            ChequeViewModel cheques = _payService.GetChequesForAdviserPanel(pageId, adviserId, filterAdviser, studentFamily, chequeDate, fromDate, toDate, owner, fromAmount, toAmount, chequeNumber);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            advisers.AddRange(_adviserService.GetSubAdviserListItems(adviserId));
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(cheques);
        }

        #endregion

        #region ShowInstallments

        public IActionResult ShowInstallments(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "", string installmentDate = "",
            string fromDate = "", string toDate = "", int fromAmount = 0, int toAmount = 0)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            InstallmentViewModel installment = _payService.GetInstallmentsForAdviserPanel(pageId, adviserId, filterAdviser, studentFamily, installmentDate,
                fromDate, toDate, fromAmount, toAmount);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            advisers.AddRange(_adviserService.GetSubAdviserListItems(adviserId));
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(installment);
        }

        #endregion

        #region AddPay

        #region PayCash

        [Route("/AdviserPanel/AddPayCash/{id}/{studentId}")]
        public IActionResult AddPayCash(int id, int studentId)
        {
            ViewData["RegisterId"] = id;
            ViewData["StudentId"] = studentId;

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/AddPayCash/{id}/{studentId}")]
        public IActionResult AddPayCash(AddPayViewModel pay)
        {
            Pay addPay = new Pay()
            {
                RegisterId = pay.RegisterId,
                Amount = pay.Amount,
                PayDate = pay.PayDate,
                PursuitCode = pay.PursuitCode,
                Description = pay.Description
            };

            _payService.AddPay(addPay);

            int studentId = pay.StudentId;

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }

        #endregion

        #region Cheque

        [Route("/AdviserPanel/AddCheque/{id}/{studentId}")]
        public IActionResult AddCheque(int id, int studentId)
        {
            ViewData["RegisterId"] = id;
            ViewData["StudentId"] = studentId;

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/AddCheque/{id}/{studentId}")]
        public IActionResult AddCheque(AddChequeViewModel addCheque)
        {
            Cheque cheque = new Cheque()
            {
                RegisterId = addCheque.RegisterId,
                Amount = addCheque.Amount,
                ChequeDate = addCheque.ChequeDate,
                Owner = addCheque.Owner,
                ChequeNumber = addCheque.ChequeNumber,
                AccountNumber = addCheque.AccountNumber,
                ForWho = addCheque.ForWho,
                Description = addCheque.Description,
                IsPass = false
            };

            _payService.AddCheque(cheque);

            int studentId = addCheque.StudentId;

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }

        #endregion

        #region Installment

        [Route("/AdviserPanel/AddInstallment/{id}/{studentId}")]
        public IActionResult AddInstallment(int id, int studentId)
        {
            ViewData["RegisterId"] = id;
            ViewData["StudentId"] = studentId;

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/AddInstallment/{id}/{studentId}")]
        public IActionResult AddInstallment(AddInstallmentViewModel addInstallment)
        {
            Installment installment = new Installment()
            {
                RegisterId = addInstallment.RegisterId,
                InstallmentDate = addInstallment.InstallmentDate,
                Amount = addInstallment.Amount,
                Description = addInstallment.Description,
                IsPayed = false
            };

            _payService.AddInstallment(installment);

            int studentId = addInstallment.StudentId;

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }

        #endregion

        #region PayInstallment

        [Route("/AdviserPanel/PayInstallment/{id}/{studentId}")]
        public IActionResult PayInstallment(int id, int studentId)
        {
            Installment installment = _payService.GetInstallmentById(id);

            _payService.PayInstallment(id);

            AddPayViewModel addPay = new AddPayViewModel();

            addPay.RegisterId = installment.RegisterId;
            addPay.Amount = installment.Amount;
            addPay.PayDate = DateTime.Now;

            ViewData["StudentId"] = studentId;

            return View(addPay);
        }

        [HttpPost]
        [Route("/AdviserPanel/PayInstallment/{id}/{studentId}")]
        public IActionResult PayInstallment(AddPayViewModel pay)
        {
            Pay addPay = new Pay()
            {
                RegisterId = pay.RegisterId,
                Amount = pay.Amount,
                PayDate = pay.PayDate,
                PursuitCode = pay.PursuitCode,
                Description = pay.Description
            };

            _payService.AddPay(addPay);

            int studentId = pay.StudentId;

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }

        #endregion

        #region PassCheque

        [Route("/AdviserPanel/PassCheque/{id}/{studentId}")]
        public IActionResult PassCheque(int id, int studentId)
        {
            _payService.PassCheque(id);

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }

        #endregion

        #endregion

        #region EditPay

        #region EditPayCash

        [Route("/AdviserPanel/EditPayCash/{id}/{studentId}")]
        public IActionResult EditPayCash(int id, int studentId)
        {
            EditPayViewModel editPay = _payService.GetDataForEditPay(id);

            ViewData["StudentId"] = studentId;

            return View(editPay);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditPayCash/{id}/{studentId}")]
        public IActionResult EditPayCash(EditPayViewModel editPay)
        {
            _payService.EditPay(editPay);

            return Redirect("/AdviserPanel/ShowPays");
        }

        #endregion

        #region EditCheque

        [Route("/AdviserPanel/EditCheque/{id}")]
        public IActionResult EditCheque(int id)
        {
            Cheque cheque = _payService.GetDataForEditCheque(id);

            return View(cheque);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditCheque/{id}")]
        public IActionResult EditCheque(Cheque cheque)
        {
            _payService.EditCheque(cheque);

            return Redirect("/AdviserPanel/ShowCheque");
        }

        #endregion

        #region EditInstallment

        [Route("/AdviserPanel/EditInstallment/{id}")]
        public IActionResult EditInstallment(int id)
        {
            Installment installment = _payService.GetDataForEditInstallment(id);

            return View(installment);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditInstallment/{id}")]
        public IActionResult EditInstallment(Installment installment)
        {
            _payService.EditInstallment(installment);

            return Redirect("/AdviserPanel/ShowInstallments");
        }

        #endregion

        #endregion

        #region DeletePay

        #region DeletePayCash

        public IActionResult DeletePayCash(int id)
        {
            _payService.DeletePay(id);

            return Redirect("/AdviserPanel/ShowPays");
        }

        #endregion

        #region DeleteCheque

        public IActionResult DeleteCheque(int id)
        {
            _payService.DeleteCheque(id);

            return Redirect("/AdviserPanel/ShowCheque");
        }

        #endregion

        #region DeleteInstallment

        public IActionResult DeleteInstallment(int id)
        {
            _payService.DeleteInstallment(id);

            return Redirect("/AdviserPanel/ShowInstallments");
        }

        #endregion

        #endregion

        #endregion

        #region FollowUp

        #region RegisteredFollowUp

        public IActionResult FollowUps(int pageId = 1, int adviserId = 0, string studentFamily = "", string followUpDate = "")
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            FollowUpViewModel followUp = _callService.GetFollowUps(pageId, adviserId, studentFamily, followUpDate);

            return View(followUp);
        }

        public IActionResult AddFollowUp()
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> registers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            registers.AddRange(_adviserService.GetRegisterAdviserForFollowUpListItems(adviserId));
            ViewData["registers"] = new SelectList(registers, "Value", "Text", 0);

            return View();
        }

        [HttpPost]
        public IActionResult AddFollowUp(FollowUp followUp)
        {

            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            followUp.AdviserId = adviserId;

            _callService.AddFollowUp(followUp);

            return Redirect("/AdviserPanel/FollowUps");
        }

        [Route("/AdviserPanel/EditFollowUp/{id}")]
        public IActionResult EditFollowUp(int id)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> registers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            registers.AddRange(_adviserService.GetRegisterAdviserForFollowUpListItems(adviserId));
            ViewData["registers"] = new SelectList(registers, "Value", "Text", 0);


            FollowUp followUp = _callService.GetDataForEditFollowUp(id);

            return View(followUp);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditFollowUp/{id}")]
        public IActionResult EditFollowUp(FollowUp followUp)
        {
            _callService.EditFollowUp(followUp);

            return Redirect("/AdviserPanel/FollowUps");
        }

        [Route("/AdviserPanel/DeleteFollowUp/{id}")]
        public IActionResult DeleteFollowUp(int id)
        {
            _callService.DeleteFollowUp(id);

            return Redirect("/AdviserPanel/FollowUps");
        }

        #endregion

        #region UnRegisteredFollowUp

        public IActionResult UnRegisteredFollowUps(int pageId = 1, int adviserId = 0,
            string studentFamily = "", string followUpDate = "")
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            var unRegisterFollowUp =
                _callService.GetUnRegisterFollowUps(pageId, adviserId, studentFamily, followUpDate);

            return View(unRegisterFollowUp);
        }

        public IActionResult AddUnRegisterFollowUp()
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> students = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            students.AddRange(_studentService.GetStudentsForUnRegisterFollowUpListItems(adviserId));
            ViewData["students"] = new SelectList(students, "Value", "Text", 0);

            return View();
        }

        [HttpPost]
        public IActionResult AddUnRegisterFollowUp(UnregisteredFollowUp unregisteredFollowUp)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            unregisteredFollowUp.AdviserId = adviserId;

            _callService.AddUnRegisteredFollowUp(unregisteredFollowUp);

            return Redirect("/AdviserPanel/UnRegisteredFollowUps");
        }

        [Route("/AdviserPanel/EditUnRegisterFollowUp/{id}")]
        public IActionResult EditUnRegisterFollowUp(int id)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> students = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            students.AddRange(_studentService.GetStudentsForUnRegisterFollowUpListItems(adviserId));
            ViewData["students"] = new SelectList(students, "Value", "Text", 0);

            var unRegisteredFollowUp = _callService.GetDataForEditUnRegisteredFollowUp(id);

            return View(unRegisteredFollowUp);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditUnRegisterFollowUp/{id}")]
        public IActionResult EditUnRegisterFollowUp(UnregisteredFollowUp unregisteredFollowUp)
        {
            _callService.EditUnRegisteredFollowUp(unregisteredFollowUp);

            return Redirect("/AdviserPanel/UnRegisteredFollowUps");
        }

        public IActionResult DeleteUnRegisteredFollowUp(int id)
        {
            _callService.DeleteUnregisteredFollowUp(id);

            return Redirect("/AdviserPanel/UnRegisteredFollowUps");
        }

        #endregion

        #endregion

        #region Calls

        #region RegisteredCalls

        public IActionResult RegisteredCalls(int pageId = 1, int adviserId = 0, string studentFamily = "", string callDate = "")
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            CallViewModel call = _callService.GetCalls(pageId, adviserId, studentFamily, callDate);

            return View(call);
        }

        [Route("/AdviserPanel/AddRegisteredCall/{id}/{isCall}")]
        public IActionResult AddRegisteredCall(int id,int isCall)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> registers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            registers.AddRange(_adviserService.GetRegisterAdviserForFollowUpListItems(adviserId));
            ViewData["registers"] = new SelectList(registers, "Value", "Text", 0);

            ViewData["FollowUpId"] = id;
            ViewData["AdviserId"] = adviserId;
            ViewData["IsCall"] = isCall;

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/AddRegisteredCall/{id}/{isCall}")]
        public IActionResult AddRegisteredCall(AddCallViewModel addCall)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            Call call = new Call()
            {
                RegisterId = addCall.RegisterId,
                AdviserId = adviserId,
                Subject = addCall.Subject,
                Description = addCall.Description,
                CallDate = addCall.CallDate,
                CallTime = addCall.CallTime,
                IsCall = addCall.IsCall,
                IsDelete = false
            };

            _callService.AddCall(call);

            _callService.FollowedFollowUp(addCall.FollowUpId);

            return Redirect("/AdviserPanel/RegisteredCalls");
        }

        [Route("/AdviserPanel/EditRegisteredCall/{id}")]
        public IActionResult EditRegisteredCall(int id)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> registers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            registers.AddRange(_adviserService.GetRegisterAdviserForFollowUpListItems(adviserId));
            ViewData["registers"] = new SelectList(registers, "Value", "Text", 0);

            Call call = _callService.GetDataForEditCall(id);
            
            return View(call);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditRegisteredCall/{id}")]
        public IActionResult EditRegisteredCall(Call call)
        {
            _callService.EditCall(call);

            return Redirect("/AdviserPanel/RegisteredCalls");
        }

        [Route("/AdviserPanel/DeleteRegisteredCall/{id}")]
        public IActionResult DeleteRegisteredCall(int id)
        {
            _callService.DeleteCall(id);

            return Redirect("/AdviserPanel/RegisteredCalls");
        }

        #endregion

        #region UnRegisteredCalls

        public IActionResult UnRegisteredCalls(int pageId = 1, int adviserId = 0, string studentFamily = "", string callDate = "")
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            adviserId = adviser.AdviserId;

            UnregisteredCallsViewModel unRegisteredCall =
                _callService.GetUnregisteredCalls(pageId, adviserId, studentFamily, callDate);

            return View(unRegisteredCall);
        }

        [Route("/AdviserPanel/AddUnRegisteredCall/{id}/{isCall}")]
        public IActionResult AddUnRegisteredCall(int id, int isCall)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> students = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            students.AddRange(_studentService.GetStudentsForUnRegisterFollowUpListItems(adviserId));
            ViewData["students"] = new SelectList(students, "Value", "Text", 0);

            ViewData["FollowUpId"] = id;
            ViewData["AdviserId"] = adviserId;
            ViewData["IsCall"] = isCall;

            return View();
        }

        [HttpPost]
        [Route("/AdviserPanel/AddUnRegisteredCall/{id}/{isCall}")]
        public IActionResult AddUnRegisteredCall(AddUnRegisteredCallViewModel addUnregisteredCalls)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            UnregisteredCalls call = new UnregisteredCalls()
            {
                AdviserId = adviserId,
                Subject = addUnregisteredCalls.Subject,
                Description = addUnregisteredCalls.Description,
                CallDate = addUnregisteredCalls.CallDate,
                CallTime = addUnregisteredCalls.CallTime,
                IsCall = addUnregisteredCalls.IsCall,
                IsDelete = false,
                StudentId = addUnregisteredCalls.StudentId
            };

            _callService.AddUnRegisteredCall(call);

            _callService.FollowedUpUnRegisteredFollowUp(addUnregisteredCalls.UnRegisteredFollowUpId);

            return Redirect("/AdviserPanel/UnRegisteredCalls");
        }

        [Route("/AdviserPanel/EditUnRegisteredCall/{id}")]
        public IActionResult EditUnRegisteredCall(int id)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);
            int adviserId = adviser.AdviserId;

            List<SelectListItem> students = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            students.AddRange(_studentService.GetStudentsForUnRegisterFollowUpListItems(adviserId));
            ViewData["students"] = new SelectList(students, "Value", "Text", 0);

            UnregisteredCalls unRegisteredCall = _callService.GetDataForEditUnregisteredCalls(id);

            return View(unRegisteredCall);
        }

        [HttpPost]
        [Route("/AdviserPanel/EditUnRegisteredCall/{id}")]
        public IActionResult EditUnRegisteredCall(UnregisteredCalls unregisteredCalls)
        {
            _callService.EditUnregisteredCalls(unregisteredCalls);

            return Redirect("/AdviserPanel/UnRegisteredCalls");
        }

        [Route("/AdviserPanel/DeleteUnRegisteredCall/{id}")]
        public IActionResult DeleteUnRegisteredCall(int id)
        {
            _callService.DeleteUnregisteredCall(id);

            return Redirect("/AdviserPanel/UnRegisteredCalls");
        }

        #endregion

        #endregion

        #region ChooseAdviser

        public IActionResult ChooseAdviser()
        {
            List<SelectListItem> registers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب ثبت نام",Value = ""}
            };
            registers.AddRange(_registerService.GetAllRegistersListItems());
            ViewData["registers"] = new SelectList(registers, "Value", "Text", 0);

            List<SelectListItem> groups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب نوع مشاور",Value = ""}
            };
            groups.AddRange(_adviserService.GetGroupAdviserListItems());
            ViewData["groups"] = new SelectList(groups, "Value", "Text", 0);

            List<SelectListItem> allAdvisersForChoose = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب مشاور",Value = ""}
            };
            allAdvisersForChoose.AddRange(_adviserService.GetAdviserListItems());
            ViewData["allAdvisersForChoose"] = new SelectList(allAdvisersForChoose, "Value", "Text", 0);

            return View();
        }

        [HttpPost]
        public IActionResult ChooseAdviser(Register_sAdviser registerSAdviser)
        {
            _registerService.DeActiveRegistersAdviser(registerSAdviser.RegisterId,registerSAdviser.AdviserTypeId,registerSAdviser);

            registerSAdviser.IsActive = true;

            _registerService.AddRegistersAdviser(registerSAdviser);

            Register register = _registerService.GetRegisterById(registerSAdviser.RegisterId);
            int studentId = register.StudentId;

            return Redirect($"/AdviserPanel/StudentDetail/{studentId}/1");
        }


        #endregion

        #region Events

        public IActionResult Events()
        {
            return View();
        }

        #endregion
    }
}
