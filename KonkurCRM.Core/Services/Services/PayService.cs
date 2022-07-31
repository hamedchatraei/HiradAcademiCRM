using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Salary;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class PayService : IPayService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;
        private IAdviserService _adviserService;

        public PayService(KonkurCRMContext context, IConfiguration configuration, IAdviserService adviserService)
        {
            _context = context;
            _configuration = configuration;
            _adviserService = adviserService;
        }

        #region Pay

        public int AddPay(Pay pay)
        {
            _context.Pays.Add(pay);
            _context.SaveChanges();
            return pay.PayId;
        }

        public void EditPay(EditPayViewModel editPay)
        {
            Pay pay = GetPayById(editPay.PayId);

            pay.PayId = editPay.PayId;
            pay.RegisterId = editPay.RegisterId;
            pay.Amount = editPay.Amount;
            pay.PayDate = editPay.PayDate;
            pay.PursuitCode = editPay.PursuitCode;
            pay.Description = editPay.Description;

            UpdatePay(pay);
        }

        public void UpdatePay(Pay pay)
        {
            _context.Pays.Update(pay);
            _context.SaveChanges();
        }

        public void DeletePay(int payId)
        {
            Pay pay = GetPayById(payId);
            _context.Pays.Remove(pay);
            _context.SaveChanges();
        }

        public EditPayViewModel GetDataForEditPay(int payId)
        {
            return _context.Pays.Where(p => p.PayId == payId).Select(p => new EditPayViewModel()
            {
                PayId = payId,
                Amount = p.Amount,
                Description = p.Description,
                PayDate = p.PayDate,
                PursuitCode = p.PursuitCode,
                RegisterId = p.RegisterId
            }).Single();
        }

        public Pay GetPayById(int payId)
        {
            return _context.Pays.Find(payId);
        }

        public Pay GetFirstPayByRegisterId(int registerId)
        {
            return _context.Pays.First(p => p.RegisterId == registerId);
        }

        public PayViewModel GetPays(int pageId = 1, int adviserId = 0, string studentFamily = "", string payDate = "", string fromDate = "", string toDate = "",
            string pursuitCode = "", int fromAmount = 0, int toAmount = 0)
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(payDate))
            {
                date = Convert.ToDateTime(payDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            IEnumerable<InformationPayViewModel> pay = ShowPays();

            if (!string.IsNullOrEmpty(payDate))
            {
                pay = pay.Where(r => r.PayDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                pay = pay.Where(r => r.PayDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                pay = pay.Where(r => r.PayDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                pay = pay.Where(r => r.PayDate.Date >= fDate && r.PayDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount==0)
            {
                pay = pay.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount!=0 && fromAmount==0)
            {
                pay = pay.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount!=0 && toAmount!=0)
            {
                pay = pay.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (adviserId != 0)
            {
                pay = pay.Where(a => a.AdviserId == adviserId);
            }

            if (!string.IsNullOrEmpty(studentFamily))
            {
                pay = pay.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(pursuitCode))
            {
                pay = pay.Where(a => a.PursuitCode.Contains(pursuitCode));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            PayViewModel list = new PayViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)pay.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationPay = pay.OrderByDescending(u => u.PayDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public PayViewModel GetPaysForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "",
            string payDate = "", string fromDate = "", string toDate = "", string pursuitCode = "", int fromAmount = 0,
            int toAmount = 0)
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(payDate))
            {
                date = Convert.ToDateTime(payDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            List<InformationPayViewModel> myAdvisersPay = new List<InformationPayViewModel>();
            List<InformationPayViewModel> infoPay = ShowPays();
            List<int> subAdviserIds = new List<int>();
            subAdviserIds.Add(adviserId);

            List<Adviser> subAdviser = _adviserService.getAdvisersByParentId(adviserId);

            foreach (var item in subAdviser)
            {
                subAdviserIds.Add(item.AdviserId);

                List<Adviser> sub2Adviser = _adviserService.getAdvisersByParentId(item.AdviserId);

                foreach (var items in sub2Adviser)
                {
                    subAdviserIds.Add(items.AdviserId);
                }
            }

            foreach (var item in subAdviserIds)
            {
                List<InformationPayViewModel> result = infoPay.Where(i => i.AdviserId == item).ToList();

                foreach (var items in result)
                {
                    myAdvisersPay.Add(items);
                }
            }

            IEnumerable<InformationPayViewModel> pay = myAdvisersPay;

            if (!string.IsNullOrEmpty(payDate))
            {
                pay = pay.Where(r => r.PayDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                pay = pay.Where(r => r.PayDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                pay = pay.Where(r => r.PayDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                pay = pay.Where(r => r.PayDate.Date >= fDate && r.PayDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                pay = pay.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                pay = pay.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                pay = pay.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (!string.IsNullOrEmpty(studentFamily))
            {
                pay = pay.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(pursuitCode))
            {
                pay = pay.Where(a => a.PursuitCode.Contains(pursuitCode));
            }

            if (filterAdviser != 0)
            {
                pay = pay.Where(p => p.AdviserId == filterAdviser);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            PayViewModel list = new PayViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)pay.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationPay = pay.OrderByDescending(u => u.PayDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public PayViewModel GetPayByRegisterId(int pageId = 1, int registerId = 0, string payDate = "", string fromDate = "", string toDate = "", string pursuitCode = "", int fromAmount = 0,
            int toAmount = 0)
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(payDate))
            {
                date = Convert.ToDateTime(payDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            IEnumerable<InformationPayViewModel> pay = ShowPaysByRegisterId(registerId);

            if (!string.IsNullOrEmpty(payDate))
            {
                pay = pay.Where(r => r.PayDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                pay = pay.Where(r => r.PayDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                pay = pay.Where(r => r.PayDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                pay = pay.Where(r => r.PayDate.Date >= fDate && r.PayDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                pay = pay.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                pay = pay.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                pay = pay.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (registerId != 0)
            {
                pay = pay.Where(a => a.RegisterId == registerId);
            }

            if (!string.IsNullOrEmpty(pursuitCode))
            {
                pay = pay.Where(a => a.PursuitCode.Contains(pursuitCode));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            PayViewModel list = new PayViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)pay.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationPay = pay.OrderByDescending(u => u.PayDate).Skip(skip).Take(take).ToList();

            return list;
        }

        #region Dapper

        public List<InformationPayViewModel> ShowPays()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationPayViewModel>("ShowPays", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationPayViewModel> ShowPaysByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationPayViewModel>("ShowPaysByRegisterId",new{ registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public int GetSumAmountPay()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@sumAmount", "", DbType.Int32, direction: ParameterDirection.Output);

                db.Query<Pay>("GetSumAmountPay", p, commandType: CommandType.StoredProcedure);

                var sumAmount = p.Get<int>("@sumAmount");

                return sumAmount;
            }
        }

        public int GetSumAmountPayByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@registerId", registerId);
                p.Add("@sumAmount", "", DbType.Int32, direction: ParameterDirection.Output);

                db.Query<Pay>("GetSumAmountPayByRegisterId", p, commandType: CommandType.StoredProcedure);

                var sumAmount = p.Get<int>("@sumAmount");

                return sumAmount;
            }
        }

        public string GetPayTypeById(int payTypeId)
        {
            PayType payType = _context.PayTypes.Find(payTypeId);

            return payType.PayTypeTitle;
        }

        public List<SelectListItem> GetPayTypeListItem()
        {
            return _context.PayTypes.Select(p => new SelectListItem()
            {
                Text = p.PayTypeTitle,
                Value = p.PayTypeId.ToString()

            }).ToList();
        }

        #endregion

        #endregion

        #region Cheque

        public int AddCheque(Cheque cheque)
        {
            _context.Cheques.Add(cheque);
            _context.SaveChanges();
            return cheque.ChequeId;
        }

        public void EditCheque(Cheque cheque)
        {
            Cheque editCheque = GetChequeById(cheque.ChequeId);

            editCheque.ChequeId = cheque.ChequeId;
            editCheque.RegisterId = cheque.RegisterId;
            editCheque.AccountNumber = cheque.AccountNumber;
            editCheque.Amount = cheque.Amount;
            editCheque.ChequeDate = cheque.ChequeDate;
            editCheque.ChequeNumber = cheque.ChequeNumber;
            editCheque.ForWho = cheque.ForWho;
            editCheque.Owner = cheque.Owner;
            editCheque.Description = cheque.Description;
            editCheque.IsPass = cheque.IsPass;

            UpdateCheque(editCheque);
        }

        public void UpdateCheque(Cheque cheque)
        {
            _context.Cheques.Update(cheque);
            _context.SaveChanges();
        }

        public void DeleteCheque(int chequeId)
        {
            Cheque cheque = GetChequeById(chequeId);
            _context.Cheques.Remove(cheque);
            _context.SaveChanges();
        }

        public void PassCheque(int chequeId)
        {
            Cheque cheque = GetChequeById(chequeId);
            cheque.IsPass = true;

            UpdateCheque(cheque);
        }

        public Cheque GetDataForEditCheque(int chequeId)
        {
            return _context.Cheques.Where(c => c.ChequeId == chequeId).Select(c => new Cheque()
            {
                ChequeId = chequeId,
                AccountNumber = c.AccountNumber,
                Amount = c.Amount,
                ChequeDate = c.ChequeDate,
                ChequeNumber = c.ChequeNumber,
                Description = c.Description,
                ForWho = c.ForWho,
                Owner = c.Owner,
                RegisterId = c.RegisterId,
                IsPass = c.IsPass
            }).Single();
        }

        public Cheque GetChequeById(int chequeId)
        {
            return _context.Cheques.Find(chequeId);
        }

        public ChequeViewModel GetCheques(int pageId = 1, int adviserId = 0, string studentFamily = "", string chequeDate = "", string fromDate = "",
            string toDate = "", string owner = "", int fromAmount = 0, int toAmount = 0, string chequeNumber = "")
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(chequeDate))
            {
                date = Convert.ToDateTime(chequeDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            IEnumerable<InformationChequeViewModel> cheque = ShowCheques();

            if (!string.IsNullOrEmpty(chequeDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date >= fDate && r.ChequeDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                cheque = cheque.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                cheque = cheque.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                cheque = cheque.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (adviserId != 0)
            {
                cheque = cheque.Where(a => a.AdviserId == adviserId);
            }

            if (!string.IsNullOrEmpty(studentFamily))
            {
                cheque = cheque.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(owner))
            {
                cheque = cheque.Where(a => a.Owner.Contains(owner));
            }

            if (!string.IsNullOrEmpty(chequeNumber))
            {
                cheque = cheque.Where(a => a.ChequeNumber.Contains(chequeNumber));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            ChequeViewModel list = new ChequeViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)cheque.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationCheque = cheque.OrderByDescending(u => u.ChequeDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public ChequeViewModel GetChequesForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "",
            string chequeDate = "", string fromDate = "", string toDate = "", string owner = "", int fromAmount = 0,
            int toAmount = 0, string chequeNumber = "")
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(chequeDate))
            {
                date = Convert.ToDateTime(chequeDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            List<InformationChequeViewModel> myAdvisersCheque = new List<InformationChequeViewModel>();
            List<InformationChequeViewModel> infoCheque = ShowCheques();
            List<int> subAdviserIds = new List<int>();
            subAdviserIds.Add(adviserId);

            List<Adviser> subAdviser = _adviserService.getAdvisersByParentId(adviserId);

            foreach (var item in subAdviser)
            {
                subAdviserIds.Add(item.AdviserId);

                List<Adviser> sub2Adviser = _adviserService.getAdvisersByParentId(item.AdviserId);

                foreach (var items in sub2Adviser)
                {
                    subAdviserIds.Add(items.AdviserId);
                }
            }

            foreach (var item in subAdviserIds)
            {
                List<InformationChequeViewModel> result = infoCheque.Where(i => i.AdviserId == item).ToList();

                foreach (var items in result)
                {
                    myAdvisersCheque.Add(items);
                }
            }

            IEnumerable<InformationChequeViewModel> cheque = myAdvisersCheque;

            if (!string.IsNullOrEmpty(chequeDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date >= fDate && r.ChequeDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                cheque = cheque.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                cheque = cheque.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                cheque = cheque.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (!string.IsNullOrEmpty(studentFamily))
            {
                cheque = cheque.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(owner))
            {
                cheque = cheque.Where(a => a.Owner.Contains(owner));
            }

            if (!string.IsNullOrEmpty(chequeNumber))
            {
                cheque = cheque.Where(a => a.ChequeNumber.Contains(chequeNumber));
            }

            if (filterAdviser != 0)
            {
                cheque = cheque.Where(c => c.AdviserId == filterAdviser);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            ChequeViewModel list = new ChequeViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)cheque.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationCheque = cheque.OrderByDescending(u => u.ChequeDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public ChequeViewModel GetChequeByRegisterId(int pageId = 1, int registerId = 0, string chequeDate = "", string fromDate = "",
            string toDate = "", string owner = "", int fromAmount = 0, int toAmount = 0, string chequeNumber = "")
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(chequeDate))
            {
                date = Convert.ToDateTime(chequeDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            IEnumerable<InformationChequeViewModel> cheque = ShowChequeByRegisterId(registerId);

            if (!string.IsNullOrEmpty(chequeDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                cheque = cheque.Where(r => r.ChequeDate.Date >= fDate && r.ChequeDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                cheque = cheque.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                cheque = cheque.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                cheque = cheque.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (registerId != 0)
            {
                cheque = cheque.Where(a => a.RegisterId == registerId);
            }

            if (!string.IsNullOrEmpty(owner))
            {
                cheque = cheque.Where(a => a.Owner.Contains(owner));
            }

            if (!string.IsNullOrEmpty(chequeNumber))
            {
                cheque = cheque.Where(a => a.ChequeNumber.Contains(chequeNumber));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            ChequeViewModel list = new ChequeViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)cheque.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationCheque = cheque.OrderByDescending(u => u.ChequeDate).Skip(skip).Take(take).ToList();

            return list;
        }

        #region Dapper

        public List<InformationChequeViewModel> ShowCheques()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationChequeViewModel>("ShowCheques", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationChequeViewModel> ShowChequeByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationChequeViewModel>("ShowChequeByRegisterId", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public int GetSumAmountCheque()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@sumAmount", "", DbType.Int32, direction: ParameterDirection.Output);

                db.Query<Cheque>("GetSumAmountCheque", p, commandType: CommandType.StoredProcedure);

                var sumAmount = p.Get<int>("@sumAmount");

                return sumAmount;
            }
        }

        public int GetSumAmountChequeByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@registerId", registerId);
                p.Add("@sumAmount", "", DbType.Int32, direction: ParameterDirection.Output);

                db.Query<Cheque>("GetSumAmountChequeByRegisterId", p, commandType: CommandType.StoredProcedure);

                var sumAmount = p.Get<int>("@sumAmount");

                return sumAmount;
            }
        }

        #endregion

        #endregion

        #region Installment

        public int AddInstallment(Installment installment)
        {
            _context.Installments.Add(installment);
            _context.SaveChanges();
            return installment.InstallmentId;
        }

        public void EditInstallment(Installment installment)
        {
            Installment editInstallment = GetInstallmentById(installment.InstallmentId);

            editInstallment.InstallmentId = installment.InstallmentId;
            editInstallment.RegisterId = installment.RegisterId;
            editInstallment.InstallmentDate = installment.InstallmentDate;
            editInstallment.Amount = installment.Amount;
            editInstallment.Description = installment.Description;
            editInstallment.IsPayed = installment.IsPayed;

            UpdateInstallment(editInstallment);
        }

        public void DeleteInstallment(int installmentId)
        {
            Installment installment = GetInstallmentById(installmentId);

            _context.Installments.Remove(installment);
            _context.SaveChanges();
        }

        public void UpdateInstallment(Installment installment)
        {
            _context.Installments.Update(installment);
            _context.SaveChanges();
        }

        public void PayInstallment(int installmentId)
        {
            Installment installment = GetInstallmentById(installmentId);

            installment.IsPayed = true;
            UpdateInstallment(installment);
        }

        public Installment GetDataForEditInstallment(int installmentId)
        {
            return _context.Installments.Where(i => i.InstallmentId == installmentId).Select(i => new Installment()
            {
                InstallmentId = installmentId,
                RegisterId = i.RegisterId,
                InstallmentDate = i.InstallmentDate,
                Amount = i.Amount,
                Description = i.Description,
                IsPayed = i.IsPayed
            }).Single();
        }

        public Installment GetInstallmentById(int installmentId)
        {
            return _context.Installments.Find(installmentId);
        }

        public InstallmentViewModel GetInstallments(int pageId = 1, int adviserId = 0, string studentFamily = "",
            string installmentDate = "", string fromDate = "", string toDate = "", int fromAmount = 0, int toAmount = 0)
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(installmentDate))
            {
                date = Convert.ToDateTime(installmentDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            IEnumerable<InformationInstallmentViewModel> installments = ShowInstallments();

            if (!string.IsNullOrEmpty(installmentDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date >= fDate && r.InstallmentDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                installments = installments.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                installments = installments.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                installments = installments.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (adviserId != 0)
            {
                installments = installments.Where(a => a.AdviserId == adviserId);
            }

            if (!string.IsNullOrEmpty(studentFamily))
            {
                installments = installments.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            InstallmentViewModel list = new InstallmentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)installments.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationInstallment = installments.OrderByDescending(u => u.InstallmentDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public InstallmentViewModel GetInstallmentsForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0,
            string studentFamily = "", string installmentDate = "",
            string fromDate = "", string toDate = "", int fromAmount = 0, int toAmount = 0)
        {
            var date = DateTime.Now;
            var fDate = DateTime.Now;
            var tDate = DateTime.Now;

            if (!string.IsNullOrEmpty(installmentDate))
            {
                date = Convert.ToDateTime(installmentDate).Date;
            }

            if (!string.IsNullOrEmpty(fromDate))
            {
                fDate = Convert.ToDateTime(fromDate).Date;
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                tDate = Convert.ToDateTime(toDate).Date;
            }

            List<InformationInstallmentViewModel> myAdvisersInstallment = new List<InformationInstallmentViewModel>();
            List<InformationInstallmentViewModel> infoInstallment = ShowInstallments();
            List<int> subAdviserIds = new List<int>();
            subAdviserIds.Add(adviserId);

            List<Adviser> subAdviser = _adviserService.getAdvisersByParentId(adviserId);

            foreach (var item in subAdviser)
            {
                subAdviserIds.Add(item.AdviserId);

                List<Adviser> sub2Adviser = _adviserService.getAdvisersByParentId(item.AdviserId);

                foreach (var items in sub2Adviser)
                {
                    subAdviserIds.Add(items.AdviserId);
                }
            }

            foreach (var item in subAdviserIds)
            {
                List<InformationInstallmentViewModel> result = infoInstallment.Where(i => i.AdviserId == item).ToList();

                foreach (var items in result)
                {
                    myAdvisersInstallment.Add(items);
                }
            }

            IEnumerable<InformationInstallmentViewModel> installments = myAdvisersInstallment;

            if (!string.IsNullOrEmpty(installmentDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date == date);
            }

            if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date >= fDate);
            }
            else if (!string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(fromDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date <= tDate);
            }
            else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                installments = installments.Where(r => r.InstallmentDate.Date >= fDate && r.InstallmentDate.Date <= tDate);
            }

            if (fromAmount != 0 && toAmount == 0)
            {
                installments = installments.Where(r => r.Amount >= fromAmount);
            }
            else if (toAmount != 0 && fromAmount == 0)
            {
                installments = installments.Where(r => r.Amount <= toAmount);
            }
            else if (fromAmount != 0 && toAmount != 0)
            {
                installments = installments.Where(r => r.Amount >= fromAmount && r.Amount <= toAmount);
            }

            if (!string.IsNullOrEmpty(studentFamily))
            {
                installments = installments.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (filterAdviser != 0)
            {
                installments = installments.Where(i => i.AdviserId == filterAdviser);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            InstallmentViewModel list = new InstallmentViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)installments.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationInstallment = installments.OrderByDescending(u => u.InstallmentDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public List<InformationInstallmentViewModel> ShowInstallments()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationInstallmentViewModel>("ShowInstallments", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        #endregion

    }
}
