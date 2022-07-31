using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class SettingsService : ISettingsService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;

        public SettingsService(KonkurCRMContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        #region StartDayOfMonth

        public void AddStartDayOfMonth(StartDayOfMonth startDayOfMonth)
        {
            _context.StartDayOfMonths.Add(startDayOfMonth);
            _context.SaveChanges();
        }

        public void UpdateStartDayOfMonth(StartDayOfMonth startDayOfMonth)
        {
            _context.StartDayOfMonths.Update(startDayOfMonth);
            _context.SaveChanges();
        }

        public int GetStartDayOfMonth()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@startDay", "", DbType.Int32, direction: ParameterDirection.Output);

                db.Query<StartDayOfMonth>("GetStartDayOfMonth", p, commandType: CommandType.StoredProcedure);

                var amount = p.Get<int>("@startDay");

                return amount;
            }
        }

        #endregion
    }
}
