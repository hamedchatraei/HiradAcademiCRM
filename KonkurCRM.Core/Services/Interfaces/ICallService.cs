using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.DataLayer.Entities.Calls;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface ICallService
    {
        #region Call

        int AddCall(Call call);
        void UpdateCall(Call call);
        void EditCall(Call call);
        void DeleteCall(int callId);
        List<InformationCallViewModel> ShowCall(int adviserId);
        CallViewModel GetCalls(int pageId = 1, int adviserId = 0, string studentFamily = "",string callDate = "");
        Call GetDataForEditCall(int callId);
        Call GetCallById(int callId);
        List<Call> GetCallByRegisterId(int registerId);

        #endregion

        #region FollowUp

        int AddFollowUp(FollowUp followUp);
        void EditFollowUp(FollowUp followUp);
        void FollowedFollowUp(int followId);
        void UpdateFollowUp(FollowUp followUp);
        void DeleteFollowUp(int followUpId);
        List<InformationFollowUpViewModel> ShowFollowUp(int adviserId);
        FollowUpViewModel GetFollowUps(int pageId=1,int adviserId=0, string studentFamily="",string followUpDate="");
        FollowUp GetDataForEditFollowUp(int followUpId);
        FollowUp GetFollowUpById(int followUpId);


        #endregion

        #region UnRegisteredCall

        int AddUnRegisteredCall(UnregisteredCalls unregisteredCalls);
        void EditUnregisteredCalls(UnregisteredCalls unregisteredCalls);
        void DeleteUnregisteredCall(int unregisteredCallId);
        void UpdateUnRegisteredCall(UnregisteredCalls unregisteredCalls);
        UnregisteredCalls GetDataForEditUnregisteredCalls(int unregisteredCallId);
        UnregisteredCalls GetUnregisteredCallsById(int unregisteredCallId);
        List<InformationUnregisteredCalls> ShowUnRegisteredCall(int adviserId);
        UnregisteredCallsViewModel GetUnregisteredCalls(int pageId = 1, int adviserId = 0, string studentFamily = "", string callDate = "");

        #endregion

        #region UnRegisteredFollowUp

        int AddUnRegisteredFollowUp(UnregisteredFollowUp unregisteredFollowUp);
        void DeleteUnregisteredFollowUp(int unregisteredFollowUpId);
        void EditUnRegisteredFollowUp(UnregisteredFollowUp unregisteredFollowUp);
        void FollowedUpUnRegisteredFollowUp(int unRegisteredFollowUpId);
        UnregisteredFollowUp GetUnregisteredFollowUpById(int unregisteredFollowUpId);
        void UpdateUnRegisteredFollowUp(UnregisteredFollowUp unregisteredFollowUp);
        UnregisteredFollowUp GetDataForEditUnRegisteredFollowUp(int unregisteredFollowUpId);
        List<InformationUnRegisteredFollowUpViewModel> ShowUnRegisteredFollowUp(int adviserId);
        UnRegisterFollowUpViewModel GetUnRegisterFollowUps(int pageId = 1, int adviserId = 0, string studentFamily = "", string followUpDate = "");

        #endregion

        #region NotCalled

        void AddNotCalled(NotCalled notCalled);
        void EditNotCalled(NotCalled notCalled);
        void UpdateNotCalled(NotCalled notCalled);
        void DeleteNotCalled(int notCalledId);
        NotCalled GetNotCalledByNumber(string number);
        NotCalled GetNotCalledById(int notCalledId);
        List<NotCalled> GetNotCalledsByAdviserId(int adviserId);
        List<InformationNotCalledViewModel> ShowAllNotCalled();
        NotCalledViewModel GetNotCalleds(int pageId = 1, string callDate = "");

        #endregion
    }
}
