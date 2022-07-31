using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkurCRM.Core.Generator
{
    public class GenerateDate
    {
        public DateTime GetStartDayOfMonth(int startDay)
        {
            DateTime today = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int yearPersian = persianCalendar.GetYear(today);
            int monthPersian = persianCalendar.GetMonth(today);
            

            int currentDay = GetCurrentDay();

            if (currentDay < startDay)
            {
                monthPersian -= 1;
            }

            DateTime dayOfMonth = persianCalendar.ToDateTime(yearPersian, monthPersian, startDay, 0, 0, 0, 0);

            return dayOfMonth;
        }

        public DateTime GetStartDayOfLastMonth(int startDay)
        {
            DateTime today = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int yearPersian = persianCalendar.GetYear(today);
            int monthPersian = persianCalendar.GetMonth(today)-1;


            int currentDay = GetCurrentDay();

            if (currentDay < startDay)
            {
                monthPersian -= 1;
            }

            DateTime dayOfMonth = persianCalendar.ToDateTime(yearPersian, monthPersian, startDay, 0, 0, 0, 0);

            return dayOfMonth;
        }

        public int GetCurrentDay()
        {
            DateTime today = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int yearPersian = persianCalendar.GetYear(today);
            int monthPersian = persianCalendar.GetMonth(today);
            int dayPersian = persianCalendar.GetDayOfMonth(today);

            return dayPersian;
        }

        public DateTime GetEndDateOfMonth(DateTime fromDate)
        {
            DateTime toDate = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int yearPersian = persianCalendar.GetYear(fromDate);
            int monthPersian = persianCalendar.GetMonth(fromDate);

            if (monthPersian >= 1 && monthPersian <= 6)
            {
                toDate = fromDate.AddDays(30);
            }
            else if (monthPersian >= 7 && monthPersian <= 11)
            {
                toDate = fromDate.AddDays(29);
            }
            else if (monthPersian == 12)
            {
                if (yearPersian == 1403 || yearPersian == 1408 || yearPersian == 1412)
                {
                    toDate = fromDate.AddDays(29);
                }
                else
                {
                    toDate = fromDate.AddDays(28);
                }
            }

            return toDate;
        }
    }
}
