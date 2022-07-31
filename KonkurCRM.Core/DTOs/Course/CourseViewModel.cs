using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.Core.DTOs.Course
{
    public class CourseViewModel
    {
        public List<DataLayer.Entities.Courses.Course> Courses { get; set; }
        public List<InformationCourseViewModel> InformationCourseViewModels { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }

    public class InformationCourseViewModel
    {
        public int CourseId { get; set; }
        public DateTime CourseDate { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string CourseTitle { get; set; }
        public int PercentAmount { get; set; }
    }
}
