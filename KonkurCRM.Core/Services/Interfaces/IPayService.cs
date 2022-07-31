using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Salary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface IPayService
    {
        #region Pay

        int AddPay(Pay pay);
        void EditPay(EditPayViewModel editPay);
        void UpdatePay(Pay pay);   
        void DeletePay(int payId);
        EditPayViewModel GetDataForEditPay(int payId);
        Pay GetPayById(int payId);
        Pay GetFirstPayByRegisterId(int registerId);
        PayViewModel GetPays(int pageId = 1, int adviserId = 0, string studentFamily = "", string payDate = "",string fromDate="",string toDate="",string pursuitCode="",int fromAmount=0, int toAmount=0);
        PayViewModel GetPaysForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "", string payDate = "",string fromDate="",string toDate="",string pursuitCode="",int fromAmount=0, int toAmount=0);
        PayViewModel GetPayByRegisterId(int pageId = 1, int registerId = 0, string payDate = "", string fromDate = "", string toDate = "", string pursuitCode = "", int fromAmount = 0, int toAmount = 0);
        List<InformationPayViewModel> ShowPays();
        List<InformationPayViewModel> ShowPaysByRegisterId(int registerId);
        int GetSumAmountPay();
        int GetSumAmountPayByRegisterId(int registerId);
        string GetPayTypeById(int payTypeId);
        List<SelectListItem> GetPayTypeListItem();

        #endregion

        #region Cheque

        int AddCheque(Cheque cheque);
        void EditCheque(Cheque cheque);
        void UpdateCheque(Cheque cheque);
        void DeleteCheque(int chequeId);
        void PassCheque(int chequeId);
        Cheque GetDataForEditCheque(int chequeId);
        Cheque GetChequeById(int chequeId);
        ChequeViewModel GetCheques(int pageId = 1, int adviserId = 0, string studentFamily = "", string chequeDate = "", string fromDate = "", string toDate = "", string owner = "", int fromAmount = 0,
            int toAmount = 0,string chequeNumber="");
        ChequeViewModel GetChequesForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "", string chequeDate = "", string fromDate = "", string toDate = "", string owner = "", int fromAmount = 0,
            int toAmount = 0, string chequeNumber = "");
        ChequeViewModel GetChequeByRegisterId(int pageId = 1, int registerId = 0, string chequeDate = "", string fromDate = "", string toDate = "", string owner = "", int fromAmount = 0,
            int toAmount = 0, string chequeNumber = "");
        List<InformationChequeViewModel> ShowCheques();
        List<InformationChequeViewModel> ShowChequeByRegisterId(int registerId);
        int GetSumAmountCheque();
        int GetSumAmountChequeByRegisterId(int registerId);

        #endregion

        #region Installment

        int AddInstallment(Installment installment);
        void EditInstallment(Installment installment);
        void DeleteInstallment(int installmentId);
        void UpdateInstallment(Installment installment);
        void PayInstallment(int installmentId);
        Installment GetDataForEditInstallment(int installmentId);
        Installment GetInstallmentById(int installmentId);
        InstallmentViewModel GetInstallments(int pageId = 1, int adviserId = 0,string studentFamily = "", string installmentDate = "",
            string fromDate = "", string toDate = "", int fromAmount = 0, int toAmount = 0);
        InstallmentViewModel GetInstallmentsForAdviserPanel(int pageId = 1, int adviserId = 0, int filterAdviser = 0, string studentFamily = "", string installmentDate = "",
            string fromDate = "", string toDate = "", int fromAmount = 0, int toAmount = 0);
        List<InformationInstallmentViewModel> ShowInstallments();

        #endregion


    }
}
