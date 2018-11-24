using System;
using DevelopBase.Services;
using PurocumentLib.Model;
using System.Linq;
using System.Collections.Generic;
using Entity=PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using DevelopBase.Common;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Entity;

namespace PurocumentLib.Service
{
    public class PurchasingplanService :ServiceBase ,IPurchasingplanService
    {
        public PurchasingplanService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void CreatePlan(Model.PurchasingPlan plan)
        {
            if(plan.Details.Count(c=>c.PurchasingPlanCount<=0)>0)
            {
                throw new Exception("商品数量无效");
            }
            //创建主表
            var entity=new Entity.PurchasingPlan()
            {
                Desc=plan.Desc,
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

        public Model.PurchasingPlan Load(int id)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.PurchasingPlan.Include(i=>i.Details).SingleOrDefault(s=>s.ID==id);
            var details=entity.Details.Select(s=>new Model.PurchasingPlanDetail()
            {
                GoodsID=s.GoodsID,
                PurchasingPlanCount=s.PurchasingCount
            });
            var master=new Model.PurchasingPlan()
            {
                ID=entity.ID,
                DepartmentID=entity.DepartmentID,
                BizType=entity.BizTypeID,
                Desc=entity.Desc,
                Status=entity.Status,
                Details=details,
                CreateTime=entity.CreateTime,
                CreateUser=entity.CreateUserID,
                UpdateUser=entity.UpdateUserID,
                UpdateTime=entity.UpdateTime

            };
            return master;
        }
        //初审提交
        public void SubmitPlan(IEnumerable<int> ids,int userID,string desc)
        {
            if(ids==null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(dbcontext.PurchasingPlan.Count(c=>ids.Contains(c.ID) && c.Status!=1)>0)
            {
                throw new Exception("采购计划状态不正确");
            }
            var plans=dbcontext.PurchasingPlan.Where(w=>ids.Contains(w.ID)).ToList();
            foreach(var item in plans)
            {
                item.Status=2;
                //添加初审记录
                var submitRecord=new PurchasingAudit()
                {
                    PlanID=item.ID,
                    UserID=userID,
                    Desc=desc,
                    CreateTime=DateTime.Now
                };
                dbcontext.Add(submitRecord);
            }
            dbcontext.UpdateRange(plans);
            dbcontext.SaveChanges();
        }

        public void UpdatePlan(Model.PurchasingPlan plan)
        {
            if(plan==null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.PurchasingPlan.Include(i=>i.Details).SingleOrDefault(s=>s.ID==plan.ID);
            if(entity==null)
            {
                throw new Exception("采购计划不存在");
            }
            if(entity.Status!=1)
            {
                throw new Exception("采购计划不可修改");
            }
            entity.Desc=plan.Desc;
            entity.UpdateTime=DateTime.Now;
            entity.UpdateUserID=plan.UpdateUser;
            //修改商品信息
            var saveGoods=entity.Details.Select(s=>s.GoodsID).ToList();
            var submitGoods=plan.Details.Select(s=>s.GoodsID).ToList();
            //新增
            var addGoods =submitGoods.Where(w=>!saveGoods.Contains(w));
            var addDetails=plan.Details.Where(w=>addGoods.Contains(w.GoodsID)).Select(s=>new Entity.PurchasingPlanDetail()
            {
                PurchasingPlanID=entity.ID,
                GoodsID=s.GoodsID,
                PurchasingCount=s.PurchasingPlanCount
            });
            dbcontext.AddRange(addDetails);
            //删除
            var removeGoods=saveGoods.Where(w=>!submitGoods.Contains(w));
            var removeDetails=entity.Details.Where(w=>removeGoods.Contains(w.GoodsID));
            dbcontext.RemoveRange(removeDetails);
            //修改
            var updateGoods=submitGoods.Where(w=>saveGoods.Contains(w));
            var updateDetails=plan.Details.Where(w=>updateGoods.Contains(w.GoodsID));
            foreach(var goods in updateDetails)
            {
                var updated=entity.Details.SingleOrDefault(s=>s.GoodsID==goods.GoodsID);
                updated.PurchasingCount=goods.PurchasingPlanCount;
                dbcontext.Update(updated);
            }
            dbcontext.SaveChanges();
        }
    }
}
