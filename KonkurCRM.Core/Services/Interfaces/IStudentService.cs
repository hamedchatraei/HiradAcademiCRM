using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.DTOs.Student;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Register;
using KonkurCRM.DataLayer.Entities.Students;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface IStudentService
    {
        int AddStudent(Student student);
        void EditStudent(EditStudentViewModel editStudent);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        void CancelStudent(int studentId);
        List<InformationStudentViewModel> ShowStudents();
        List<InformationStudentViewModel> ShowUnregisteredStudents();
        List<InformationStudentViewModel> ShowUnRegisteredStudentByAdviser();
        List<InformationStudentViewModel> ShowDeletedStudents();
        List<InformationStudentViewModel> ShowCanceledStudents();
        StudentViewModel GetStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "",int studyId = 0 , int gradeId = 0);
        StudentViewModel GetUnregisteredStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        StudentViewModel GetDeletedStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        StudentViewModel GetCanceledStudents(int pageId = 1, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        StudentViewModel GetAdviserPanelStudents(int pageId = 1,int adviserId=0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        StudentViewModel GetAdviserPanelUnregisteredStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        StudentViewModel GetAdviserPanelDeletedStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        StudentViewModel GetAdviserPanelCanceledStudents(int pageId = 1, int adviserId = 0, string family = "", string mobile = "", string phone = "", string city = "", int studyId = 0, int gradeId = 0);
        EditStudentViewModel GetDataForEditStudent(int studentId);
        Student GetStudentById(int studentId);
        string GetGradeTitleById(int gradeId);
        string GetStudyTitleById(int studyId);
        int ShowAmountDebtorStudent(int studentId);
        List<InformationRegistersAdviserViewModel> ShowStudentsAdviserForLastRegister(int studentId);
        IList<InformationRegistersAdviserViewModel> ShowStudentsAdviserForAllRegister(int studentId);
        List<SelectListItem> GetGradeListItems();
        List<SelectListItem> GetStudyListItems();
        List<SelectListItem> GetStudentsForUnRegisterFollowUpListItems(int adviserId);
        List<Register> GetStudentsRegisters(int studentId);
        List<Pay> GetDebtorStudents(int registerId);
        List<InformationRegistersAdviserViewModel> GetStudentsAdviser(int registerId);
        List<InformationUnregisteredCalls> GetUnregisteredCallsByStudentId(int studentId);
    }
}
