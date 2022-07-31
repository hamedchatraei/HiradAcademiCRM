using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.Convertor;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.Generator;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Register;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;
        private ISettingsService _settingsService;
        private IAdviserService _adviserService;

        public RegisterService(KonkurCRMContext context, IConfiguration configuration, ISettingsService settingsService, IAdviserService adviserService)
        {
            _context = context;
            _configuration = configuration;
            _settingsService = settingsService;
            _adviserService = adviserService;
        }


        #region Register

        public int AddRegister(Register register)
        {
            _context.Registers.Add(register);
            _context.SaveChanges();
            return register.RegisterId;
        }

        public void EditRegister(EditRegisterViewModel editRegister)
        {
            Register register = GetRegisterById(editRegister.RegisterId);

            register.RegisterId = editRegister.RegisterId;
            register.AdviserId = editRegister.AdviserId;
            register.StudentId = editRegister.StudentId;
            register.PayTypeId = editRegister.PayTypeId;
            register.CourseId = editRegister.CourseId;
            register.PlanId = editRegister.PlanId;
            register.RegisterDate = editRegister.RegisterDate;
            register.Discount = editRegister.Discount;
            register.FinalAmount = editRegister.FinalAmount;


            UpdateRegister(register);
        }

        public void UpdateRegister(Register register)
        {
            _context.Registers.Update(register);
            _context.SaveChanges();
        }

        public void DeleteRegister(int registerId)
        {
            Register register = GetRegisterById(registerId);
            register.IsDelete = true;
            UpdateRegister(register);
        }

        public EditRegisterViewModel GetDataForEditRegister(int registerId)
        {
            return _context.Registers.Where(r => r.RegisterId == registerId).Select(r => new EditRegisterViewModel()
            {
                RegisterId = registerId,
                StudentId = r.StudentId,
                AdviserId = r.AdviserId,
                CourseId = r.CourseId,
                PlanId = r.PlanId,
                PayTypeId = r.PayTypeId,
                RegisterDate = r.RegisterDate,
                Discount = r.Discount,
                FinalAmount = r.FinalAmount
            }).Single();
        }

        public Register GetRegisterById(int registerId)
        {
            return _context.Registers.Find(registerId);
        }

        public Register GetRegisterByStudentId(int studentId)
        {
            Register register = _context.Registers.OrderBy(r => r.RegisterId).Last(r => r.StudentId == studentId);

            return register;
        }

        public RegisterViewModel GetRegisters(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0, int courseId = 0)
        {
            IEnumerable<InformationRegisterViewModel> registers = ShowRegisters();

            if (adviserId != 0)
            {
                registers = registers.Where(a => a.AdviserId == adviserId);
            }

            if (studentId != 0)
            {
                registers = registers.Where(a => a.StudentId == studentId);
            }

            if (planId != 0)
            {
                registers = registers.Where(a => a.PlanId == planId);
            }

            if (courseId != 0)
            {
                registers = registers.Where(a => a.CourseId == courseId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            RegisterViewModel list = new RegisterViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)registers.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationRegister = registers.OrderByDescending(a => a.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public RegisterViewModel GetRegistersByAdviserId(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0,
            int courseId = 0)
        {
            IEnumerable<InformationRegisterViewModel> registers = ShowRegisterByAdviserId(adviserId);

            if (adviserId != 0)
            {
                registers = registers.Where(a => a.AdviserId == adviserId);
            }

            if (studentId != 0)
            {
                registers = registers.Where(a => a.StudentId == studentId);
            }

            if (planId != 0)
            {
                registers = registers.Where(a => a.PlanId == planId);
            }

            if (courseId != 0)
            {
                registers = registers.Where(a => a.CourseId == courseId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            RegisterViewModel list = new RegisterViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)registers.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationRegister = registers.OrderByDescending(a => a.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public RegisterViewModel GetRegistersByPlanId(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0,
            int courseId = 0)
        {
            IEnumerable<InformationAdvisersRegisterViewModel> registers = GetRegisterByPlanId(planId);

            if (adviserId != 0)
            {
                registers = registers.Where(a => a.AdviserId == adviserId);
            }

            if (studentId != 0)
            {
                registers = registers.Where(a => a.StudentId == studentId);
            }

            if (planId != 0)
            {
                registers = registers.Where(a => a.PlanId == planId);
            }

            if (courseId != 0)
            {
                registers = registers.Where(a => a.CourseId == courseId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            RegisterViewModel list = new RegisterViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)registers.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationAdvisersRegister = registers.OrderByDescending(a => a.StartDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public RegisterViewModel GetRegistersByCourseId(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0,
            int courseId = 0)
        {
            IEnumerable<InformationAdvisersRegisterViewModel> registers = GetRegisterByCourseId(courseId);

            if (adviserId != 0)
            {
                registers = registers.Where(a => a.AdviserId == adviserId);
            }

            if (studentId != 0)
            {
                registers = registers.Where(a => a.StudentId == studentId);
            }

            if (planId != 0)
            {
                registers = registers.Where(a => a.PlanId == planId);
            }

            if (courseId != 0)
            {
                registers = registers.Where(a => a.CourseId == courseId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            RegisterViewModel list = new RegisterViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)registers.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationAdvisersRegister = registers.OrderByDescending(a => a.StartDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public RegisterViewModel GetDeletedRegisters(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0,
            int courseId = 0)
        {
            IEnumerable<InformationRegisterViewModel> registers = ShowDeletedRegisters();

            if (adviserId != 0)
            {
                registers = registers.Where(a => a.AdviserId == adviserId);
            }

            if (studentId != 0)
            {
                registers = registers.Where(a => a.StudentId == studentId);
            }

            if (planId != 0)
            {
                registers = registers.Where(a => a.PlanId == planId);
            }

            if (courseId != 0)
            {
                registers = registers.Where(a => a.CourseId == courseId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            RegisterViewModel list = new RegisterViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)registers.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationRegister = registers.OrderByDescending(a => a.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public RegisterViewModel GetAdviserStudents(int pageId = 1, int adviserId = 0, string family = "", int planId = 0, int courseId = 0)
        {
            IEnumerable<InformationRegisterViewModel> register = ShowRegisterByAdviserId(adviserId);
            IEnumerable<InformationAdvisersRegisterViewModel> infoRegister = _adviserService.GetRegistersByAdviserId(adviserId);
            List<int> listGroupId = _adviserService.GetAdvisersGroup(adviserId);

            if (!string.IsNullOrEmpty(family))
            {
                register = register.Where(a => a.StudentNameFamily.Contains(family));
                infoRegister = infoRegister.Where(a => a.StudentNameFamily.Contains(family));
            }

            if (planId != 0)
            {
                register = register.Where(a => a.PlanId == planId);
                infoRegister = infoRegister.Where(a => a.PlanId == planId);
            }

            if (courseId != 0)
            {
                register = register.Where(a => a.CourseId == courseId);
                infoRegister = infoRegister.Where(a => a.CourseId == courseId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            RegisterViewModel list = new RegisterViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)register.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.InformationRegister = register.OrderByDescending(a => a.RegisterDate).Skip(skip).Take(take).ToList();
            list.InformationAdvisersRegister = infoRegister.OrderByDescending(a => a.StartDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public int GetSumAmountPayAdviserForLastMonth(int adviserId)
        {
            var registers = GetRegisterByAdviserId(adviserId).Where(a => a.AdviserTypeId == 2);

            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetPayByRegisterId(item.RegisterId, adviserId);
                List<InformationChequeByRegisterViewModel> resultCheque = GetChequeByRegisterId(item.RegisterId, adviserId);


                resultPay = resultPay.Where(r => r.PayDate.Date >= startDate.Date && r.PayDate.Date <= endDate.Date).ToList();
                resultCheque = resultCheque.Where(r => r.ChequeDate.Date >= startDate.Date && r.ChequeDate.Date <= endDate.Date && r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetTotalSumAmountPayAdviser(int adviserId)
        {
            var registers = GetRegisterByAdviserId(adviserId).Where(a => a.AdviserTypeId == 2);

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> result = GetPayByRegisterId(item.RegisterId, adviserId);
                List<InformationChequeByRegisterViewModel> resultCheque = GetChequeByRegisterId(item.RegisterId, adviserId);

                resultCheque = resultCheque.Where(r => r.IsPass).ToList();

                foreach (var pay in result)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }
            }


            return amount;
        }

        public int GetSumAmountPayAdviserByCourseIdForLastMonth(int adviserId, int courseId)
        {
            List<InformationAdvisersRegisterViewModel> registers = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r => r.CourseId == courseId).ToList();

            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetPayByRegisterId(item.RegisterId, adviserId);
                List<InformationChequeByRegisterViewModel> resultCheque = GetChequeByRegisterId(item.RegisterId, adviserId);


                resultPay = resultPay.Where(r => r.PayDate.Date >= startDate.Date && r.PayDate.Date <= endDate.Date).ToList();
                resultCheque = resultCheque.Where(r => r.ChequeDate.Date >= startDate.Date && r.ChequeDate.Date <= endDate.Date && r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetTotalSumAmountPayAdviserByCourseId(int adviserId, int courseId)
        {
            List<InformationAdvisersRegisterViewModel> registers = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r => r.CourseId == courseId).ToList();

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> result = GetPayByRegisterId(item.RegisterId, adviserId);
                List<InformationChequeByRegisterViewModel> resultCheque = GetChequeByRegisterId(item.RegisterId, adviserId);

                resultCheque = resultCheque.Where(r => r.IsPass).ToList();

                foreach (var pay in result)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }
            }


            return amount;
        }

        public int GetSumAmountPayAdviserByPlanIdForLastMonth(int adviserId, int planId)
        {
            List<InformationAdvisersRegisterViewModel> registers = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r => r.PlanId == planId && r.AdviserTypeId == 2).ToList();

            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetPayByRegisterId(item.RegisterId, adviserId);
                List<InformationChequeByRegisterViewModel> resultCheque = GetChequeByRegisterId(item.RegisterId, adviserId);


                resultPay = resultPay.Where(r => r.PayDate.Date >= startDate.Date && r.PayDate.Date <= endDate.Date).ToList();
                resultCheque = resultCheque.Where(r => r.ChequeDate.Date >= startDate.Date && r.ChequeDate.Date <= endDate.Date && r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetTotalSumAmountPayAdviserByPlanId(int adviserId, int planId)
        {
            List<InformationAdvisersRegisterViewModel> registers = _adviserService.GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r => r.PlanId == planId && r.AdviserTypeId == 2).ToList();

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> result = GetPayByRegisterId(item.RegisterId, adviserId);
                List<InformationChequeByRegisterViewModel> resultCheque = GetChequeByRegisterId(item.RegisterId, adviserId);

                resultCheque = resultCheque.Where(r => r.IsPass).ToList();

                foreach (var pay in result)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetSumAmountPayCourseForLastMonth(int courseId)
        {
            List<InformationAdvisersRegisterViewModel> registers = GetRegisterByCourseId(courseId);

            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);

            int amount = 0;

            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetAllPayByRegisterId(item.RegisterId).Where(r => r.CourseId == courseId).ToList();
                List<InformationChequeByRegisterViewModel> resultCheque = GetAllChequeByRegisterId(item.RegisterId).Where(r => r.CourseId == courseId).ToList();


                resultPay = resultPay.Where(r => r.PayDate.Date >= startDate.Date && r.PayDate.Date <= endDate.Date).ToList();
                resultCheque = resultCheque.Where(r => r.ChequeDate.Date >= startDate.Date && r.ChequeDate.Date <= endDate.Date && r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetTotalSumAmountPayCourse(int courseId)
        {
            List<InformationAdvisersRegisterViewModel> registers = GetRegisterByCourseId(courseId);

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetAllPayByRegisterId(item.RegisterId).Where(r => r.CourseId == courseId).ToList();
                List<InformationChequeByRegisterViewModel> resultCheque = GetAllChequeByRegisterId(item.RegisterId).Where(r => r.CourseId == courseId).ToList();

                resultCheque = resultCheque.Where(r => r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetSumAmountPayPlanForLastMonth(int planId)
        {
            List<InformationAdvisersRegisterViewModel> registers = GetRegisterByPlanId(planId);

            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);

            int amount = 0;

            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetAllPayByRegisterId(item.RegisterId).Where(r => r.PlanId == planId).ToList();
                List<InformationChequeByRegisterViewModel> resultCheque = GetAllChequeByRegisterId(item.RegisterId).Where(r => r.PlanId == planId).ToList();


                resultPay = resultPay.Where(r => r.PayDate.Date >= startDate.Date && r.PayDate.Date <= endDate.Date).ToList();
                resultCheque = resultCheque.Where(r => r.ChequeDate.Date >= startDate.Date && r.ChequeDate.Date <= endDate.Date && r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public int GetTotalSumAmountPayPlan(int planId)
        {
            List<InformationAdvisersRegisterViewModel> registers = GetRegisterByPlanId(planId);

            int amount = 0;


            foreach (var item in registers)
            {
                List<InformationPayByRegisterViewModel> resultPay = GetAllPayByRegisterId(item.RegisterId).Where(r => r.PlanId == planId).ToList();
                List<InformationChequeByRegisterViewModel> resultCheque = GetAllChequeByRegisterId(item.RegisterId).Where(r => r.PlanId == planId).ToList();

                resultCheque = resultCheque.Where(r => r.IsPass).ToList();

                foreach (var pay in resultPay)
                {
                    amount += pay.Amount;
                }

                foreach (var cheque in resultCheque)
                {
                    amount += cheque.Amount;
                }

            }


            return amount;
        }

        public List<Register_sAdviser> GetRegisterAdviserForLastMonth(int adviserId)
        {
            var registers = GetRegisterByAdviserId(adviserId);

            return registers;
        }

        #region Dapper

        public List<InformationRegisterViewModel> ShowRegisters()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegisterViewModel>("ShowRegisters", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationRegisterViewModel> ShowDeletedRegisters()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegisterViewModel>("ShowRegisters", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationRegisterViewModel> ShowRegisterByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegisterViewModel>("ShowRegisterByAdviserId", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).OrderByDescending(r => r.RegisterDate).ToList();

                return Show;
            }
        }

        public List<InformationRegisterViewModel> ShowRegisterByStudentId(int studentId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegisterViewModel>("ShowRegisterByStudentId", new { studentId = studentId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationRegisterViewModel> GetAllRegisters()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegisterViewModel>("GetAllRegisters", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationAdvisersRegisterViewModel> GetRegisterByPlanId(int planId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdvisersRegisterViewModel>("GetRegisterByPlanId", new { planId = planId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationAdvisersRegisterViewModel> GetRegisterByCourseId(int courseId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdvisersRegisterViewModel>("GetRegisterByCourseId", new { courseId = courseId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<Register_sAdviser> GetRegisterByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<Register_sAdviser>("GetRegisterByAdviserId", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationPayByRegisterViewModel> GetPayByRegisterId(int registerId, int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationPayByRegisterViewModel>("GetPayByRegisterId", new { registerId = registerId, adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationChequeByRegisterViewModel> GetChequeByRegisterId(int registerId, int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationChequeByRegisterViewModel>("GetChequeByRegisterId", new { registerId = registerId, adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationPayByRegisterViewModel> GetAllPayByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationPayByRegisterViewModel>("GetAllPayByRegisterId", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationChequeByRegisterViewModel> GetAllChequeByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationChequeByRegisterViewModel>("GetAllChequeByRegisterId", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<Installment> GetInstallmentByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<Installment>("GetInstallmentByRegisterId", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public Register GetLastRegisterByStudentId(int studentId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<Register>("GetLastRegisterByStudentId", new { studentId = studentId }, commandType: CommandType.StoredProcedure).Single();

                return Show;
            }
        }

        public List<InformationRegistersAdviserViewModel> ShowInformationRegisterByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegistersAdviserViewModel>("ShowInformationRegisterByRegisterId", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<SelectListItem> GetAllRegistersListItems()
        {
            List<InformationRegisterViewModel> info = GetAllRegisters();

            return info.Select(a => new SelectListItem()
            {
                Text = "ثبت نام" + ' ' + a.StudentNameFamily + ' ' + "در تاریخ" + ' ' + a.RegisterDate.ToShamsi() + ' ' + "توسط" + ' ' + a.AdviserNameFamily,
                Value = a.RegisterId.ToString()

            }).ToList();
        }

        #endregion

        #endregion

        #region Register'sAdviser

        public int AddRegistersAdviser(Register_sAdviser registerSAdviser)
        {
            _context.RegisterSAdvisers.Add(registerSAdviser);
            _context.SaveChanges();
            return registerSAdviser.RAId;
        }

        public void EditRegistersAdviser(Register_sAdviser registerSAdviser)
        {
            Register_sAdviser editRegisterSAdviser = GetRegistersAdviserById(registerSAdviser.RAId);

            editRegisterSAdviser.RAId = registerSAdviser.RAId;
            editRegisterSAdviser.RegisterId = registerSAdviser.RegisterId;
            editRegisterSAdviser.AdviserId = registerSAdviser.AdviserId;
            editRegisterSAdviser.AdviserTypeId = registerSAdviser.AdviserTypeId;
            editRegisterSAdviser.StartDate = registerSAdviser.StartDate;
            editRegisterSAdviser.EndDate = registerSAdviser.EndDate;
            editRegisterSAdviser.IsActive = registerSAdviser.IsActive;

            UpdateRegistersAdviser(editRegisterSAdviser);
        }

        public void DeActiveRegistersAdviser(int registerId, int typeId, Register_sAdviser registerSAdviser)
        {
            List<Register_sAdviser> list = _context.RegisterSAdvisers
                .Where(r => r.RegisterId == registerId && r.AdviserTypeId == typeId).ToList();
            

            if (list.Any(l => l.RegisterId == registerId && l.AdviserTypeId == typeId))
            {
                foreach (var item in list)
                {
                    item.IsActive = false;
                    UpdateRegistersAdviser(item);
                }
            }

            Register_sAdviser last = list.Last();
            last.EndDate = registerSAdviser.StartDate;

            EditRegistersAdviser(last);

        }

        public void DeleteRegistersAdviser(int raId)
        {
            Register_sAdviser registerSAdviser = GetRegistersAdviserById(raId);

            _context.RegisterSAdvisers.Remove(registerSAdviser);
            _context.SaveChanges();
        }

        public void UpdateRegistersAdviser(Register_sAdviser registerSAdviser)
        {
            _context.RegisterSAdvisers.Update(registerSAdviser);
            _context.SaveChanges();
        }

        public Register_sAdviser GetRegistersAdviserById(int raId)
        {
            return _context.RegisterSAdvisers.Find(raId);
        }

        public Register_sAdviser GetRegistersAdviserByRegisterId(int registerId)
        {
            return _context.RegisterSAdvisers.OrderBy(r => r.RAId).FirstOrDefault(r => r.RegisterId == registerId);
        }

        public Register_sAdviser GetDataForEditRegisterSAdviser(int raId)
        {
            return _context.RegisterSAdvisers.Where(r => r.RAId == raId).Select(r => new Register_sAdviser()
            {
                RAId = raId,
                RegisterId = r.RegisterId,
                AdviserId = r.AdviserId,
                AdviserTypeId = r.AdviserTypeId,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                IsActive = r.IsActive
            }).Single();
        }

        public void AddAdviserToRegister(List<int> adviserIds, int registerId)
        {
            foreach (int adviserId in adviserIds)
            {
                _context.RegisterSAdvisers.Add(new Register_sAdviser()
                {
                    AdviserId = adviserId,
                    RegisterId = registerId
                });
            }

            _context.SaveChanges();
        }

        public void UpdateRegistersAdviser(int registerId, List<int> adviserIds)
        {
            //Delete All Register's Adviser
            _context.RegisterSAdvisers.Where(r => r.RegisterId == registerId).ToList().ForEach(r => _context.RegisterSAdvisers.Remove(r));

            //Add Adviser To Register
            AddAdviserToRegister(adviserIds, registerId);
        }

        public List<int> GetRegistersAdviser(int registerId)
        {
            return _context.RegisterSAdvisers
                .Where(r => r.RegisterId == registerId)
                .Select(r => r.AdviserId).ToList();
        }

        #endregion
    }
}
