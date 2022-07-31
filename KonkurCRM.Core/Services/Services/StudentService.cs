using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.DTOs.Student;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Register;
using KonkurCRM.DataLayer.Entities.Students;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class StudentService: IStudentService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;
        private IAdviserService _adviserService;
        private IRegisterService _registerService;

        public StudentService(KonkurCRMContext context, IConfiguration configuration, IAdviserService adviserService , IRegisterService registerService)
        {
            _context = context;
            _configuration = configuration;
            _adviserService = adviserService;
            _registerService = registerService;
        }

        public int AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student.StudentId;
        }

        public void EditStudent(EditStudentViewModel editStudent)
        {
            Student student = GetStudentById(editStudent.StudentId);

            student.StudentId = editStudent.StudentId;
            student.StudyId = editStudent.StudyId;
            student.GradeId = editStudent.GradeId;
            student.Name = editStudent.Name;
            student.Family = editStudent.Family;
            student.FatherName = editStudent.FatherName;
            student.Mobile1 = editStudent.Mobile1;
            student.Mobile2 = editStudent.Mobile2;
            student.Mobile3 = editStudent.Mobile3;
            student.PhoneNumber = editStudent.PhoneNumber;
            student.City = editStudent.City;


            UpdateStudent(student);
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(int studentId)
        {
            Student student = GetStudentById(studentId);
            student.IsDelete = true;
            UpdateStudent(student);
        }

        public void CancelStudent(int studentId)
        {
            Register register = _registerService.GetRegisterByStudentId(studentId);

            register.IsCancel = true;
            _context.SaveChanges();
        }

        public StudentViewModel GetStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            IEnumerable<InformationStudentViewModel> students = ShowStudents();

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            if (studyId != 0)
            {
                students = students.Where(a => a.StudyId == studyId);
            }

            if (gradeId != 0)
            {
                students = students.Where(a => a.GradeId == gradeId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public StudentViewModel GetUnregisteredStudents(int pageId = 1, string family = "", string mobile = "",
            string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            IEnumerable<InformationStudentViewModel> students = ShowUnregisteredStudents();

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            if (studyId != 0)
            {
                students = students.Where(a => a.StudyId == studyId);
            }

            if (gradeId != 0)
            {
                students = students.Where(a => a.GradeId == gradeId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public StudentViewModel GetDeletedStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            IEnumerable<InformationStudentViewModel> students = ShowDeletedStudents();

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public StudentViewModel GetCanceledStudents(int pageId = 1, string family = "", string mobile = "",
            string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            IEnumerable<InformationStudentViewModel> students = ShowCanceledStudents();

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public StudentViewModel GetAdviserPanelStudents(int pageId = 1,int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            List<InformationAdvisersRegisterViewModel> informationAdvisersRegister = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r=>r.IsDelete == false && r.IsCancel == false).ToList();
            List<InformationStudentViewModel> infoStudent = new List<InformationStudentViewModel>();

            foreach (var items in informationAdvisersRegister)
            {
                Student student = GetStudentById(items.StudentId);

                string grade = GetGradeTitleById(student.GradeId);
                string study = GetStudyTitleById(student.StudyId);

                InformationStudentViewModel info = new InformationStudentViewModel()
                {
                    StudentId = student.StudentId,
                    GradeId = student.GradeId,
                    StudyId = student.StudyId,
                    Grade = grade,
                    Study = study,
                    Name = student.Name,
                    Family = student.Family,
                    FatherName = student.FatherName,
                    Mobile1 = student.Mobile1,
                    Mobile2 = student.Mobile2,
                    Mobile3 = student.Mobile3,
                    PhoneNumber = student.PhoneNumber,
                    City = student.City,
                    RegisterId = items.RegisterId,
                    IsDelete = student.IsDelete
                };

                infoStudent.Add(info);
            }

            IEnumerable<InformationStudentViewModel> students = infoStudent;

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            if (studyId != 0)
            {
                students = students.Where(a => a.StudyId == studyId);
            }

            if (gradeId != 0)
            {
                students = students.Where(a => a.GradeId == gradeId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }
        
        public StudentViewModel GetAdviserPanelUnregisteredStudents(int pageId = 1,int adviserId = 0 , string family = "", string mobile = "",
            string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            IEnumerable<InformationStudentViewModel> students = ShowUnRegisteredStudentByAdviser().Where(r=>r.AdviserId==adviserId);

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            if (studyId != 0)
            {
                students = students.Where(a => a.StudyId == studyId);
            }

            if (gradeId != 0)
            {
                students = students.Where(a => a.GradeId == gradeId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public StudentViewModel GetAdviserPanelDeletedStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            List<InformationAdvisersRegisterViewModel> informationAdvisersRegister = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r => r.IsDelete).ToList();
            List<InformationStudentViewModel> infoStudent = new List<InformationStudentViewModel>();

            foreach (var items in informationAdvisersRegister)
            {
                Student student = GetStudentById(items.StudentId);

                string grade = GetGradeTitleById(student.GradeId);
                string study = GetStudyTitleById(student.StudyId);

                InformationStudentViewModel info = new InformationStudentViewModel()
                {
                    StudentId = student.StudentId,
                    GradeId = student.GradeId,
                    StudyId = student.StudyId,
                    Grade = grade,
                    Study = study,
                    Name = student.Name,
                    Family = student.Family,
                    FatherName = student.FatherName,
                    Mobile1 = student.Mobile1,
                    Mobile2 = student.Mobile2,
                    Mobile3 = student.Mobile3,
                    PhoneNumber = student.PhoneNumber,
                    City = student.City,
                    RegisterId = items.RegisterId,
                    IsDelete = student.IsDelete
                };

                infoStudent.Add(info);
            }
            IEnumerable<InformationStudentViewModel> students = infoStudent;

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public StudentViewModel GetAdviserPanelCanceledStudents(int pageId = 1,int adviserId = 0, string family = "", string mobile = "",
            string phone = "", string city = "", int studyId = 0, int gradeId = 0)
        {
            List<InformationAdvisersRegisterViewModel> informationAdvisersRegister = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r=>r.IsCancel).ToList();
            List<InformationStudentViewModel> infoStudent = new List<InformationStudentViewModel>();

            foreach (var items in informationAdvisersRegister)
            {
                Student student = GetStudentById(items.StudentId);

                string grade = GetGradeTitleById(student.GradeId);
                string study = GetStudyTitleById(student.StudyId);

                InformationStudentViewModel info = new InformationStudentViewModel()
                {
                    StudentId = student.StudentId,
                    GradeId = student.GradeId,
                    StudyId = student.StudyId,
                    Grade = grade,
                    Study = study,
                    Name = student.Name,
                    Family = student.Family,
                    FatherName = student.FatherName,
                    Mobile1 = student.Mobile1,
                    Mobile2 = student.Mobile2,
                    Mobile3 = student.Mobile3,
                    PhoneNumber = student.PhoneNumber,
                    City = student.City,
                    RegisterId = items.RegisterId,
                    IsDelete = student.IsDelete
                };

                infoStudent.Add(info);
            }

            IEnumerable<InformationStudentViewModel> students = infoStudent;

            if (!string.IsNullOrEmpty(family))
            {
                students = students.Where(a => a.Family.Contains(family));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                students = students.Where(a => a.Mobile1.Contains(mobile) || a.Mobile2.Contains(mobile) || a.Mobile3.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                students = students.Where(a => a.PhoneNumber.Contains(phone));
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(a => a.City.Contains(city));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            StudentViewModel list = new StudentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)students.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationStudents = students.OrderBy(s => s.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public EditStudentViewModel GetDataForEditStudent(int studentId)
        {
            return _context.Students.Where(s => s.StudentId == studentId).Select(s => new EditStudentViewModel()
            {
                StudentId = studentId,
                Name = s.Name,
                Family = s.Family,
                FatherName = s.FatherName,
                Mobile1 = s.Mobile1,
                Mobile2 = s.Mobile2,
                Mobile3 = s.Mobile3,
                PhoneNumber = s.PhoneNumber,
                City = s.City,
                GradeId = s.GradeId,
                StudyId = s.StudyId
            }).Single();
        }

        public Student GetStudentById(int studentId)
        {
            return _context.Students.Find(studentId);
        }

        public string GetGradeTitleById(int gradeId)
        {
            Grade grade = _context.Grades.Find(gradeId);

            return grade.GradeTitle;
        }

        public string GetStudyTitleById(int studyId)
        {
            Study study = _context.Studies.Find(studyId);

            return study.StudyTitle;
        }

        public int ShowAmountDebtorStudent(int studentId)
        {
            int amount = 0;

            List<Register> registers = GetStudentsRegisters(studentId);

            foreach (var register in registers)
            {
                List<Pay> pays = GetDebtorStudents(register.RegisterId);

                foreach (var pay in pays)
                {
                    amount += pay.Amount;
                }
            }

            return amount;
        }

        public List<InformationRegistersAdviserViewModel> ShowStudentsAdviserForLastRegister(int studentId)
        {
            int registerId = _context.Registers.OrderBy(r=>r.RegisterId).Last(r => r.StudentId == studentId).RegisterId;

            List<InformationRegistersAdviserViewModel> registers = GetStudentsAdviser(registerId);

            return registers;
        }

        public IList<InformationRegistersAdviserViewModel> ShowStudentsAdviserForAllRegister(int studentId)
        {
            List<Register> registers = GetStudentsRegisters(studentId);

            List<InformationRegistersAdviserViewModel> studentsAdviser =
                new List<InformationRegistersAdviserViewModel>();

            foreach (var register in registers)
            {
                var advisers = GetStudentsAdviser(register.RegisterId);

                studentsAdviser.AddRange(advisers);
            }

            return studentsAdviser;
        }

        public List<SelectListItem> GetGradeListItems()
        {
            return _context.Grades.Select(g => new SelectListItem()
            {
                Text = g.GradeTitle,
                Value = g.GradeId.ToString()

            }).ToList();
        }

        public List<SelectListItem> GetStudyListItems()
        {
            return _context.Studies.Select(s => new SelectListItem()
            {
                Text = s.StudyTitle,
                Value = s.StudyId.ToString()

            }).ToList();
        }

        public List<SelectListItem> GetStudentsForUnRegisterFollowUpListItems(int adviserId)
        {
            List<InformationStudentViewModel> students = ShowUnregisteredStudents();

            return students.Select(s => new SelectListItem()
            {
                Text = s.Name + ' ' + s.Family,
                Value = s.StudentId.ToString()

            }).ToList();
        }

        #region Dapper


        public List<InformationStudentViewModel> ShowStudents()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationStudentViewModel>("ShowStudents", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationStudentViewModel> ShowUnregisteredStudents()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationStudentViewModel>("ShowUnregisteredStudents", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationStudentViewModel> ShowUnRegisteredStudentByAdviser()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationStudentViewModel>("ShowUnRegisteredStudentByAdviser", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationStudentViewModel> ShowDeletedStudents()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationStudentViewModel>("ShowDeletedStudents", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationStudentViewModel> ShowCanceledStudents()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationStudentViewModel>("ShowCanceledStudents", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }
        
        public List<Register> GetStudentsRegisters(int studentId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<Register>("GetStudentsRegisters", new { studentId = studentId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<Pay> GetDebtorStudents(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<Pay>("GetDebtorStudents",new { registerId  = registerId } ,commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationRegistersAdviserViewModel> GetStudentsAdviser(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegistersAdviserViewModel>("GetStudentsAdviser", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationUnregisteredCalls> GetUnregisteredCallsByStudentId(int studentId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationUnregisteredCalls>("ShowUnregisteredCallsByStudentId", new { studentId = studentId }, commandType: CommandType.StoredProcedure).OrderByDescending(a=>a.CallDate).ToList();

                return Show;
            }
        }


        #endregion
    }
}
