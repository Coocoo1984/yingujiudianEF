using System;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PurocumentLib.Service
{
    public class PurchasingAuditService : ServiceBase, IPurchasingAuditService
    {
        public PurchasingAuditService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        //采购计划审核（初审）
        public void PlanAudit(int planId, int userID, bool isPass, string Desc)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan=dbcontext.PurchasingPlan.Single(s=>s.ID==planId);
            if(plan==null)
            {
                throw new Exception("采购计划不存在");
            }

            int status = 0;
            int auditStatus = 0;
            var firstStatus=new int[]{2,4};
            if(firstStatus.Contains(plan.Status))
            {
                status = isPass ? 5 : 3;
                auditStatus = isPass ? 2 : 1;
            }
            ////var secondStatus=new int[]{6,8};
            ////if(secondStatus.Contains(plan.Status))
            ////{
            ////    //复审
            ////    status = isPass ? 9 : 7;
            ///     auditStatus = isPass ? 4 : 3;
            ////}
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

        //提交复审 生成与供应商的订单及订单明细
        public void ComfirmPlanAndSubmitOrder(int planId, int userID, bool isPass, string Desc)
        {


            var dc = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan = dc.PurchasingPlan.Single(s => s.ID == planId);


            ///下面这段代码写得乱 性能应该也不高 EF语法糖不熟悉 后面要改

            if (plan == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            //采购计划
            var entityPP = dbcontext.PurchasingPlan.Include(i => i.Details).SingleOrDefault(s => s.ID == plan.ID);


            if (entityPP == null)
            {
                throw new Exception("采购计划不存在");
            }

            int status = 0;
            int auditStatus = 0;
            var secondStatus = new int[] { 6, 8 };
            if (secondStatus.Contains(entityPP.Status))
            {
                status = isPass ? 9 : 7;
                auditStatus = isPass ? 2 : 1;
            }
            plan.Status = status;
            dbcontext.Update(plan);     //更新采购计划状态


            DateTime dateTimeNow = DateTime.Now;

            //采购计划的部门
            var entityD = dbcontext.Department.SingleOrDefault(s => s.ID == plan.DepartmentID);

            //按供应商分组,循环操作
            var vendorIDs = entityPP.Details.Select(s => s.VendorID).Distinct().ToList();

            foreach (var vendorID in vendorIDs)
            {
                //按供应商的订单明细集合
                var verdorPPDs = entityPP.Details.Where(w => vendorIDs.Contains(w.VendorID));

                int itemCount = 0;
                decimal? total = 0;

                foreach (var vendorPPD in verdorPPDs)
                {
                    //生成每个供应商分配的采购明细
                    PurchasingOrderDetail pod = new PurchasingOrderDetail
                    {
                        //PurchasingOrder = po,
                        PurchasingOrderStateID = 1,//写死
                        GoodsClassID = vendorPPD.GoodsClassID,
                        GoodsID = vendorPPD.GoodsID,
                        Count = vendorPPD.PurchasingCount,
                        Price = vendorPPD.Price,
                        Subtotal = vendorPPD.PurchasingCount * vendorPPD.Price,
                        ActualCount = 0,
                        ActualSubtotal = 0,
                        //CreateUsrID =  //没得用户
                        CreateTime = dateTimeNow,
                        //PurchasiongOrderStateID = secondStatus,//冗余的字段
                        PurchasingPlanDetailID = vendorPPD.ID
                    };
                    dbcontext.Add(pod);    /// 生成订单明细

                    total += pod.Subtotal;//累计每种商品的小计金额
                    itemCount++;//明细数量

                    //更新采购计划、采购计划明细状态
                    vendorPPD.Status = 9;//复审通过

                    dbcontext.Update(vendorPPD);   /// 更新PPD

                }

                PurchasingOrder po = new PurchasingOrder
                {
                    Code = $"PO{DateTime.Now.ToString("yyyyMMddHHmmssfff")}",//[2][17]
                    PurchasingPlanID = plan.ID,
                    PurchasingOrderStatusID = 1,//写死
                    VendorID = vendorID.Value,
                    DepartmentID = plan.DepartmentID,
                    Tel = entityD.Tel,
                    Addr = entityD.Address,
                    BizTypeID = plan.BizTypeID,
                    //CreateUsrID =  //没得用户
                    CreateTime = dateTimeNow,
                    Total = total,
                    ItemCount = itemCount
                };
                dbcontext.Add(po);  // 生成PO

            }

            PurchasingAudit insertPA = new PurchasingAudit
            {
                PlanID = plan.ID,
                //UserID = //没得用户
                CreateTime = dateTimeNow,
                Result = auditStatus,
                //Desc = //没得
            };
            dbcontext.Add(insertPA);    //插入审核表

            dbcontext.SaveChanges();
        }
    }
}
