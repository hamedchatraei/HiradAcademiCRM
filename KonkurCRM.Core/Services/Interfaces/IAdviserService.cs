using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.Convertor;
using KonkurCRM.Core.DTOs.Adviser;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.DataLayer.Entities.Advisers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface IAdviserService
    {
        #region Adviser

        int AddAdviser(Adviser adviser);
        void EditAdviser(EditAdviserViewModel editAdviser);
        void UpdateAdviser(Adviser adviser);
        void DeleteAdviser(int adviserId);
        List<InformationAdviserViewModel> ShowAdvisers();
        List<InformationAdviserViewModel> ShowAdvisersForAdviserPanel(int adviserId);
        List<InformationAdviserViewModel> ShowDeletedAdvisers();
        List<InformationAdvisersRegisterViewModel> GetRegistersByAdviserId(int adviserId);
        List<InformationRegistersAdviserViewModel> GetInformationRegisterByAdviserId(int adviserId);
        List<InformationAdvisersRegisterViewModel> GetRegistersByAdviserIdWithPlanAndCourse(int adviserId);
        List<InformationAdvisersRegisterViewModel> GetRegistersByAdviserIdWithPlanAndCourseForLastMonth(int adviserId);
        AdviserViewModel GetAdvisers(int pageId = 1, string family = "", string mobile = "" , int groupId = 0);
        AdviserViewModel GetAdvisersForAdviserPanel(int pageId = 1,int adviserId = 0,int filterAdviser = 0, string family = "", string mobile = "");
        AdviserViewModel GetDeletedAdvisers(int pageId = 1, string family = "", string mobile = "", int groupId = 0);
        EditAdviserViewModel GetDataForEditAdviser(int adviserId);
        Adviser GetAdviserById(int adviserId);
        Adviser GetAdviserByUserId(int userId);
        List<Adviser> getAdvisersByParentId(int parentId);
        List<Adviser> GetAllAdvisers();
        int CommissionAdviserForMonth(int adviserId);
        List<AdviserInvoiceViewModel> ShowAdviserInvoices(int adviserId);
        List<InformationPayByRegisterViewModel> GetPayByAdviserId(int adviserId);
        List<InformationChequeByRegisterViewModel> GetChequeByAdviserId(int adviserId);
        List<SelectListItem> GetAdviserListItems();
        List<SelectListItem> GetAdviserListItemsByType(int id);
        List<SelectListItem> GetSubAdviserListItems(int adviserId);
        List<SelectListItem> GetVendorAdviserListItems();
        List<SelectListItem> GetSyllabusAdviserListItems();
        List<SelectListItem> GetRegisterAdviserForFollowUpListItems(int adviserId);

        #endregion

        #region GroupAdviser

        int AddGroupAdviser(GroupAdviser groupAdviser);
        void UpdateGroupAdviser(GroupAdviser groupAdviser);
        void DeleteGroupAdviser(int groupAdviserId);
        GroupAdviserViewModel GetGroupAdvisers(int pageId = 1);
        GroupAdviser GetGroupAdviserById(int groupAdviserId);
        List<AdviserGroup> GetGroupAdviserByAdviserId(int adviserId);
        void AddGroupToAdviser(List<int> groupIds, int adviserId);
        void UpdateAdvisersGroup(int adviserId, List<int> groupIds);
        List<int> GetAdvisersGroup(int adviserId);
        List<SelectListItem> GetGroupAdviserListItems();
        List<string> GroupAdviserTilte(int adviserId);
        string GetGroupTitleByGroupId(int groupId);

        #endregion

        #region AdviserInvoice

        void AddInvoice(AdviserInvoice adviserInvoice);
        EditAdviserInvoiceViewModel GetDataForEditAdviserInvoice(int adviserInvoiceId);
        void EditAdviserInvoice(EditAdviserInvoiceViewModel adviserInvoice);
        AdviserInvoice GetAdviserInvoiceById(int adviserInvoiceId);
        void UpdateInvoice(AdviserInvoice adviserInvoice);
        void DeleteInvoice(int invoiceId);
        List<AdviserInvoice> getInvoices();
        List<AdviserInvoiceViewModel> ShowInformationAdviserInvoice();
        InvoiceViewModel showAdviserInvoices(int pageId = 1, int adviserId = 0, string fromDate = "", string toDate = "");
        List<SelectListItem> GetSalaryTypeListItems();

        #endregion

    }
}
