using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KonkurCRM.Core.DTOs.Plan;
using KonkurCRM.Core.DTOs.Register;
using KonkurCRM.Core.Services.Interfaces;
using KonkurCRM.DataLayer.Context;
using KonkurCRM.DataLayer.Entities.Plans;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KonkurCRM.Core.Services.Services
{
    public class PlanService: IPlanService
    {
        private KonkurCRMContext _context;
        private IConfiguration _configuration;

        public PlanService(KonkurCRMContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #region Plan

        public int AddPlan(Plan plan)
        {
            _context.Plans.Add(plan);
            _context.SaveChanges();
            return plan.PlanId;
        }

        public void EditPlan(EditPlanViewModel editPlan)
        {
            Plan plan = GetPlanById(editPlan.PlanId);

            plan.PlanId = editPlan.PlanId;
            plan.PlanTitle = editPlan.PlanTitle;
            plan.Length = editPlan.Length;
            plan.Amount = editPlan.Amount;

            UpdatePlan(plan);
        }

        public void UpdatePlan(Plan plan)
        {
            _context.Plans.Update(plan);
            _context.SaveChanges();
        }

        public void DeletePlan(int planId)
        {
            Plan plan = GetPlanById(planId);
            plan.IsDelete = true;
            UpdatePlan(plan);
        }

        public PlanViewModel GetPlans(int pageId = 1, string planTitle = "")
        {
            IQueryable<Plan> plans = _context.Plans.Where(p => p.IsDelete == false);

            if (!string.IsNullOrEmpty(planTitle))
            {
                plans = plans.Where(a => a.PlanTitle.Contains(planTitle));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            PlanViewModel list = new PlanViewModel();
            list.CurrentPage = pageId;
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.PageCount = (int)Math.Ceiling((decimal)plans.Count() / take);
            list.Plans = plans.OrderBy(u => u.PlanTitle).Skip(skip).Take(take).ToList();

            return list;
        }

        public PlanViewModel GetDeletedPlans(int pageId = 1, string planTitle = "")
        {
            IQueryable<Plan> plans = _context.Plans.Where(p => p.IsDelete);

            if (!string.IsNullOrEmpty(planTitle))
            {
                plans = plans.Where(a => a.PlanTitle.Contains(planTitle));
            }

            int take = 10;
            int skip = (pageId - 1) * take;

            PlanViewModel list = new PlanViewModel();
            list.CurrentPage = pageId;
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.PageCount = (int)Math.Ceiling((decimal)plans.Count() / take);
            list.Plans = plans.OrderBy(u => u.PlanTitle).Skip(skip).Take(take).ToList();

            return list;
        }

        public EditPlanViewModel GetDataForEditPlan(int planId)
        {
            return _context.Plans.Where(p => p.PlanId == planId).Select(p => new EditPlanViewModel()
            {
                PlanId = planId,
                PlanTitle = p.PlanTitle,
                Length = p.Length,
                Amount = p.Amount,
                AttributePlans = p.AttributePlans.Select(a => a.PlanAttributeId).ToList()
            }).Single();
        }

        public Plan GetPlanById(int planId)
        {
            return _context.Plans.Find(planId);
        }

        public List<SelectListItem> GetPlanListItem()
        {
            return _context.Plans.Where(u => u.IsDelete == false).Select(p => new SelectListItem()
            {
                Text =p.PlanTitle ,
                Value = p.PlanId.ToString()

            }).ToList();
        }

        public string GetPlanTitleById(int planId)
        {
            Plan plan = GetPlanById(planId);

            return plan.PlanTitle;
        }

        #endregion

        #region Attribute

        public int AddPlanAttribute(PlanAttribute planAttribute)
        {
            _context.PlanAttributes.Add(planAttribute);
            _context.SaveChanges();
            return planAttribute.PlanAttributeId;
        }

        public void EditPlanAttribute(PlanAttribute planAttribute)
        {
            PlanAttribute editPlanAttribute = GetPlanAttributeById(planAttribute.PlanAttributeId);

            editPlanAttribute.PlanAttributeId = planAttribute.PlanAttributeId;
            editPlanAttribute.Title = planAttribute.Title;

            UpdatePlanAttribute(editPlanAttribute);
        }

        public void UpdatePlanAttribute(PlanAttribute planAttribute)
        {
            _context.PlanAttributes.Update(planAttribute);
            _context.SaveChanges();
        }

        public void DeletePlanAttribute(int planAttributeId)
        {
            PlanAttribute planAttribute = GetPlanAttributeById(planAttributeId);
            _context.PlanAttributes.Remove(planAttribute);
            _context.SaveChanges();
        }

        public PlanAttributeViewModel GetPlanAttributes(int pageId = 1)
        {
            IQueryable<PlanAttribute> planAttributes= _context.PlanAttributes;

            int take = 10;
            int skip = (pageId - 1) * take;

            PlanAttributeViewModel list = new PlanAttributeViewModel();
            list.CurrentPage = pageId;
            list.PageCount = (int)Math.Ceiling((decimal)planAttributes.Count() / take);
            list.StartPage = (pageId - 2 <= 0) ? 1 : pageId - 2;
            list.EndPage = (pageId + 9 > list.PageCount) ? list.PageCount : pageId + 9;
            list.PlanAttributes = planAttributes.OrderBy(u => u.Title).Skip(skip).Take(take).ToList();

            return list;
        }

        public PlanAttribute GetPlanAttributeById(int planAttributeId)
        {
            return _context.PlanAttributes.Find(planAttributeId);
        }

        public void AddAttributeToPlan(List<int> planAttributeIds, int planId)
        {
            foreach (int planAttributeId in planAttributeIds)
            {
                _context.AttributePlans.Add(new AttributePlan()
                {
                    PlanAttributeId = planAttributeId,
                    PlanId = planId
                });
            }

            _context.SaveChanges();
        }

        public void UpdateAttributesPlan(List<int> planAttributeIds, int planId)
        {
            //Delete All Attribute's Plan
            _context.AttributePlans.Where(a=>a.PlanId==planId).ToList().ForEach(a=>_context.AttributePlans.Remove(a));

            //Add New Attribute To Plan
            AddAttributeToPlan(planAttributeIds,planId);
        }

        public List<int> GetAttributesPlans(int planId)
        {
            return _context.AttributePlans
                .Where(a => a.PlanId == planId)
                .Select(a => a.PlanAttributeId).ToList();
        }

        public List<AttributePlan> GetAttributePlansByPlanAttributeId(int planAttributeId)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("HiradCRMConnection")))
            {
                var Show = db.Query<AttributePlan>("GetAttributePlansByPlanAttributeId", new { planAttributeId = planAttributeId }, commandType: CommandType.StoredProcedure).ToList();

                return Show;
            }
        }

        public void DeleteAttributePlanByPlanAttributeId(int planAttributeId)
        {
            List<AttributePlan> attributePlans = GetAttributePlansByPlanAttributeId(planAttributeId);

            foreach (var attributePlan in attributePlans)
            {
                _context.AttributePlans.Remove(attributePlan);
                _context.SaveChanges();
            }
        }

        #endregion
    }
}
