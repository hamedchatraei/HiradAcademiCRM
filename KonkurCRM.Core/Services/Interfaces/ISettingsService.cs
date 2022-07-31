using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Settings;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface ISettingsService
    {
        #region StartDayOfMonth

        void AddStartDayOfMonth(StartDayOfMonth startDayOfMonth);
        void UpdateStartDayOfMonth(StartDayOfMonth startDayOfMonth);
        int GetStartDayOfMonth();

        #endregion
    }
}
