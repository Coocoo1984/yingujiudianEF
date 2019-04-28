using System;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IPurchasingAuditService:IService
    {
        //采购计划审核
        void PlanAudit(int planId,int userID,bool isPass,string Desc);
        //采购计划审核
        void PlanAudit2(int planId, int userID, bool isPass, string Desc);
        //采购计划复审
        void ComfirmPlanAndSubmitOrder(int planId, int userID, bool isPass, string Desc);
    }
}
