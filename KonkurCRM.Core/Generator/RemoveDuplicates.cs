using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Adviser;
using KonkurCRM.Core.DTOs.Student;

namespace KonkurCRM.Core.Generator
{
    public static class RemoveDuplicates
    {
        public static List<InformationAdviserViewModel> RemoveDuplicateds<InformationAdviserViewModel>(List<InformationAdviserViewModel> items, IEqualityComparer<InformationAdviserViewModel> comparer)
        {
            return (from s in items select s).Distinct(comparer).ToList();
        }

        public static List<InformationAdviserViewModel> RemoveDuplicateAdvisers(List<InformationAdviserViewModel> student)
        {
            student = RemoveDuplicateds(student, new AdviserComparer());

            return student;
        }

        public static List<InformationStudentViewModel> RemoveStudentDuplicateds<InformationStudentViewModel>(List<InformationStudentViewModel> items, IEqualityComparer<InformationStudentViewModel> comparer)
        {
            return (from s in items select s).Distinct(comparer).ToList();
        }

        public static List<InformationStudentViewModel> RemoveDuplicateStudents(List<InformationStudentViewModel> student)
        {
            student = RemoveDuplicateds(student, new StudentComparer());

            return student;
        }
    }
}
