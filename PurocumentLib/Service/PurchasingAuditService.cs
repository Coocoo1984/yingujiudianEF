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
            int auditType = isPass ? (int)EnumPurchasingAuditType.PlanAudit1Pass : (int)EnumPurchasingAuditType.PlanAudit1Rejected;

            //保存审核结果和修改计划状态
            plan.Status = status;
            plan.UpdateTime = dateTimeNow;
            plan.UpdateUserID = userID;

            int intDeparmentID = plan.DepartmentID;
            string strCode = plan.Code;
            string strDateTime = dateTimeNow.ToString(StrDateTimeFormat);
            string result = isPass ? "通过" : $"未通过:{Desc}";
            string title = string.Empty;
            string content = string.Empty;
            string toUsrID = string.Empty;

            dbcontext.Update(plan);

            var record = new PurchasingAudit()
            {
                PlanID = planId,
                Result = auditType,
                UserID = userID,
                Desc = Desc,
                CreateTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();


            Usr usr = dbcontext.Usr.SingleOrDefault(s => s.ID == userID);
            Department department = dbcontext.Department.SingleOrDefault(s => s.ID == intDeparmentID);

            if (isPass)
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购总监
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "待复审采购计划";
                content = $"编号:{strCode}&nbsp部门:{department.Name}";
                MessageService.Post(
                    toUsrID,
                    title,
                    strDateTime,
                    content
                );
            }
            else
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购主管
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "采购计划复审驳回";
                content = $"编号:{strCode}&nbsp复审结果:{result}";

            }

        }

        //采购计划审核（复审）
        public void PlanAudit2(int planId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan = dbcontext.PurchasingPlan.Single(s => s.ID == planId);
            if (plan == null)
            {
                throw new Exception("采购计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)EnumPurchasingPlanState.PlanAudit2Pass : (int)EnumPurchasingPlanState.PlanAudit2Rejected;
            int auditType = isPass ? (int)EnumPurchasingAuditType.PlanAudit2Pass : (int)EnumPurchasingAuditType.PlanAudit2Rejected;

            //保存审核结果和修改计划状态
            plan.Status = status;
            plan.UpdateTime = dateTimeNow;
            plan.UpdateUserID = userID;

            int intDeparmentID = plan.DepartmentID;
            string strCode = plan.Code;
            string strDateTime = dateTimeNow.ToString(StrDateTimeFormat);
            string result = isPass ? "通过" : $"未通过:{Desc}";
            string title = string.Empty;
            string content = string.Empty;
            string toUsrID = string.Empty;

            dbcontext.Update(plan);

            var record = new PurchasingAudit()
            {
                PlanID = planId,
                Result = auditType,
                UserID = userID,
                Desc = Desc,
                CreateTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();


            Usr usr = dbcontext.Usr.SingleOrDefault(s => s.ID == userID);
            Department department = dbcontext.Department.SingleOrDefault(s => s.ID == intDeparmentID);
            
            if (isPass)
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购总监
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "待三审采购计划";
                content = $"编号:{strCode}&nbsp部门:{department?.Name}";

            }
            else
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购主管
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "采购计划复审驳回";
                content = $"编号:{strCode}&nbsp复审结果:{result}";

            }
            MessageService.Post(
                toUsrID,
                title,
                strDateTime,
                content
            );
        }


        //提交三审 生成与供应商的订单及订单明细
        public void ComfirmPlanAndSubmitOrder(int planId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var plan = dbcontext.PurchasingPlan.SingleOrDefault(s => s.ID == planId);
            if (plan == null)
            {
                throw new Exception("采购计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int planStatus = isPass ? (int)EnumPurchasingPlanState.PlanAudit3Pass : (int)EnumPurchasingPlanState.PlanAudit3Rejected;
            int planAuditType = isPass ? (int)EnumPurchasingAuditType.PlanAudit3Pass : (int)EnumPurchasingAuditType.PlanAudit3Rejected;

            int orderStatus = isPass ? (int)EnumPurchasingOrderState.AwaitVendorConfirm : (int)EnumPurchasingOrderState.AwaitVendorConfirm;


            //保存审核结果和修改计划状态
            plan.Status = planStatus;
            plan.UpdateTime = dateTimeNow;
            plan.UpdateUserID = userID;
            dbcontext.Update(plan);///

            PurchasingAudit insertPA = new PurchasingAudit
            {
                PlanID = plan.ID,
                UserID = userID,
                CreateTime = dateTimeNow,
                Result = planAuditType,//审核状态 若复审通过及订单生成 故仅生成一条
                Desc = Desc
            };
            dbcontext.Add(insertPA);///

            ///下面这段代码写得乱 性能应该也不高 Linq to EF语法不熟悉 后面要改

            if (isPass)  //审核通过
            {
                List<PurchasingOrder> insertListPOs = new List<PurchasingOrder>();
                List<PurchasingOrderDetail> insertListPODs = new List<PurchasingOrderDetail>();

                //采购计划的部门
                var entityD = dbcontext.Department.SingleOrDefault(s => s.ID == plan.DepartmentID);

                //采购计划明细
                var entityPP = dbcontext.PurchasingPlan.Include(i => i.Details).SingleOrDefault(s => s.ID == plan.ID);

                //按供应商分组,循环操作
                var vendorIDs = entityPP.Details.Where(s => s.VendorID.HasValue).Select(s => s.VendorID).Distinct().ToList();//之前未去重，导致严重逻辑错误
                //特殊情况下供应商尚未报价
                if (vendorIDs == null || vendorIDs.Count == 0)
                {
                    throw new Exception("不存在或未选定供应商");
                }

                //按供应商的循环操作
                foreach (var vendorID in vendorIDs)
                {
                    //按供应商的订单明细集合
                    var verdorPPDs = entityPP.Details.Where(w => vendorIDs.Contains(w.VendorID));

                    int itemCount = 0;
                    decimal? total = 0;
                    //一个供应商生成一个订单（因采购计划能为一种业务类型 要么食材 要么办公用品 故这里不按照业务类型再做拆分 数据库设计其实是支持的）
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
                        UpdateUserID = userID,
                        UpdateTime = dateTimeNow,
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
                            Subtotal = vendorPPD.PurchasingCount * vendorPPD.Price,//这里之前没有结果
                            ActualCount = 0,
                            ActualSubtotal = 0,
                            CreateUsrID = userID,
                            CreateTime = dateTimeNow,
                            UpdateUsrID = userID,
                            UpdateTime = dateTimeNow,
                            PurchasingPlanDetailID = vendorPPD.ID
                        };
                        insertListPODs.Add(pod); //订单明细

                        po.Total += pod.Subtotal;//累计每种商品的小计金额
                        po.ItemCount++;//明细数量

                        //更新采购计划、采购计划明细状态
                        vendorPPD.Status = planStatus;//采购状态

                        dbcontext.Update(vendorPPD);   /// 更新PPD

                    }

                    insertListPOs.Add(po);///  // 订单

                }
                dbcontext.AddRange(insertListPOs);
                dbcontext.AddRange(insertListPODs);
            }

            dbcontext.SaveChanges();
        }
    }
}
