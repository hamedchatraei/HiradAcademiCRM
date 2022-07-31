using ClosedXML.Excel;
using KonkurCRM.Core.DTOs.Adviser;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.Core.DTOs.Course;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.DTOs.Plan;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.DTOs.Student;
using KonkurCRM.Core.DTOs.User;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Calls;
using KonkurCRM.DataLayer.Entities.Courses;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Plans;
using KonkurCRM.DataLayer.Entities.Register;
using KonkurCRM.DataLayer.Entities.Salary;
using KonkurCRM.DataLayer.Entities.Students;
using KonkurCRM.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HiradAcademiCRM.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        private IAdviserService _adviserService;
        private IStudentService _studentService;
        private ICourseService _courseService;
        private IPlanService _planService;
        private IUserService _userService;
        private IPermissionService _permissionService;
        private IPayService _payService;
        private IRegisterService _registerService;
        private ICallService _callService;

        public AdminPanelController(IAdviserService adviserService, IStudentService studentService, ICourseService courseService, IPlanService planService, IUserService userService, IPermissionService permissionService, IPayService payService, IRegisterService registerService, ICallService callService)
        {
            _adviserService = adviserService;
            _studentService = studentService;
            _courseService = courseService;
            _planService = planService;
            _userService = userService;
            _permissionService = permissionService;
            _payService = payService;
            _registerService = registerService;
            _callService = callService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Advisers

        #region ShowAdvisers

        public IActionResult Advisers(int pageId = 1, string family = "", string mobile = "", int groupId = 0)
        {
            AdviserViewModel adviser = _adviserService.GetAdvisers(pageId, family, mobile, groupId);

            List<SelectListItem> groups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            groups.AddRange(_adviserService.GetGroupAdviserListItems());
            ViewData["groupAdviser"] = new SelectList(groups, "Value", "Text", 0);

            ViewData["groupId"] = groupId;

            return View(adviser);
        }

        public IActionResult DeletedAdvisers(int pageId = 1, string family = "", string mobile = "", int groupId = 0)
        {
            AdviserViewModel adviser = _adviserService.GetDeletedAdvisers(pageId, family, mobile, groupId);

            List<SelectListItem> groups = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            groups.AddRange(_adviserService.GetGroupAdviserListItems());
            ViewData["groupAdviser"] = new SelectList(groups, "Value", "Text", 0);

            ViewData["groupId"] = groupId;

            return View(adviser);
        }

        [Route("adminpanel/AdviserDetail/{id}")]

        public IActionResult AdviserDetail(int id)
        {
            Adviser adviser = _adviserService.GetAdviserById(id);

            ViewData["adviserId"] = id;

            return View(adviser);
        }

        [HttpPost]
        [Route("adminpanel/AdviserDetail/{id}")]

        public IActionResult AdviserDetail(int id, int planId, int courseId)
        {
            Adviser adviser = _adviserService.GetAdviserById(id);

            ViewData["adviserId"] = id;
            ViewData["sPlanId"] = planId;
            ViewData["sCourseId"] = courseId;

            return View(adviser);
        }

        [Route("/AdminPanel/AdvisersStudent/{id}")]
        public IActionResult AdvisersStudent(int id, int pageId = 1, string family = "", int planId = 0, int courseId = 0)
        {
            RegisterViewModel register = _registerService.GetAdviserStudents(pageId, id, family, planId, courseId);

            ViewData["AdviserId"] = id;

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

            return View(register);
        }

        #endregion

        #region AddAdviser

        public IActionResult AddAdviser()
        {
            List<SelectListItem> adviser = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب سرگروه",Value = ""}
            };
            adviser.AddRange(_adviserService.GetAdviserListItems());
            ViewData["AdvisersParent"] = new SelectList(adviser, "Value", "Text", 0);

            return View();
        }

        [HttpPost]
        public IActionResult AddAdviser(List<int> selectedGroups, Adviser adviser, List<int> selectedRoles, CreateUserViewModel user)
        {
            if (!ModelState.IsValid)
                return View();



            //Add Adviser
            int adviserId = _adviserService.AddAdviser(adviser);

            //Add Groups To Adviser
            _adviserService.AddGroupToAdviser(selectedGroups, adviserId);

            //Add User
            user.UserAliasName = adviser.Name + ' ' + adviser.Family;
            user.Mobile = adviser.JobMobile;

            int userId = _userService.AddUserFromAdmin(user);

            //Add Roles To User
            _permissionService.AddRolesToUser(selectedRoles, userId);

            // Add UserID To Adviser
            EditAdviserViewModel editAdviser = new EditAdviserViewModel()
            {
                AdviserId = adviserId,
                Name = adviser.Name,
                Family = adviser.Family,
                JobMobile = adviser.JobMobile,
                PersonalMobile = adviser.PersonalMobile,
                ParentId = adviser.ParentId,
                Study = adviser.Study,
                AccountNumber = adviser.AccountNumber,
                UserId = userId
            };

            _adviserService.EditAdviser(editAdviser);

            return Redirect($"/adminPanel/AdviserDetail/{adviserId}");
        }

        #endregion

        #region EditAdviser

        [Route("adminpanel/EditAdviser/{id}")]
        public IActionResult EditAdviser(int id)
        {
            AdviserAndUserComplexViewModel adviserAndUser = new AdviserAndUserComplexViewModel();

            EditAdviserViewModel editAdviser = _adviserService.GetDataForEditAdviser(id);

            adviserAndUser.EditAdviserViewModel = editAdviser;

            string userAliasName = editAdviser.Name + ' ' + editAdviser.Family;

            KonkurCRM.DataLayer.Entities.User.User user = _userService.GetUserByUserAliasName(userAliasName);

            EditUserViewModel editUser = _userService.GetUserForShowInEditMode(user.UserId);

            adviserAndUser.EditUserViewModel = editUser;

            List<SelectListItem> adviser = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب سرگروه",Value = ""}
            };
            adviser.AddRange(_adviserService.GetAdviserListItems());
            ViewData["AdvisersParent"] = new SelectList(adviser, "Value", "Text", 0);


            return View(adviserAndUser);
        }

        [HttpPost]
        [Route("adminpanel/EditAdviser/{id}")]
        public IActionResult EditAdviser(AdviserAndUserComplexViewModel adviserAndUser, List<int> selectedGroups, List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
                return View(adviserAndUser);

            adviserAndUser.EditUserViewModel.UserAliasName = adviserAndUser.EditAdviserViewModel.Name + ' ' + adviserAndUser.EditAdviserViewModel.Family;
            adviserAndUser.EditUserViewModel.Mobile = adviserAndUser.EditAdviserViewModel.JobMobile;

            _adviserService.EditAdviser(adviserAndUser.EditAdviserViewModel);

            _adviserService.UpdateAdvisersGroup(adviserAndUser.EditAdviserViewModel.AdviserId, selectedGroups);

            _userService.EditUserFromAdmin(adviserAndUser.EditUserViewModel);

            _permissionService.EditRolesUser(adviserAndUser.EditUserViewModel.UserId, selectedRoles);

            return Redirect($"/adminpanel/AdviserDetail/{adviserAndUser.EditAdviserViewModel.AdviserId}");
        }

        #endregion

        #region DeleteAdviser

        [Route("/adminpanel/DeleteAdviser/{id}")]
        public IActionResult DeleteAdviser(int id)
        {
            _adviserService.DeleteAdviser(id);

            return Redirect("/adminpanel/Advisers");
        }

        #endregion

        #endregion

        #region Students

        #region ShowStudents

        public IActionResult Students(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            StudentViewModel student = _studentService.GetStudents(pageId, family, mobile, phone, city, studyId, gradeId);

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

        public IActionResult DeletedStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            StudentViewModel student = _studentService.GetDeletedStudents(pageId, family, mobile, phone, city, studyId, gradeId);

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

        public IActionResult CanceledStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            StudentViewModel student = _studentService.GetCanceledStudents(pageId, family, mobile, phone, city, studyId, gradeId);

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

        public IActionResult UnregisteredStudents(int pageId = 1, string family = "", string mobile = "",
            string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            StudentViewModel student = _studentService.GetUnregisteredStudents(pageId, family, mobile, phone, city, studyId, gradeId);

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

        [Route("adminpanel/StudentDetail/{id}/{position}")]
        public IActionResult StudentDetail(int id, int position)
        {
            Student student = _studentService.GetStudentById(id);

            ViewData["Position"] = position;

            return View(student);
        }

        [Route("adminpanel/UnRegisteredStudentDetail/{id}")]
        public IActionResult UnRegisteredStudentDetail(int id)
        {
            Student student = _studentService.GetStudentById(id);

            return View(student);
        }

        #endregion

        #region CancelStudent

        [Route("/AdminPanel/CancelStudent/{id}")]
        public IActionResult CancelStudent(int id)
        {
            _studentService.CancelStudent(id);

            return Redirect("/AdminPanel/CanceledStudents");
        }

        #endregion

        #region DeleteStudent

        [Route("/AdminPanel/DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);

            return Redirect("/AdminPanel/DeletedStudents");
        }

        #endregion

        #endregion

        #region Pays

        #region ShowSalaries

        [Route("adminpanel/Salaries")]
        public IActionResult Salaries(int pageId = 1, int adviserId = 0, string fromDate = "", string toDate = "")
        {
            InvoiceViewModel invoice = _adviserService.showAdviserInvoices(pageId, adviserId, fromDate, toDate);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(invoice);
        }

        #endregion

        #region AddSalary

        public IActionResult AddSalary()
        {
            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            List<SelectListItem> salaryType = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            salaryType.AddRange(_adviserService.GetSalaryTypeListItems());
            ViewData["salaryType"] = new SelectList(salaryType, "Value", "Text", 0);


            return View();
        }

        [HttpPost]
        public IActionResult AddSalary(AdviserInvoice adviserInvoice)
        {
            List<AdviserInvoice> adviserInvoices = _adviserService.getInvoices();

            _adviserService.AddInvoice(adviserInvoice);

            return Redirect("/AdminPanel/Salaries");
        }

        #endregion

        #region EditSalary

        [Route("/AdminPanel/EditSalary/{id}")]
        public IActionResult EditSalary(int id)
        {
            EditAdviserInvoiceViewModel editAdviserInvoice = _adviserService.GetDataForEditAdviserInvoice(id);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            List<SelectListItem> salaryType = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            salaryType.AddRange(_adviserService.GetSalaryTypeListItems());
            ViewData["salaryType"] = new SelectList(salaryType, "Value", "Text", 0);


            return View(editAdviserInvoice);
        }

        [HttpPost]
        [Route("/AdminPanel/EditSalary/{id}")]
        public IActionResult EditSalary(EditAdviserInvoiceViewModel editAdviserInvoice)
        {
            _adviserService.EditAdviserInvoice(editAdviserInvoice);

            return Redirect("/AdminPanel/Salaries");
        }

        #endregion

        #region DeleteSalary

        [Route("/AdminPanel/DeleteSalary/{id}")]
        public IActionResult DeleteSalary(int id)
        {
            _adviserService.DeleteInvoice(id);

            return Redirect("/AdminPanel/Salaries");
        }

        #endregion

        #region ShowPays

        public IActionResult ShowPays(int pageId = 1, int adviserId = 0, string studentFamily = "", string payDate = "", string fromDate = "", string toDate = "",
            string pursuitCode = "", int fromAmount = 0, int toAmount = 0)
        {
            PayViewModel pays = _payService.GetPays(pageId, adviserId, studentFamily, payDate, fromDate, toDate, pursuitCode, fromAmount, toAmount);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(pays);
        }

        #endregion

        #region ShowCheque

        public IActionResult ShowCheque(int pageId = 1, int adviserId = 0, string studentFamily = "", string chequeDate = "", string fromDate = "", string toDate = "", string owner = "", int fromAmount = 0,
            int toAmount = 0, string chequeNumber = "")
        {
            ChequeViewModel cheques = _payService.GetCheques(pageId, adviserId, studentFamily, chequeDate, fromDate, toDate, owner, fromAmount, toAmount, chequeNumber);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(cheques);
        }

        #endregion

        #region ShowInstallments

        public IActionResult ShowInstallments(int pageId = 1, int adviserId = 0, string studentFamily = "", string installmentDate = "",
            string fromDate = "", string toDate = "", int fromAmount = 0, int toAmount = 0)
        {
            InstallmentViewModel installment = _payService.GetInstallments(pageId, adviserId, studentFamily, installmentDate,
                fromDate, toDate, fromAmount, toAmount);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه مشاوران",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(installment);
        }

        #endregion

        #endregion

        #region Courses

        #region ShowCourses

        [Route("adminpanel/Courses")]
        public IActionResult Courses(int pageId = 1, string courseDate = "", string title = "")
        {
            CourseViewModel courses = _courseService.GetCourses(pageId, courseDate, title);

            return View(courses);
        }

        public IActionResult DeletedCourses(int pageId = 1, string courseDate = "", string title = "")
        {
            CourseViewModel courses = _courseService.GetDeletedCourse(pageId, courseDate, title);

            return View(courses);
        }

        [Route("adminpanel/CourseDetail/{id}")]
        public IActionResult CourseDetail(int id)
        {
            Course course = _courseService.GetCourseById(id);

            ViewData["RangeDate"] = 1;

            return View(course);
        }


        [HttpPost]
        [Route("adminpanel/CourseDetail/{id}")]
        public IActionResult CourseDetail(int id, int rangeDate)
        {
            Course course = _courseService.GetCourseById(id);

            ViewData["RangeDate"] = rangeDate;

            return View(course);
        }

        #endregion

        #region AddCourse

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            int courseId = _courseService.AddCourse(course);

            return Redirect($"/adminpanel/CourseDetail/{courseId}");
        }

        #endregion

        #region EditCourse

        [Route("/adminpanel/EditCourse/{id}")]
        public IActionResult EditCourse(int id)
        {
            Course course = _courseService.GetDataForEditCourse(id);

            return View(course);
        }

        [HttpPost]
        [Route("/adminpanel/EditCourse/{id}")]
        public IActionResult EditCourse(Course course)
        {
            _courseService.EditCourse(course);

            return Redirect($"/adminpanel/CourseDetail/{course.CourseId}");
        }

        #endregion

        #region DeleteCourse

        [Route("/adminpanel/DeleteCourse/{id}")]
        public IActionResult DeleteCourse(int id)
        {
            _courseService.DeleteCourse(id);

            return Redirect("/adminpanel/Courses");
        }

        #endregion

        #endregion

        #region Plans

        #region ShowPlans

        [Route("adminpanel/Plans")]
        public IActionResult Plans(int pageId = 1, string planTitle = "")
        {
            PlanViewModel plan = _planService.GetPlans(pageId, planTitle);

            return View(plan);
        }

        public IActionResult DeletedPlans(int pageId = 1, string planTitle = "")
        {
            PlanViewModel plan = _planService.GetDeletedPlans(pageId, planTitle);

            return View(plan);
        }

        [Route("adminpanel/PlanDetail/{id}")]
        public IActionResult PlanDetail(int id)
        {
            Plan plan = _planService.GetPlanById(id);

            ViewData["RangeDatePlan"] = 1;

            return View(plan);
        }

        [HttpPost]
        [Route("adminpanel/PlanDetail/{id}")]
        public IActionResult PlanDetail(int id, int rangeDatePlan)
        {
            Plan plan = _planService.GetPlanById(id);

            ViewData["RangeDatePlan"] = 1;

            return View(plan);
        }

        #endregion

        #region AddPlan

        public IActionResult AddPlan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPlan(Plan plan, List<int> selectedAttrs)
        {
            if (!ModelState.IsValid)
                return View();


            // Add Plan
            int planId = _planService.AddPlan(plan);

            //Add Attribute To plan
            _planService.AddAttributeToPlan(selectedAttrs, planId);

            return Redirect($"/adminPanel/PlanDetail/{planId}");
        }

        #endregion

        #region EditPlan

        [Route("/adminPanel/EditPlan/{id}")]
        public IActionResult EditPlan(int id)
        {
            EditPlanViewModel plan = _planService.GetDataForEditPlan(id);

            return View(plan);
        }

        [HttpPost]
        [Route("/adminPanel/EditPlan/{id}")]
        public IActionResult EditPlan(EditPlanViewModel editPlan, List<int> selectedAttrs)
        {
            if (!ModelState.IsValid)
                return View(editPlan);

            _planService.EditPlan(editPlan);

            _planService.UpdateAttributesPlan(selectedAttrs, editPlan.PlanId);

            return Redirect($"/adminPanel/PlanDetail/{editPlan.PlanId}");
        }

        #endregion

        #region DeletePlan

        [Route("/adminPanel/DeletePlan/{id}")]
        public IActionResult DeletePlan(int id)
        {
            _planService.DeletePlan(id);

            return Redirect("/adminPanel/Plans");
        }

        #endregion

        #region PlanAttributes

        #region ShowPlanAttribute

        public IActionResult PlanAttributes(int pageId = 1)
        {
            PlanAttributeViewModel planAttribute = _planService.GetPlanAttributes(pageId);

            return View(planAttribute);
        }

        #endregion

        #region AddPlanAttribute

        [Route("/AdminPanel/AddPlanAttribute")]
        public IActionResult AddPlanAttribute(string planAttrTitle)
        {
            PlanAttribute planAttribute = new PlanAttribute()
            {
                Title = planAttrTitle
            };

            _planService.AddPlanAttribute(planAttribute);

            return Redirect("/AdminPanel/PlanAttributes");
        }

        #endregion

        #region EditPlanAttribute


        [Route("/AdminPanel/EditPlanAttribute/{id}")]
        public IActionResult EditPlanAttribute(int id, string planAttrTitle)
        {
            PlanAttribute planAttribute = _planService.GetPlanAttributeById(id);

            planAttribute.Title = planAttrTitle;

            _planService.EditPlanAttribute(planAttribute);

            return Redirect("/AdminPanel/PlanAttributes");
        }

        #endregion

        #region DeletePlanAttribute

        [Route("AdminPanel/DeletePlanAttribute/{id}")]
        public IActionResult DeletePlanAttribute(int id)
        {
            _planService.DeleteAttributePlanByPlanAttributeId(id);

            _planService.DeletePlanAttribute(id);

            return Redirect("/AdminPanel/PlanAttributes");
        }

        #endregion

        #endregion

        #endregion

        #region CallNumbers

        public IActionResult NotCalledNumbers(int pageId = 1, string addDate = "")
        {
            NotCalledViewModel notCalled = _callService.GetNotCalleds(pageId, addDate);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View(notCalled);
        }

        public IActionResult AddCallNumber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCallNumber(IFormFile file)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            Adviser adviser = _adviserService.GetAdviserByUserId(user.UserId);

            int adviserId = adviser.AdviserId;

            try
            {
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowno = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    var test = row.Cell(1).Value.ToString();
                    if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                    {
                        break;
                    }

                    NotCalled notCalled = new NotCalled();

                    notCalled.Number = row.Cell(1).Value.ToString();
                    notCalled.AdviserId = adviserId;
                    notCalled.AddDate = DateTime.Now;

                    List<string> listNumber = new List<string>();
                    List<InformationNotCalledViewModel> notCalledList = _callService.ShowAllNotCalled();

                    foreach (var item in notCalledList)
                    {
                        listNumber.Add(item.Number);
                    }

                    if (!listNumber.Contains(notCalled.Number))
                        _callService.AddNotCalled(notCalled);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Redirect("/AdminPanel/NotCalledNumbers");
        }


        public IActionResult DedicateNumberToAdviser(List<InformationNotCalledViewModel> info,int adviserId)
        {
            foreach (var item in info)
            {
                NotCalled notCalled = new NotCalled()
                {
                    AdviserId = adviserId,
                    Number = item.Number,
                    AddDate = item.AddDate,
                    NotCalledId = item.NotCalledId,
                    IsDedicated = true
                };

                _callService.EditNotCalled(notCalled);
            }

            return Redirect("/AdminPanel/NotCalledNumbers");
        }
        

        public IActionResult DedicateNumberToAdviserByOnce()
        {
            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", 0);

            return View();
        }

        [HttpPost]
        public IActionResult DedicateNumberToAdviserByOnce(NotCalled notCalled)
        {
            notCalled.IsDedicated = true;

            _callService.AddNotCalled(notCalled);

            List<SelectListItem> advisers = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "همه",Value = ""}
            };
            advisers.AddRange(_adviserService.GetAdviserListItems());
            ViewData["advisers"] = new SelectList(advisers, "Value", "Text", notCalled.AdviserId);

            ViewBag.IsAdd = true;
            ViewData["AddedNumber"] = notCalled.Number;

            return View();
        }


        #endregion

        #region ManageRoles

        #region ShowRole

        public IActionResult ManageRoles()
        {
            List<Role> roles = _permissionService.GetRoles().Where(r => r.IsDelete == false).ToList();

            return View(roles);
        }

        [Route("/AdminPanel/RoleDetail/{id}")]
        public IActionResult RoleDetail(int id)
        {
            Role role = _permissionService.GetRoleById(id);

            return View(role);
        }

        #endregion

        #region AddRole

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(Role role, List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
                return View();

            // Add Role
            int roleId = _permissionService.AddRole(role);

            // Add Permission To Role
            _permissionService.AddPermissionsToRole(roleId, selectedPermissions);

            return Redirect($"/AdminPanel/RoleDetail/{roleId}");
        }

        #endregion

        #region EditRole

        [Route("/AdminPanel/EditRole/{id}")]
        public IActionResult EditRole(int id)
        {
            EditRoleViewModel editRole = _permissionService.GetDataForEditRole(id);

            return View(editRole);
        }

        [HttpPost]
        [Route("/AdminPanel/EditRole/{id}")]
        public IActionResult EditRole(EditRoleViewModel editRole, List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
                return View(editRole);

            _permissionService.EditRole(editRole);

            _permissionService.UpdatePermissionsRole(editRole.RoleId, selectedPermissions);

            return Redirect($"/AdminPanel/RoleDetail/{editRole.RoleId}");
        }

        #endregion

        #region DeleteRole

        [Route("/AdminPanel/DeleteRole/{id}")]
        public IActionResult DeleteRole(int id)
        {
            Role role = _permissionService.GetRoleById(id);

            _permissionService.DeleteRole(role);

            return Redirect("/AdminPanel/ManageRoles");
        }

        #endregion

        #endregion
    }
}
