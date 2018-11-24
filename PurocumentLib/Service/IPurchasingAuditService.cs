using System;

namespace PurocumentLib.Service
{
    public interface IPurchasingAuditService
    {
        //采购计划审核
        void PlanAudit(int planId,int userID,string Desc);
    }
}
