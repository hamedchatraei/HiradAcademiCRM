using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.Convertor;
using KonkurCRM.Core.DTOs.Call;
using KonkurCRM.Core.DTOs.Course;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class CourseService : ICourseService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;

        public CourseService(KonkurCRMContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public int AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course.CourseId;
        }

        public void EditCourse(Course course)
        {
            Course editCourse = GetCourseById(course.CourseId);

            editCourse.CourseId = course.CourseId;
            editCourse.CourseTitle = course.CourseTitle;
            editCourse.CourseDate = course.CourseDate;
            editCourse.Origin = course.Origin;
            editCourse.PercentAmount = course.PercentAmount;
            editCourse.Description = course.Description;

            UpdateCourse(editCourse);
        }

        public void DeleteCourse(int courseId)
        {
            Course course = GetCourseById(courseId);
            course.IsDelete = true;
            UpdateCourse(course);
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public List<SelectListItem> GetCourseListItems()
        {
            return _context.Courses.Where(u => u.IsDelete == false).Select(c => new SelectListItem()
            {
                Text = c.CourseTitle + ' ' + c.Origin + ' ' + c.CourseDate.ToShamsi(),
                Value = c.CourseId.ToString()

            }).ToList();
        }

        public CourseViewModel GetCourses(int pageId = 1, string courseDate = "", string title = "")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(courseDate))
            {
                date = Convert.ToDateTime(courseDate).Date;
            }

            IEnumerable<InformationCourseViewModel> course = ShowCourse();

            if (!string.IsNullOrEmpty(courseDate))
            {
                course = course.Where(r => r.CourseDate.Date == date);
            }

            if (!string.IsNullOrEmpty(title))
            {
                course = course.Where(r => r.CourseTitle.Contains(title));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            CourseViewModel list = new CourseViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)course.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationCourseViewModels = course.OrderBy(u => u.CourseDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public CourseViewModel GetDeletedCourse(int pageId = 1, string courseDate = "",string title ="")
        {
            var date = DateTime.Now;

            if (!string.IsNullOrEmpty(courseDate))
            {
                date = Convert.ToDateTime(courseDate).Date;
            }

            IEnumerable<InformationCourseViewModel> course = ShowDeletedCourse();

            if (!string.IsNullOrEmpty(courseDate))
            {
                course = course.Where(r => r.CourseDate.Date == date);
            }

            if (!string.IsNullOrEmpty(title))
            {
                course = course.Where(r => r.CourseTitle.Contains(title));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            CourseViewModel list = new CourseViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)course.Count() / take);

            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;

            list.InformationCourseViewModels = course.OrderBy(u => u.CourseDate).Skip(skip).Take(take).ToList();

            return list;
        }

        public Course GetDataForEditCourse(int courseId)
        {
            return _context.Courses.Where(c => c.CourseId == courseId).Select(c => new Course()
            {
                CourseId = courseId,
                CourseTitle = c.CourseTitle,
                Origin = c.Origin,
                Description = c.Description,
                PercentAmount = c.PercentAmount,
                CourseDate = c.CourseDate
            }).Single();
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public string GetCourseTitleById(int courseId)
        {
            Course course = _context.Courses.Find(courseId);

            return course.CourseTitle + ' ' + course.CourseDate.ToShamsi();
        }

        #region Dapper


        public List<InformationCourseViewModel> ShowCourse()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationCourseViewModel>("ShowCourse", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public List<InformationCourseViewModel> ShowDeletedCourse()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<InformationCourseViewModel>("ShowDeletedCourse", commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }


        #endregion
    }
}
