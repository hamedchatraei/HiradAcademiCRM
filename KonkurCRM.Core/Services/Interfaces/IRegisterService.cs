using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Pay;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Register;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface IRegisterService
    {
        #region Register

        int AddRegister(Register register);
        void EditRegister(EditRegisterViewModel editRegister);
        void UpdateRegister(Register register);
        void DeleteRegister(int registerId);
        EditRegisterViewModel GetDataForEditRegister(int registerId);
        Register GetRegisterById(int registerId);
        Register GetRegisterByStudentId(int studentId);
        RegisterViewModel GetRegisters(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0, int courseId = 0);
        RegisterViewModel GetRegistersByAdviserId(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0, int courseId = 0);
        RegisterViewModel GetRegistersByPlanId(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0, int courseId = 0);
        RegisterViewModel GetRegistersByCourseId(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0, int courseId = 0);
        RegisterViewModel GetDeletedRegisters(int pageId = 1, int adviserId = 0, int studentId = 0, int planId = 0, int courseId = 0);
        RegisterViewModel GetAdviserStudents(int pageId = 1, int adviserId = 0,string family = "", int planId = 0 , int courseId = 0);
        int GetSumAmountPayAdviserForLastMonth(int adviserId);
        int GetTotalSumAmountPayAdviser(int adviserId);
        int GetSumAmountPayAdviserByCourseIdForLastMonth(int adviserId,int courseId);
        int GetTotalSumAmountPayAdviserByCourseId(int adviserId, int courseId);
        int GetSumAmountPayAdviserByPlanIdForLastMonth(int adviserId,int planId);
        int GetTotalSumAmountPayAdviserByPlanId(int adviserId, int planId);
        int GetSumAmountPayCourseForLastMonth(int courseId);
        int GetTotalSumAmountPayCourse(int courseId);
        int GetSumAmountPayPlanForLastMonth(int planId);
        int GetTotalSumAmountPayPlan(int planId);
        List<Register_sAdviser> GetRegisterAdviserForLastMonth(int adviserId);
        List<InformationRegisterViewModel> ShowRegisters();
        List<InformationRegisterViewModel> ShowDeletedRegisters();
        List<InformationRegisterViewModel> ShowRegisterByAdviserId(int adviserId);
        List<InformationRegisterViewModel> ShowRegisterByStudentId(int studentId);
        List<InformationRegisterViewModel> GetAllRegisters();
        List<InformationAdvisersRegisterViewModel> GetRegisterByPlanId(int planId);
        List<InformationAdvisersRegisterViewModel> GetRegisterByCourseId(int courseId);
        List<Register_sAdviser> GetRegisterByAdviserId(int adviserId);
        List<InformationPayByRegisterViewModel> GetPayByRegisterId(int registerId,int adviserId);
        List<InformationChequeByRegisterViewModel> GetChequeByRegisterId(int registerId,int adviserId);
        List<InformationPayByRegisterViewModel> GetAllPayByRegisterId(int registerId);
        List<InformationChequeByRegisterViewModel> GetAllChequeByRegisterId(int registerId);
        List<Installment> GetInstallmentByRegisterId(int registerId);
        Register GetLastRegisterByStudentId(int studentId);
        List<InformationRegistersAdviserViewModel> ShowInformationRegisterByRegisterId(int registerId);
        List<SelectListItem> GetAllRegistersListItems();

        #endregion

        #region register'sAdviser

        int AddRegistersAdviser(Register_sAdviser registerSAdviser);
        void EditRegistersAdviser(Register_sAdviser registerSAdviser);
        void DeActiveRegistersAdviser(int registerId,int typeId, Register_sAdviser registerSAdviser);
        void DeleteRegistersAdviser(int raId);
        void UpdateRegistersAdviser(Register_sAdviser registerSAdviser);
        Register_sAdviser GetRegistersAdviserById(int raId);
        Register_sAdviser GetRegistersAdviserByRegisterId(int registerId);
        Register_sAdviser GetDataForEditRegisterSAdviser(int raId);
        void AddAdviserToRegister(List<int> adviserIds, int registerId);
        void UpdateRegistersAdviser(int registerId, List<int> adviserIds);
        List<int> GetRegistersAdviser(int registerId);

        #endregion

    }
}
