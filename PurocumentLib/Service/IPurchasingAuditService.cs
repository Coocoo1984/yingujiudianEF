using System;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IPurchasingAuditService:IService
    {
        //采购计划审核
        void PlanAudit(int planId,int userID,bool isPass,string Desc);
    }
}
