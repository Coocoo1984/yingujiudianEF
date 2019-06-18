using System;
using DevelopBase.Services;
using PurocumentLib.Model;
using System.Linq;
using System.Collections.Generic;
using Entity = PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using DevelopBase.Common;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Entity;

namespace PurocumentLib.Service
{
    public class ChargeBackService : ServiceBase, IChargeBackService
    {
        public ChargeBackService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(ChargeBackModel chargeBack)
        {
            if (chargeBack.Details.Count(c => c.Count <= 0) > 0)
            {
                throw new Exception("退货商品数量无效");
            }

            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var order = dbcontext.PurchasingOrder.Include(i=>i.Details).SingleOrDefault(s => s.ID.Equals(chargeBack.PurchasingOrderID));
            if (order == null || order.ID <1)
            {
                throw new Exception("订单不存在");
            }

            decimal total;
            var values = from d in chargeBack.Details select d;
            total = values.Sum(d => d.Count * d.Price);

            //创建主表
            var entity = new Entity.ChargeBack()
            {
                Code = StrCBPrefix + DateTime.Now.ToString(StrCBSuffixFormat),
                PurchasingOrderStatusID = (int)EnumPurchasingOrderState.DepartmentChargeBack,
                CreateTime = chargeBack.CreateTime,
                UpdateTime = chargeBack.CreateTime,
                CreateUsrID = chargeBack.CreateUsrID,
                UpdateUserID = chargeBack.CreateUsrID,
                ItemCount = chargeBack.Details.Count(),
                Total = total
            };


            dbcontext.Add(entity);
            var details = from a in chargeBack.Details
                          join b in order.Details on a.PurchasingOrderDetailID equals b.ID
                          select new Entity.ChargeBackDetail()
                          {
                              PurchasingOrderDetailID = a.PurchasingOrderDetailID,
                              Count = a.Count,
                              Price = b.Price,
                              Subtotal = a.Count * b.Price,
                              ChargeBack = entity
                          };
            dbcontext.AddRange(details);

            //更新订单状态
            var Order = dbcontext.PurchasingOrder.SingleOrDefault(s => s.ID.Equals(chargeBack.PurchasingOrderID));
            Order.PurchasingOrderStatusID = (int)EnumPurchasingOrderState.DepartmentChargeBack;
            Order.UpdateUserID = chargeBack.CreateUsrID;
            Order.UpdateTime = chargeBack.CreateTime;
            dbcontext.Update(Order);

            dbcontext.SaveChanges();
        }

        public void Audit(int chargeBackId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var cb = dbcontext.ChargeBack.Include(i=>i.PurchasingOrder).Single(s => s.ID == chargeBackId);
            if (cb == null)
            {
                throw new Exception("退货计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)EnumPurchasingOrderState.ChargeBackAudit : (int)EnumPurchasingOrderState.ChargeBackAuditRejected;
            int auditType = isPass ? (int)EnumPurchasingAuditType.ChargeBackAudit : (int)EnumPurchasingAuditType.ChargeBackAuditRejected;

            //保存审核结果和修改计划状态
            cb.PurchasingOrderStatusID = cb.PurchasingOrder.PurchasingOrderStatusID = status;
            cb.UpdateTime = dateTimeNow;
            cb.UpdateUserID = userID;

            dbcontext.Update(cb);

            var record = new PurchasingAudit()
            {
                PlanID = cb.PurchasingOrder.PurchasingPlanID,//退货 关联 订单的采购计划ID
                Result = auditType,
                UserID = userID,
                Desc = Desc,
                CreateTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }

        public void Finish(int chargeBackId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var cb = dbcontext.ChargeBack.Include(i => i.PurchasingOrder).Single(s => s.ID == chargeBackId);
            if (cb == null)
            {
                throw new Exception("退货计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)EnumPurchasingOrderState.ChargeBackFinish : (int)EnumPurchasingOrderState.ChargeBackFinish;
            int auditType = isPass ? (int)EnumPurchasingAuditType.ChargeBackFinish : (int)EnumPurchasingAuditType.ChargeBackFinish;

            //保存审核结果和修改计划状态
            cb.PurchasingOrderStatusID = cb.PurchasingOrder.PurchasingOrderStatusID = status;
            cb.UpdateTime = dateTimeNow;
            cb.UpdateUserID = userID;
            
            dbcontext.Update(cb);

            var record = new PurchasingAudit()
            {
                PlanID = cb.PurchasingOrder.PurchasingPlanID,//退货 关联 订单的采购计划ID
                Result = auditType,
                UserID = userID,
                Desc = Desc,
                CreateTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }

        public ChargeBackModel Load(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ChargeBackDetailModel chargeBackDetail)
        {
            throw new NotImplementedException();
        }

        public void VendorComfirm(int chargeBackId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var cb = dbcontext.ChargeBack.Include(i => i.PurchasingOrder).Single(s => s.ID == chargeBackId);
            if (cb == null)
            {
                throw new Exception("退货计划不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)EnumPurchasingOrderState.VendorChargeBackComfirm : (int)EnumPurchasingOrderState.VendorChargeBackComfirm;
            int auditType = isPass ? (int)EnumPurchasingAuditType.VendorChargeBackComfirm : (int)EnumPurchasingAuditType.VendorChargeBackComfirm;

            //保存审核结果和修改计划状态
            cb.PurchasingOrderStatusID = cb.PurchasingOrder.PurchasingOrderStatusID = status;
            cb.UpdateTime = dateTimeNow;
            cb.UpdateUserID = userID;

            dbcontext.Update(cb);

            var record = new PurchasingAudit()
            {
                PlanID = cb.PurchasingOrder.PurchasingPlanID,//退货 关联 订单的采购计划ID
                Result = auditType,
                UserID = userID,
                Desc = Desc,
                CreateTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }
    }
}
