using System;
using DevelopBase.Services;
using PurocumentLib.Model;
using System.Linq;
using System.Collections.Generic;
using Entity=PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using DevelopBase.Common;
namespace PurocumentLib.Service
{
    public class PurchasingplanService :ServiceBase ,IPurchasingplanService
    {
        public PurchasingplanService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void CreatePlan(PurchasingPlan plan)
        {
            if(plan.Details.Count(c=>c.PurchasingPlanCount<=0)>0)
            {
                throw new Exception("商品数量无效");
            }
            //创建主表
            var entity=new Entity.PurchasingPlan()
            {
                BizTypeID=plan.BizType,
                Status=plan.Status,
                CreateTime=plan.CreateTime,
                UpdateTime=plan.CreateTime,
                CreateUserID=plan.CreateUser,
                UpdateUserID=plan.CreateUser
            };
            var dbcontext= ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            var details=plan.Details.Select(s=>new Entity.PurchasingPlanDetail()
            {
                GoodsID=s.GoodsID,
                PurchasingCount=s.PurchasingPlanCount,
                PurchasingPlan=entity
            });
            dbcontext.AddRange(details);
            dbcontext.SaveChanges();

        }
    }
}
