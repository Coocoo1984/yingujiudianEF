using System;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Model;

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
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan = dbcontext.PurchasingPlan.Single(s => s.ID == planId);
            if (plan == null)
            {
                throw new Exception("采购计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)EnumPurchasingPlanState.PlanAudit1Pass : (int)EnumPurchasingPlanState.PlanAudit1Rejected;
            int auditType = isPass ? (int)EnumAuditType.PlanAudit1Pass : (int)EnumAuditType.PlanAudit1Rejected;

            //保存审核结果和修改计划状态
            plan.Status = status;
            plan.UpdateTime = dateTimeNow;
            plan.UpdateUserID = userID;

            dbcontext.Update(plan);

            var record = new PurchasingAudit()
            {
                PlanID = planId,
                Result = auditType,
                UserID = userID,
                CreateTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }

        //提交复审 生成与供应商的订单及订单明细
        public void ComfirmPlanAndSubmitOrder(int planId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan = dbcontext.PurchasingPlan.SingleOrDefault(s => s.ID == planId);
            if (plan == null)
            {
                throw new Exception("采购计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int planStatus = isPass ? (int)EnumPurchasingPlanState.PlanAudit2Pass : (int)EnumPurchasingPlanState.PlanAudit2Rejected;
            int planAuditType = isPass ? (int)EnumAuditType.PlanAudit2Pass : (int)EnumAuditType.PlanAudit2Rejected;

            int orderStatus = isPass ? (int)EnumPurchasingOrderState.AwaitVendorConfirm : (int)EnumPurchasingOrderState.AwaitVendorConfirm;
            //int orderAuditType = isPass ? (int)EnumAuditType.pl : (int)EnumAuditType.PlanAudit1Rejected;

            List<PurchasingOrder> insertListPOs = new List<PurchasingOrder>();
            List<PurchasingOrderDetail> insertListPODs = new List<PurchasingOrderDetail>();

            //保存审核结果和修改计划状态
            plan.Status = planStatus;
            plan.UpdateTime = dateTimeNow;
            plan.UpdateUserID = userID;
            dbcontext.Update(plan);

            PurchasingAudit insertPA = new PurchasingAudit
            {
                PlanID = plan.ID,
                UserID = userID,
                CreateTime = dateTimeNow,
                Result = planAuditType,//审核状态 若复审通过及订单生成 故仅生成一条
                Desc = Desc
            };
            dbcontext.Add(insertPA);

            ///下面这段代码写得乱 性能应该也不高 Linq to EF语法不熟悉 后面要改

            //采购计划的部门
            var entityD = dbcontext.Department.SingleOrDefault(s => s.ID == plan.DepartmentID);

            //采购计划明细
            var entityPP = dbcontext.PurchasingPlan.Include(i => i.Details).SingleOrDefault(s => s.ID == plan.ID);

            //按供应商分组,循环操作
            var vendorIDs = entityPP.Details.Select(s => s.VendorID).ToList();
            //按供应商的循环操作
            foreach (var vendorID in vendorIDs)
            {
                //按供应商的订单明细集合
                var verdorPPDs = entityPP.Details.Where(w => vendorIDs.Contains(w.VendorID));

                int itemCount = 0;
                decimal? total = 0;
                PurchasingOrder po = new PurchasingOrder
                {
                    Code = StrPOPrefix + DateTime.Now.ToString(StrPOSuffixFormat),//[2][17]
                    PurchasingPlanID = plan.ID,
                    PurchasingOrderStatusID = orderStatus,//订单状态
                    VendorID = vendorID.Value,
                    DepartmentID = plan.DepartmentID,
                    Tel = entityD?.Tel,
                    Addr = entityD?.Address,
                    BizTypeID = plan.BizTypeID,
                    CreateUsrID = userID,
                    CreateTime = dateTimeNow,
                    Total = total,
                    ItemCount = itemCount
                };
                foreach (var vendorPPD in verdorPPDs)
                {
                    //生成每个供应商分配的采购明细
                    PurchasingOrderDetail pod = new PurchasingOrderDetail
                    {
                        PurchasingOrder = po,
                        PurchasingOrderStateID = orderStatus,//订单状态
                        GoodsClassID = vendorPPD.GoodsClassID,
                        GoodsID = vendorPPD.GoodsID,
                        Count = vendorPPD.PurchasingCount,
                        Price = vendorPPD.Price,
                        Subtotal = vendorPPD.PurchasingCount * vendorPPD.Price,
                        ActualCount = 0,
                        ActualSubtotal = 0,
                        CreateUsrID = userID,
                        CreateTime = dateTimeNow,
                        UpdateUsrID = userID,
                        UpdateTime = dateTimeNow,
                        //PurchasiongOrderStateID = status,//冗余的字段
                        PurchasingPlanDetailID = vendorPPD.ID
                    };
                    insertListPODs.Add(pod); //订单明细

                    po.Total += pod.Subtotal;//累计每种商品的小计金额
                    po.ItemCount++;//明细数量

                    //更新采购计划、采购计划明细状态
                    vendorPPD.Status = planStatus;//采购状态

                    dbcontext.Update(vendorPPD);   /// 更新PPD

                }


                insertListPOs.Add(po);  // 订单

            }

            dbcontext.AddRange(insertListPOs);
            dbcontext.AddRange(insertListPODs);

            dbcontext.SaveChanges();
        }
    }
}
