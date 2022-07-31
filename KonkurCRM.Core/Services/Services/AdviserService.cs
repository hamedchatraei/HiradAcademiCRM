using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.Convertor;
using KonkurCRM.Core.DTOs.Adviser;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.Generator;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Pays;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class AdviserService : IAdviserService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;
        private ISettingsService _settingsService;

        public AdviserService(KonkurCRMContext context, IConfiguration configuration, ISettingsService settingsService)
        {
            _context = context;
            _configuration = configuration;
            _settingsService = settingsService;
        }

        #region Advisers

        public int AddAdviser(Adviser adviser)
        {
            _context.Advisers.Add(adviser);
            _context.SaveChanges();
            return adviser.AdviserId;
        }

        public void EditAdviser(EditAdviserViewModel editAdviser)
        {
            Adviser adviser = GetAdviserById(editAdviser.AdviserId);

            adviser.AdviserId = editAdviser.AdviserId;
            adviser.ParentId = editAdviser.ParentId;
            adviser.Name = editAdviser.Name;
            adviser.Family = editAdviser.Family;
            adviser.JobMobile = editAdviser.JobMobile;
            adviser.PersonalMobile = editAdviser.PersonalMobile;
            adviser.Study = editAdviser.Study;
            adviser.AccountNumber = editAdviser.AccountNumber;
            adviser.UserId = editAdviser.UserId;

            UpdateAdviser(adviser);
        }

        public void DeleteAdviser(int adviserId)
        {
            Adviser adviser = GetAdviserById(adviserId);
            adviser.IsDelete = true;
            UpdateAdviser(adviser);
        }

        public Adviser GetAdviserById(int adviserId)
        {
            return _context.Advisers.Find(adviserId);
        }

        public Adviser GetAdviserByUserId(int userId)
        {
            return _context.Advisers.SingleOrDefault(a => a.UserId == userId);
        }

        public List<Adviser> getAdvisersByParentId(int parentId)
        {
            return _context.Advisers.Where(a => a.ParentId == parentId).ToList();
        }

        public List<Adviser> GetAllAdvisers()
        {
            return _context.Advisers.ToList();
        }

        public int CommissionAdviserForMonth(int adviserId)
        {
            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);
            DateTime startDateLastMonth = generate.GetStartDayOfLastMonth(startDay);
            DateTime endDateLastMonth = generate.GetEndDateOfMonth(startDateLastMonth);

            int amount = 0;

            List<InformationPayByRegisterViewModel> adviserPay = GetPayByAdviserId(adviserId);
            List<InformationChequeByRegisterViewModel> adviserCheque = GetChequeByAdviserId(adviserId);

            adviserPay = adviserPay.Where(a => a.PayDate.Date >= startDateLastMonth.Date && a.PayDate.Date <= endDateLastMonth.Date).ToList();
            adviserCheque = adviserCheque.Where(a =>
                    a.IsPass && a.ChequeDate.Date >= startDateLastMonth.Date &&
                    a.ChequeDate.Date <= endDateLastMonth.Date)
                .ToList();

            foreach (var pays in adviserPay)
            {
                amount += (pays.Amount * pays.PercentAmount) / 100;
            }

            foreach (var cheque in adviserCheque)
            {
                amount += (cheque.Amount * cheque.PercentAmount) / 100;
            }


            return amount;
        }

        public List<SelectListItem> GetAdviserListItems()
        {
            return _context.Advisers.Where(u => u.IsDelete == false).Select(a => new SelectListItem()
            {
                Text = a.Name + ' ' + a.Family,
                Value = a.AdviserId.ToString()

            }).ToList();
        }

        public List<SelectListItem> GetAdviserListItemsByType(int id)
        {
            List<AdviserGroup> adviserGroup = _context.AdviserGroups.Where(a => a.GroupId == id).ToList();
            List<Adviser> advisers = new List<Adviser>();

            foreach (var group in adviserGroup)
            {
                Adviser adviser = GetAdviserById(group.AdviserId);

                advisers.Add(adviser);
            }

            return advisers.Where(u => u.IsDelete == false).Select(a => new SelectListItem()
            {
                Text = a.Name + ' ' + a.Family,
                Value = a.AdviserId.ToString()

            }).ToList();
        }
        

        public List<SelectListItem> GetSubAdviserListItems(int adviserId)
        {
            List<int> subAdviserIds = new List<int>();
            List<Adviser> subAdviserList = new List<Adviser>();
            subAdviserIds.Add(adviserId);

            List<Adviser> subAdviser = getAdvisersByParentId(adviserId);

            foreach (var item in subAdviser)
            {
                subAdviserIds.Add(item.AdviserId);

                List<Adviser> sub2Adviser = getAdvisersByParentId(item.AdviserId);

                foreach (var items in sub2Adviser)
                {
                    subAdviserIds.Add(items.AdviserId);
                }
            }

            foreach (var item in subAdviserIds)
            {
                List<Adviser> result = _context.Advisers.Where(i => i.AdviserId == item).ToList();

                foreach (var items in result)
                {
                    subAdviserList.Add(items);
                }
            }

            return subAdviserList.Select(a => new SelectListItem()
            {
                Text = a.Name + ' ' + a.Family,
                Value = a.AdviserId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetVendorAdviserListItems()
        {
            List<InformationAdviserViewModel> info = ShowAdvisers();

            return info.Where(u => u.GroupId == 2).Select(a => new SelectListItem()
            {
                Text = a.Name + ' ' + a.Family,
                Value = a.AdviserId.ToString()

            }).ToList();
        }

        public List<SelectListItem> GetSyllabusAdviserListItems()
        {
            List<InformationAdviserViewModel> info = ShowAdvisers();

            return info.Where(u => u.GroupId == 3).Select(a => new SelectListItem()
            {
                Text = a.Name + ' ' + a.Family,
                Value = a.AdviserId.ToString()

            }).ToList();
        }

        public List<SelectListItem> GetRegisterAdviserForFollowUpListItems(int adviserId)
        {
            List<InformationAdvisersRegisterViewModel> info = GetRegistersByAdviserIdWithPlanAndCourse(adviserId);

            return info.Select(a => new SelectListItem()
            {
                Text = "ثبت نام" + ' ' + a.StudentNameFamily + ' ' + "در تاریخ" + ' ' + a.StartDate.ToShamsi(),
                Value = a.RegisterId.ToString()

            }).ToList();
        }
        
        public List<InformationAdvisersRegisterViewModel> GetRegistersByAdviserIdWithPlanAndCourseForLastMonth(int adviserId)
        {
            GenerateDate generate = new GenerateDate();
            int startDay = _settingsService.GetStartDayOfMonth();
            DateTime startDate = generate.GetStartDayOfMonth(startDay);
            DateTime endDate = generate.GetEndDateOfMonth(startDate);


            var registers = GetRegistersByAdviserIdWithPlanAndCourse(adviserId).Where(r =>
                r.StartDate.Date >= startDate.Date && r.StartDate.Date <= endDate.Date);
            List<InformationAdvisersRegisterViewModel> result = new List<InformationAdvisersRegisterViewModel>();


            foreach (var item in registers)
            {
                result.Add(item);
            }

            return result;
        }

        public AdviserViewModel GetAdvisers(int pageId = 1, string family = "", string mobile = "", int groupId = 0)
        {
            IEnumerable<InformationAdviserViewModel> advisers = ShowAdvisers();


            if (!string.IsNullOrEmpty(mobile))
            {
                advisers = advisers.Where(a => a.JobMobile.Contains(mobile) || a.PersonalMobile.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(family))
            {
                advisers = advisers.Where(a => a.Family.Contains(family));
            }

            if (groupId != 0)
            {
                advisers = advisers.Where(a => a.GroupId == groupId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            AdviserViewModel list = new AdviserViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)advisers.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationAdviser = advisers.OrderBy(a => a.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public AdviserViewModel GetAdvisersForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string family = "", string mobile = "")
        {
            List<InformationAdviserViewModel> myAdvisers = new List<InformationAdviserViewModel>();
            List<int> subAdviserIds = new List<int>();

            List<Adviser> subAdviser = getAdvisersByParentId(adviserId);

            foreach (var item in subAdviser)
            {
                subAdviserIds.Add(item.AdviserId);

                List<Adviser> sub2Adviser = getAdvisersByParentId(item.AdviserId);

                foreach (var items in sub2Adviser)
                {
                    subAdviserIds.Add(items.AdviserId);
                }
            }

            foreach (var item in subAdviserIds)
            {
                InformationAdviserViewModel info = new InformationAdviserViewModel();

                Adviser adviser = GetAdviserById(item);

                List<AdviserGroup> adviserGroups = GetGroupAdviserByAdviserId(adviser.AdviserId);

                foreach (var items in adviserGroups)
                {
                    info = new InformationAdviserViewModel()
                    {
                        AdviserId = adviser.AdviserId,
                        GroupId = items.GroupId,
                        Name = adviser.Name,
                        Family = adviser.Family,
                        JobMobile = adviser.JobMobile,
                        PersonalMobile = adviser.PersonalMobile,
                        Study = adviser.Study,
                        AccountNumber = adviser.AccountNumber,
                        IsDelete = adviser.IsDelete,
                        ParentId = Convert.ToInt32(adviser.ParentId)
                    };

                    myAdvisers.Add(info);
                }

            }

            IEnumerable<InformationAdviserViewModel> advisers = myAdvisers.Where(a => a.IsDelete == false);

            if (!string.IsNullOrEmpty(mobile))
            {
                advisers = advisers.Where(a => a.JobMobile.Contains(mobile) || a.PersonalMobile.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(family))
            {
                advisers = advisers.Where(a => a.Family.Contains(family));
            }

            if (filterAdviser != 0 )
            {
                advisers = advisers.Where(a => a.AdviserId == filterAdviser);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            AdviserViewModel list = new AdviserViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)advisers.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationAdviser = advisers.OrderBy(a => a.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public AdviserViewModel GetDeletedAdvisers(int pageId = 1, string family = "", string mobile = "", int groupId = 0)
        {
            IEnumerable<InformationAdviserViewModel> advisers = ShowDeletedAdvisers();

            if (!string.IsNullOrEmpty(mobile))
            {
                advisers = advisers.Where(a => a.JobMobile.Contains(mobile) && a.PersonalMobile.Contains(mobile));
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                advisers = advisers.Where(a => a.Family.Contains(family));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            AdviserViewModel list = new AdviserViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)advisers.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationAdviser = advisers.OrderBy(a => a.Family).Skip(skip).Take(take).ToList();

            return list;
        }

        public EditAdviserViewModel GetDataForEditAdviser(int adviserId)
        {
            return _context.Advisers.Where(a => a.AdviserId == adviserId).Select(a => new EditAdviserViewModel()
            {
                AdviserId = adviserId,
                ParentId = a.ParentId,
                Name = a.Name,
                Family = a.Family,
                JobMobile = a.JobMobile,
                PersonalMobile = a.PersonalMobile,
                Study = a.Study,
                AccountNumber = a.AccountNumber,
                AdviserGroups = a.AdviserGroups.Select(a => a.GroupId).ToList()
            }).Single();
        }

        public void UpdateAdviser(Adviser adviser)
        {
            _context.Advisers.Update(adviser);
            _context.SaveChanges();
        }

        #region Dapper

        public List<InformationAdviserViewModel> ShowAdvisers()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdviserViewModel>("ShowAdvisers", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationAdviserViewModel> ShowAdvisersForAdviserPanel(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdviserViewModel>("ShowAdvisersForAdviserPanel", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationAdviserViewModel> ShowDeletedAdvisers()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdviserViewModel>("ShowDeletedAdvisers", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationAdvisersRegisterViewModel> GetRegistersByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdvisersRegisterViewModel>("GetRegistersByAdviserId", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationRegistersAdviserViewModel> GetInformationRegisterByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationRegistersAdviserViewModel>("GetInformationRegisterByAdviserId", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationAdvisersRegisterViewModel> GetRegistersByAdviserIdWithPlanAndCourse(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationAdvisersRegisterViewModel>("GetRegistersByAdviserIdWithPlanAndCourse", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<AdviserInvoiceViewModel> ShowAdviserInvoices(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<AdviserInvoiceViewModel>("ShowAdviserInvoices", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationPayByRegisterViewModel> GetPayByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationPayByRegisterViewModel>("GetPayByAdviserId", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationChequeByRegisterViewModel> GetChequeByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationChequeByRegisterViewModel>("GetChequeByAdviserId", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        #endregion

        #endregion

        #region GroupAdviser

        public int AddGroupAdviser(GroupAdviser groupAdviser)
        {
            _context.GroupAdvisers.Add(groupAdviser);
            _context.SaveChanges();
            return groupAdviser.GroupId;
        }

        public void AddGroupToAdviser(List<int> groupIds, int adviserId)
        {
            foreach (int groupId in groupIds)
            {
                _context.AdviserGroups.Add(new AdviserGroup()
                {
                    GroupId = groupId,
                    AdviserId = adviserId
                });
            }

            _context.SaveChanges();
        }

        public void DeleteGroupAdviser(int groupAdviserId)
        {
            GroupAdviser groupAdviser = GetGroupAdviserById(groupAdviserId);
            groupAdviser.IsDelete = true;
            UpdateGroupAdviser(groupAdviser);
        }

        public List<int> GetAdvisersGroup(int adviserId)
        {
            return _context.AdviserGroups.OrderBy(a => a.GroupId)
                .Where(a => a.AdviserId == adviserId)
                .Select(a => a.GroupId).ToList();
        }

        public GroupAdviser GetGroupAdviserById(int groupAdviserId)
        {
            return _context.GroupAdvisers.Find(groupAdviserId);
        }

        public List<AdviserGroup> GetGroupAdviserByAdviserId(int adviserId)
        {
            return _context.AdviserGroups.Where(g => g.AdviserId == adviserId).ToList();
        }

        public GroupAdviserViewModel GetGroupAdvisers(int pageId = 1)
        {
            IQueryable<GroupAdviser> result = _context.GroupAdvisers.Where(g => g.IsDelete == false);

            int take = 10;
            int skip = (pageId - 1) * take;

            GroupAdviserViewModel list = new GroupAdviserViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)result.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.GroupAdvisers = result.OrderBy(u => u.GroupId).Skip(skip).Take(take).ToList();

            return list;
        }

        public void UpdateAdvisersGroup(int adviserId, List<int> groupIds)
        {
            //Delete All Adviser's Group
            _context.AdviserGroups.Where(a => a.AdviserId == adviserId).ToList().ForEach(a => _context.AdviserGroups.Remove(a));

            //Add New Group To Adviser
            AddGroupToAdviser(groupIds, adviserId);
        }

        public void UpdateGroupAdviser(GroupAdviser groupAdviser)
        {
            _context.GroupAdvisers.Update(groupAdviser);
            _context.SaveChanges();
        }

        public List<SelectListItem> GetGroupAdviserListItems()
        {
            return _context.GroupAdvisers.Where(u => u.IsDelete == false).Select(a => new SelectListItem()
            {
                Text = a.GroupTitle,
                Value = a.GroupId.ToString()

            }).ToList();
        }

        public List<string> GroupAdviserTilte(int adviserId)
        {
            List<AdviserGroup> adviser = _context.AdviserGroups.Where(a => a.AdviserId == adviserId).OrderBy(a => a.GroupId).ToList();
            List<GroupAdviser> groupAdvisers = new List<GroupAdviser>();
            List<int> groupId = new List<int>();
            List<string> groupTitle = new List<string>();

            foreach (var item in adviser)
            {
                groupId.Add(item.GroupId);
            }


            foreach (var id in groupId)
            {
                groupAdvisers.AddRange(_context.GroupAdvisers.Where(g => g.GroupId == id).ToList());
            }

            foreach (var groupAdviser in groupAdvisers)
            {
                groupTitle.Add(groupAdviser.GroupTitle);
            }


            return groupTitle;
        }

        public string GetGroupTitleByGroupId(int groupId)
        {
            GroupAdviser groupAdviser = _context.GroupAdvisers.Find(groupId);

            return groupAdviser.GroupTitle;
        }

        #endregion

        #region AdviserInvoice

        public void AddInvoice(AdviserInvoice adviserInvoice)
        {
            _context.AdviserInvoices.Add(adviserInvoice);
            _context.SaveChanges();
        }

        public EditAdviserInvoiceViewModel GetDataForEditAdviserInvoice(int adviserInvoiceId)
        {
            return _context.AdviserInvoices.Where(a => a.AdviserInvoiceId == adviserInvoiceId).Select(a =>
                new EditAdviserInvoiceViewModel()
                {
                    AdviserInvoiceId = adviserInvoiceId,
                    AdviserId = a.AdviserId,
                    SalaryTypeId = a.SalaryTypeId,
                    PaySalaryDate = a.PaySalaryDate,
                    Amount = a.Amount,
                    Description = a.Description,
                    TotalAmount = a.TotalAmount
                }).Single();
        }

        public void EditAdviserInvoice(EditAdviserInvoiceViewModel adviserInvoice)
        {
            AdviserInvoice editAdviserInvoice = GetAdviserInvoiceById(adviserInvoice.AdviserInvoiceId);

            editAdviserInvoice.AdviserInvoiceId = adviserInvoice.AdviserInvoiceId;
            editAdviserInvoice.AdviserId = adviserInvoice.AdviserId;
            editAdviserInvoice.SalaryTypeId = adviserInvoice.SalaryTypeId;
            editAdviserInvoice.Amount = adviserInvoice.Amount;
            editAdviserInvoice.Description = adviserInvoice.Description;
            editAdviserInvoice.TotalAmount = adviserInvoice.TotalAmount;
            editAdviserInvoice.PaySalaryDate = adviserInvoice.PaySalaryDate;

            UpdateInvoice(editAdviserInvoice);
        }

        public AdviserInvoice GetAdviserInvoiceById(int adviserInvoiceId)
        {
            return _context.AdviserInvoices.Find(adviserInvoiceId);
        }

        public void UpdateInvoice(AdviserInvoice adviserInvoice)
        {
            _context.AdviserInvoices.Update(adviserInvoice);
            _context.SaveChanges();
        }

        public void DeleteInvoice(int invoiceId)
        {
            AdviserInvoice adviserInvoice = GetAdviserInvoiceById(invoiceId);
            _context.AdviserInvoices.Remove(adviserInvoice);
            _context.SaveChanges();
        }

        public List<AdviserInvoice> getInvoices()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<AdviserInvoice>("getInvoices", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<AdviserInvoiceViewModel> ShowInformationAdviserInvoice()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<AdviserInvoiceViewModel>("ShowInformationAdviserInvoice", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public InvoiceViewModel showAdviserInvoices(int pageId = 1, int adviserId = 0, string fromDate = "", string toDate = "")
        {
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;


            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            IEnumerable<AdviserInvoiceViewModel> adviserInvoices = ShowInformationAdviserInvoice().Where(i => i.SalaryTypeId != 1);


            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                adviserInvoices = adviserInvoices.Where(r => r.PaySalaryDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                adviserInvoices = adviserInvoices.Where(r => r.PaySalaryDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                adviserInvoices = adviserInvoices.Where(r => r.PaySalaryDate.Date >= fDate && r.PaySalaryDate.Date <= tDate);
            }

            if (adviserId != 0)
            {
                adviserInvoices = adviserInvoices.Where(a => a.AdviserId == adviserId);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            InvoiceViewModel list = new InvoiceViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)adviserInvoices.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.AdviserInvoiceViewModels = adviserInvoices.OrderByDescending(a => a.AdviserInvoiceId).Skip(skip).Take(take).ToList();

            return list;
        }

        public List<SelectListItem> GetSalaryTypeListItems()
        {
            return _context.SalaryTypes.Where(s => s.SalaryTypeId != 1).Select(p => new SelectListItem()
            {
                Text = p.Title,
                Value = p.SalaryTypeId.ToString()

            }).ToList();
        }

        #endregion
    }
}
