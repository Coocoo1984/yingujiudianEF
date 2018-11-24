using System;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using System.Linq;
namespace PurocumentLib.Service
{
    public class PurchasingAuditService : ServiceBase, IPurchasingAuditService
    {
        public PurchasingAuditService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        //采购计划审核
        public void PlanAudit(int planId, int userID, bool isPass, string Desc)
        {
            int status=0;
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan=dbcontext.PurchasingPlan.Single(s=>s.ID==planId);
            if(plan==null)
            {
                throw new Exception("采购计划不存在");
            }
            var firstStatus=new int[]{2,4};
            if(firstStatus.Contains(plan.Status))
            {
                status=isPass?5:3;
            }
            var secondStatus=new int[]{6,8};
            if(secondStatus.Contains(plan.Status))
            {
                //复审
                status=isPass?9:7;                
            }
            //保存审核结果和修改计划状态
            plan.Status=status;
            dbcontext.Update(plan);
            var record=new PurchasingAudit()
            {
                PlanID=planId,
                Result=plan.Status,
                UserID=userID,
                CreateTime=DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }
    }
}
