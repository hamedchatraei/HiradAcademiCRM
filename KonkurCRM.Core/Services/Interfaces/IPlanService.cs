using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonkurCRM.Core.DTOs.Plan;
using KonkurCRM.DataLayer.Entities.Plans;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KonkurCRM.Core.Services.Interfaces
{
    public interface IPlanService
    {
        #region Plan

        int AddPlan(Plan plan);
        void EditPlan(EditPlanViewModel editPlan);
        void UpdatePlan(Plan plan);
        void DeletePlan(int planId);
        PlanViewModel GetPlans(int pageId = 1, string planTitle = "");
        PlanViewModel GetDeletedPlans(int pageId = 1, string planTitle = "");
        EditPlanViewModel GetDataForEditPlan(int planId);
        Plan GetPlanById(int planId);
        List<SelectListItem> GetPlanListItem();
        string GetPlanTitleById(int planId);

        #endregion

        #region PlanAttribute

        int AddPlanAttribute(PlanAttribute planAttribute);
        void EditPlanAttribute(PlanAttribute planAttribute);
        void UpdatePlanAttribute(PlanAttribute planAttribute);
        void DeletePlanAttribute(int planAttributeId);
        PlanAttributeViewModel GetPlanAttributes(int pageId=1);
        PlanAttribute GetPlanAttributeById(int planAttributeId);
        void AddAttributeToPlan(List<int> planAttributeIds,int planId);
        void UpdateAttributesPlan(List<int> planAttributeIds,int planId);
        List<int> GetAttributesPlans(int planId);
        List<AttributePlan> GetAttributePlansByPlanAttributeId(int planAttributeId);
        void DeleteAttributePlanByPlanAttributeId(int planAttributeId);

        #endregion
    }
}
