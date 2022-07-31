using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.DTOs.Adviser;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Calls;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class CallService : ICallService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;

        public CallService(KonkurCRMContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        #region Calls

        public int AddCall(Call call)
        {
            _context.Calls.Add(call);
            _context.SaveChanges();
            return call.CallId;
        }

        public void EditCall(Call call)
        {
            Call editCall = GetCallById(call.CallId);

            editCall.CallId = call.CallId;
            editCall.CallDate = call.CallDate;
            editCall.AdviserId = call.AdviserId;
            editCall.RegisterId = call.RegisterId;
            editCall.CallTime = call.CallTime;
            editCall.IsCall = call.IsCall;
            editCall.Subject = call.Subject;
            editCall.Description = call.Description;

            UpdateCall(editCall);
        }

        public void DeleteCall(int callId)
        {
            Call call = GetCallById(callId);
            _context.Calls.Remove(call);

            _context.SaveChanges();
        }

        public Call GetCallById(int callId)
        {
            return _context.Calls.Find(callId);
        }

        public List<Call> GetCallByRegisterId(int registerId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<Call>("GetCallByRegisterId", new { registerId = registerId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public CallViewModel GetCalls(int pageId = 1, int adviserId = 0, string studentFamily = "", string callDate = "")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(callDate))
            {
                date = Convert.ToDateTime(callDate).Date;
            }

            IEnumerable<InformationCallViewModel> calls = ShowCall(adviserId);
            

            if (!string.IsNullOrEmpty(studentFamily))
            {
                calls = calls.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(callDate))
            {
                calls = calls.Where(r => r.CallDate.Date == date);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            CallViewModel list = new CallViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)calls.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationCall = calls.OrderByDescending(a => a.CallDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public Call GetDataForEditCall(int callId)
        {
            return _context.Calls.Where(c => c.CallId == callId).Select(c => new Call()
            {
                CallId = callId,
                Subject = c.Subject,
                CallDate = c.CallDate,
                CallTime = c.CallTime,
                Description = c.Description,
                RegisterId = c.RegisterId,
                AdviserId = c.AdviserId,
                IsCall = c.IsCall
            }).Single();
        }

        public void UpdateCall(Call call)
        {
            _context.Calls.Update(call);
            _context.SaveChanges();
        }

        #region Dapper

        public List<InformationCallViewModel> ShowCall(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationCallViewModel>("ShowCall",new{ adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        #endregion

        #endregion

        #region FollowUp

        public int AddFollowUp(FollowUp followUp)
        {
            _context.FollowUps.Add(followUp);
            _context.SaveChanges();
            return followUp.FollowUpId;
        }

        public void EditFollowUp(FollowUp followUp)
        {
            FollowUp editFollowUp = GetFollowUpById(followUp.FollowUpId);

            editFollowUp.FollowUpId = followUp.FollowUpId;
            editFollowUp.FollowUpDateTime = followUp.FollowUpDateTime;
            editFollowUp.Description = followUp.Description;
            editFollowUp.RegisterId = followUp.RegisterId;
            editFollowUp.Subject = followUp.Subject;
            editFollowUp.IsFollowedUp = followUp.IsFollowedUp;
            editFollowUp.IsDelete = followUp.IsDelete;

            UpdateFollowUp(editFollowUp);
        }

        public void FollowedFollowUp(int followId)
        {
            FollowUp followUp = GetFollowUpById(followId);

            followUp.IsFollowedUp = true;
            UpdateFollowUp(followUp);
        }

        public void DeleteFollowUp(int followUpId)
        {
            FollowUp followUp = GetFollowUpById(followUpId);

            _context.FollowUps.Remove(followUp);
            _context.SaveChanges();
        }

        public FollowUp GetDataForEditFollowUp(int followUpId)
        {
            return _context.FollowUps.Where(f => f.FollowUpId == followUpId).Select(f => new FollowUp()
            {
                FollowUpId = followUpId,
                AdviserId = f.AdviserId,
                Subject = f.Subject,
                Description = f.Description,
                RegisterId = f.RegisterId,
                FollowUpDateTime = f.FollowUpDateTime,
                IsFollowedUp = f.IsFollowedUp
            }).Single();
        }

        public FollowUpViewModel GetFollowUps(int pageId = 1, int adviserId = 0, string studentFamily = "", string followUpDate = "")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(followUpDate))
            {
                date = Convert.ToDateTime(followUpDate).Date;
            }

            IEnumerable<InformationFollowUpViewModel> followUp = ShowFollowUp(adviserId).Where(f=>f.FollowUpId !=1);
            

            if (!string.IsNullOrEmpty(studentFamily))
            {
                followUp = followUp.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(followUpDate))
            {
                followUp = followUp.Where(r => r.FollowUpDateTime.Date == date);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            FollowUpViewModel list = new FollowUpViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)followUp.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationFollowUp = followUp.OrderBy(u => u.FollowUpDateTime).Skip(skip).Take(take).ToList();

            return list;
        }

        public FollowUp GetFollowUpById(int followUpId)
        {
            return _context.FollowUps.Find(followUpId);
        }

        public void UpdateFollowUp(FollowUp followUp)
        {
            _context.FollowUps.Update(followUp);
            _context.SaveChanges();
        }

        #region Dapper

        public List<InformationFollowUpViewModel> ShowFollowUp(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationFollowUpViewModel>("ShowFollowUp",new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        #endregion

        #endregion

        #region UnRegisteredCalls

        public int AddUnRegisteredCall(UnregisteredCalls unregisteredCalls)
        {
            _context.UnregisteredCalls.Add(unregisteredCalls);
            _context.SaveChanges();
            return unregisteredCalls.UnregisteredCallsId;
        }

        public void EditUnregisteredCalls(UnregisteredCalls unregisteredCalls)
        {
            UnregisteredCalls editUnregisteredCalls = GetUnregisteredCallsById(unregisteredCalls.UnregisteredCallsId);

            editUnregisteredCalls.UnregisteredCallsId = unregisteredCalls.UnregisteredCallsId;
            editUnregisteredCalls.AdviserId = unregisteredCalls.AdviserId;
            editUnregisteredCalls.StudentId = unregisteredCalls.StudentId;
            editUnregisteredCalls.CallDate = unregisteredCalls.CallDate;
            editUnregisteredCalls.CallTime = unregisteredCalls.CallTime;
            editUnregisteredCalls.Description = unregisteredCalls.Description;
            editUnregisteredCalls.IsCall = unregisteredCalls.IsCall;
            editUnregisteredCalls.Subject = unregisteredCalls.Subject;

            UpdateUnRegisteredCall(editUnregisteredCalls);
        }

        public void DeleteUnregisteredCall(int unregisteredCallId)
        {
            UnregisteredCalls unregisteredCalls = GetUnregisteredCallsById(unregisteredCallId);
            _context.UnregisteredCalls.Remove(unregisteredCalls);
            _context.SaveChanges();
        }

        public void UpdateUnRegisteredCall(UnregisteredCalls unregisteredCalls)
        {
            _context.UnregisteredCalls.Update(unregisteredCalls);
            _context.SaveChanges();
        }

        public UnregisteredCalls GetDataForEditUnregisteredCalls(int unregisteredCallId)
        {
            return _context.UnregisteredCalls.Where(u => u.UnregisteredCallsId == unregisteredCallId).Select(u =>
                new UnregisteredCalls()
                {
                    UnregisteredCallsId = unregisteredCallId,
                    AdviserId = u.AdviserId,
                    StudentId = u.StudentId,
                    CallDate = u.CallDate,
                    CallTime = u.CallTime,
                    Subject = u.Subject,
                    Description = u.Description,
                    IsCall = u.IsCall
                }).Single();
        }

        public UnregisteredCalls GetUnregisteredCallsById(int unregisteredCallId)
        {
            return _context.UnregisteredCalls.Find(unregisteredCallId);
        }

        public List<InformationUnregisteredCalls> ShowUnRegisteredCall(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationUnregisteredCalls>("ShowUnRegisteredCall",new{ adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public UnregisteredCallsViewModel GetUnregisteredCalls(int pageId = 1, int adviserId = 0, string studentFamily = "", string callDate = "")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(callDate))
            {
                date = Convert.ToDateTime(callDate).Date;
            }

            IEnumerable<InformationUnregisteredCalls> calls = ShowUnRegisteredCall(adviserId);

            if (!string.IsNullOrEmpty(studentFamily))
            {
                calls = calls.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(callDate))
            {
                calls = calls.Where(r => r.CallDate.Date == date);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            UnregisteredCallsViewModel list = new UnregisteredCallsViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)calls.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationUnregisteredCalls = calls.OrderByDescending(a => a.CallDate).Skip(skip).Take(take).ToList();

            return list;
        }

        #endregion

        #region UnRegisteredFollowUp

        public int AddUnRegisteredFollowUp(UnregisteredFollowUp unregisteredFollowUp)
        {
            _context.UnregisteredFollowUps.Add(unregisteredFollowUp);
            _context.SaveChanges();
            return unregisteredFollowUp.UnregisteredFollowUpId;
        }

        public void DeleteUnregisteredFollowUp(int unregisteredFollowUpId)
        {
            UnregisteredFollowUp unregisteredFollowUp = GetUnregisteredFollowUpById(unregisteredFollowUpId);

            _context.Remove(unregisteredFollowUp);
            _context.SaveChanges();
        }

        public void EditUnRegisteredFollowUp(UnregisteredFollowUp unregisteredFollowUp)
        {
            UnregisteredFollowUp edituUnregisteredFollowUp =
                GetUnregisteredFollowUpById(unregisteredFollowUp.UnregisteredFollowUpId);

            edituUnregisteredFollowUp.UnregisteredFollowUpId = unregisteredFollowUp.UnregisteredFollowUpId;
            edituUnregisteredFollowUp.AdviserId = unregisteredFollowUp.AdviserId;
            edituUnregisteredFollowUp.StudentId = unregisteredFollowUp.StudentId;
            edituUnregisteredFollowUp.Subject = unregisteredFollowUp.Subject;
            edituUnregisteredFollowUp.Description = unregisteredFollowUp.Description;
            edituUnregisteredFollowUp.FollowUpDateTime = unregisteredFollowUp.FollowUpDateTime;
            edituUnregisteredFollowUp.IsFollowedUp = unregisteredFollowUp.IsFollowedUp;

            UpdateUnRegisteredFollowUp(edituUnregisteredFollowUp);
        }

        public void FollowedUpUnRegisteredFollowUp(int unRegisteredFollowUpId)
        {
            UnregisteredFollowUp unregisteredFollowUp = GetUnregisteredFollowUpById(unRegisteredFollowUpId);

            unregisteredFollowUp.IsFollowedUp = true;
            UpdateUnRegisteredFollowUp(unregisteredFollowUp);
        }

        public UnregisteredFollowUp GetUnregisteredFollowUpById(int unregisteredFollowUpId)
        {
            return _context.UnregisteredFollowUps.Find(unregisteredFollowUpId);
        }

        public void UpdateUnRegisteredFollowUp(UnregisteredFollowUp unregisteredFollowUp)
        {
            _context.UnregisteredFollowUps.Update(unregisteredFollowUp);
            _context.SaveChanges();
        }

        public UnregisteredFollowUp GetDataForEditUnRegisteredFollowUp(int unregisteredFollowUpId)
        {
            return _context.UnregisteredFollowUps.Where(u => u.UnregisteredFollowUpId == unregisteredFollowUpId).Select(
                u => new UnregisteredFollowUp()
                {
                    UnregisteredFollowUpId = unregisteredFollowUpId,
                    AdviserId = u.AdviserId,
                    StudentId = u.StudentId,
                    Subject = u.Subject,
                    Description = u.Description,
                    FollowUpDateTime = u.FollowUpDateTime,
                    IsFollowedUp = u.IsFollowedUp
                }).Single();
        }

        public List<InformationUnRegisteredFollowUpViewModel> ShowUnRegisteredFollowUp(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationUnRegisteredFollowUpViewModel>("ShowUnRegisteredFollowUp", new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public UnRegisterFollowUpViewModel GetUnRegisterFollowUps(int pageId = 1, int adviserId = 0,
            string studentFamily = "", string followUpDate = "")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(followUpDate))
            {
                date = Convert.ToDateTime(followUpDate).Date;
            }

            IEnumerable<InformationUnRegisteredFollowUpViewModel> followUp = ShowUnRegisteredFollowUp(adviserId).Where(f=>f.UnregisteredFollowUpId != 1);


            if (!string.IsNullOrEmpty(studentFamily))
            {
                followUp = followUp.Where(a => a.StudentNameFamily.Contains(studentFamily));
            }

            if (!string.IsNullOrEmpty(followUpDate))
            {
                followUp = followUp.Where(r => r.FollowUpDateTime.Date == date);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            UnRegisterFollowUpViewModel list = new UnRegisterFollowUpViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)followUp.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationUnRegisteredFollowUp = followUp.OrderBy(u => u.FollowUpDateTime).Skip(skip).Take(take).ToList();

            return list;
        }

        #endregion

        #region NotCalled

        public void AddNotCalled(NotCalled notCalled)
        {
            _context.NotCalleds.Add(notCalled);
            _context.SaveChanges();
        }

        public void EditNotCalled(NotCalled notCalled)
        {
            NotCalled editNotCalled = GetNotCalledById(notCalled.NotCalledId);

            editNotCalled.Number = notCalled.Number;
            editNotCalled.AdviserId = notCalled.AdviserId;
            editNotCalled.AddDate = notCalled.AddDate;

            UpdateNotCalled(editNotCalled);
        }

        public void UpdateNotCalled(NotCalled notCalled)
        {
            _context.NotCalleds.Update(notCalled);
            _context.SaveChanges();
        }

        public void DeleteNotCalled(int notCalledId)
        {
            NotCalled notCalledByNum = GetNotCalledById(notCalledId);

            _context.NotCalleds.Remove(notCalledByNum);
            _context.SaveChanges();
        }

        public NotCalled GetNotCalledByNumber(string number)
        {
            return _context.NotCalleds.SingleOrDefault(n => n.Number == number);
        }

        public NotCalled GetNotCalledById(int notCalledId)
        {
            return _context.NotCalleds.Find(notCalledId);
        }

        public List<NotCalled> GetNotCalledsByAdviserId(int adviserId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<NotCalled>("GetNotCalledsByAdviserId",new { adviserId = adviserId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationNotCalledViewModel> ShowAllNotCalled()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationNotCalledViewModel>("ShowAllNotCalled", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public NotCalledViewModel GetNotCalleds(int pageId = 1, string addDate = "")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(addDate))
            {
                date = Convert.ToDateTime(addDate).Date;
            }

            IEnumerable<InformationNotCalledViewModel> calls = ShowAllNotCalled().Where(c=>c.IsDedicated == false);

            
            if (!string.IsNullOrEmpty(addDate))
            {
                calls = calls.Where(r => r.AddDate.Date == date);
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            NotCalledViewModel list = new NotCalledViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)calls.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationNotCalled = calls.OrderByDescending(a => a.AddDate).Skip(skip).Take(take).ToList();

            return list;
        }

        #endregion
    }
}
