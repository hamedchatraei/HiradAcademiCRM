using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.DataLayer.Entities.Advisers;
using KonkurCRM.DataLayer.Entities.Calls;
using KonkurCRM.DataLayer.Entities.Courses;
using KonkurCRM.DataLayer.Entities.Pays;
using KonkurCRM.DataLayer.Entities.Permission;
using KonkurCRM.DataLayer.Entities.Plans;
using KonkurCRM.DataLayer.Entities.Register;
using KonkurCRM.DataLayer.Entities.Salary;
using KonkurCRM.DataLayer.Entities.Settings;
using KonkurCRM.DataLayer.Entities.Students;
using KonkurCRM.DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace KonkurCRM.DataLayer.Context
{
    public class KonkurCRMContext : DbContext
    {
        public KonkurCRMContext(DbContextOptions<KonkurCRMContext> options) :base(options)
        {
            
        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Advisers

        public DbSet<Adviser> Advisers { get; set; }
        public DbSet<AdviserGroup> AdviserGroups { get; set; }
        public DbSet<GroupAdviser> GroupAdvisers { get; set; }
        public DbSet<AdviserInvoice> AdviserInvoices { get; set; }

        #endregion

        #region Calls

        public DbSet<Call> Calls { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<UnregisteredCalls> UnregisteredCalls { get; set; }
        public DbSet<UnregisteredFollowUp> UnregisteredFollowUps { get; set; }
        public DbSet<NotCalled> NotCalleds { get; set; }

        #endregion

        #region Courses

        public DbSet<Course> Courses { get; set; }

        #endregion

        #region Pays

        public DbSet<Pay> Pays { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<PayType> PayTypes { get; set; }
        public DbSet<Installment> Installments { get; set; }

        #endregion

        #region Plans

        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanAttribute> PlanAttributes { get; set; }
        public DbSet<AttributePlan> AttributePlans { get; set; }

        #endregion

        #region Register

        public DbSet<Register> Registers { get; set; }
        public DbSet<Register_sAdviser> RegisterSAdvisers { get; set; }

        #endregion

        #region Salary
        
        public DbSet<SalaryType> SalaryTypes { get; set; }

        #endregion

        #region Students

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Study> Studies { get; set; }

        #endregion

        #region Settings

        public DbSet<StartDayOfMonth> StartDayOfMonths { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            base.OnModelCreating(modelBuilder);
        }
    }
}
