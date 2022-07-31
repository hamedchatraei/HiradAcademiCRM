using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Course;
using KonkurCRM.DataLayer.Entities.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface ICourseService
    {
        int AddCourse(Course course);
        void EditCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
        List<InformationCourseViewModel> ShowCourse();
        List<InformationCourseViewModel> ShowDeletedCourse();
        CourseViewModel GetCourses(int pageId = 1, string courseDate = "",string title ="");
        CourseViewModel GetDeletedCourse(int pageId = 1, string courseDate = "", string title = "");
        Course GetDataForEditCourse(int courseId);
        Course GetCourseById(int courseId);
        List<SelectListItem> GetCourseListItems();
        string GetCourseTitleById(int courseId);
    }
}
