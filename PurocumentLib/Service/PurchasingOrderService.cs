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
    public class PurchasingOrderService : ServiceBase, IPurchasingOrderService
    {
        public PurchasingOrderService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 供应商订单确认
        /// </summary>
        public void ComfirmOrder(int orderId, int userID, bool isPass, string Desc)
        {
            if (orderId == 0)
            {
                throw new ArgumentNullException();
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = (int)EnumPurchasingOrderState.VendorConfirmed;//isPass ? (int)PurchasingOrderStateEnum.VendorConfirmed : (int)PurchasingOrderStateEnum.Other;
            int auditType = (int)EnumPurchasingAuditType.VendorConfirmed;//isPass ? (int)AuditTypeEnum.VendorConfirmed : (int)AuditTypeEnum.Other;

            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && c.PurchasingOrderStatusID != (int)EnumPurchasingOrderState.AwaitVendorConfirm) > 0)
            {
                throw new Exception("订单状态不正确");
            }

            PurchasingOrder order = dbcontext.PurchasingOrder.Single<PurchasingOrder>(w => orderId.Equals(w.ID));
            order.PurchasingOrderStatusID = status;
            order.UpdateUserID = userID;
            order.UpdateTime = dateTimeNow;
            dbcontext.Update(order);

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dateTimeNow,
                Desc = Desc,
                Result = auditType
            };
            dbcontext.Add(pa);

            dbcontext.SaveChanges();
        }

        /// <summary>
        /// 供应商发货确认
        /// </summary>
        public void ComfirmDelivery(int orderId, int userID, bool isPass, string Desc)
        {

            if (orderId == 0)
            {
                throw new ArgumentNullException();
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = (int)EnumPurchasingOrderState.VendorShipped;//isPass ? (int)PurchasingOrderStateEnum.VendorShipped : (int)PurchasingOrderStateEnum.Other;
            int auditType = (int)EnumPurchasingAuditType.VendorShipped;//isPass ? (int)AuditTypeEnum.VendorShipped : (int)AuditTypeEnum.Other;

            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && c.PurchasingOrderStatusID != (int)EnumPurchasingOrderState.VendorConfirmed) > 0)
            {
                throw new Exception("订单状态不正确");
            }

            PurchasingOrder order = dbcontext.PurchasingOrder.Single<PurchasingOrder>(w => orderId.Equals(w.ID));
            order.PurchasingOrderStatusID = status;
            order.UpdateUserID = userID;
            order.UpdateTime = dateTimeNow;
            dbcontext.Update(order);

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dateTimeNow,
                Desc = Desc,
                Result = auditType
            };
            dbcontext.Add(pa);

            dbcontext.SaveChanges();
        }

        /// <summary>
        /// 部门收货确认
        /// </summary>
        /// <param name="po"></param>
        /// <param name="userID"></param>
        public void CheckInOrder(int orderId, int userID, List<Model.PurchasingOrderDetailModel> ListOrderDetailIDAndActualCount)
        {
            if (orderId == 0)
            {
                throw new ArgumentNullException();
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = (int)EnumPurchasingOrderState.DeparmentCheckIn;
            int auditType = (int)EnumPurchasingAuditType.DeparmentCheckIn;

            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && (c.PurchasingOrderStatusID.Equals((int)EnumPurchasingAuditType.VendorShipped) || c.PurchasingOrderStatusID.Equals((int)EnumPurchasingAuditType.DeparmentCheckIn))) > 0)
            {
                throw new Exception("订单状态不正确");
            }

            if (ListOrderDetailIDAndActualCount.Count < 1) //待更新的明细为0
            {
                return;
            }

            PurchasingOrder order = dbcontext.PurchasingOrder.Single<PurchasingOrder>(w => orderId.Equals(w.ID));
            order.PurchasingOrderStatusID = status;
            order.UpdateUserID = userID;
            order.UpdateTime = dateTimeNow;
            dbcontext.Update(order);

            var updatePPDIDs = ListOrderDetailIDAndActualCount.Select(s => s.ID);
            var details = dbcontext.PurchasingOrder.Include(r => r.Details).FirstOrDefault(en => en.ID == orderId);
            var pods = from f in ListOrderDetailIDAndActualCount
                       join d in details.Details
                       on f.ID equals d.ID
                       select d;
            foreach (var item in pods)
            {
                item.ActualCount = ListOrderDetailIDAndActualCount.FirstOrDefault(i => i.ID == item.ID).ActualCount;
                item.ActualSubtotal = item.Price * item.ActualCount;
                item.PurchasingOrderStateID = status;
                item.UpdateUsrID = userID;
                item.UpdateTime = dateTimeNow;
            }
            dbcontext.UpdateRange(pods);

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dateTimeNow,
                //Desc = Desc,
                Result = auditType
            };
            dbcontext.Add(pa);

            dbcontext.SaveChanges();
        }


        /// <summary>
        /// 部门全部收货完毕
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        public void FinishOrder(int orderId, int userID, bool isPass, string Desc)
        {
            if (orderId == 0)
            {
                throw new ArgumentNullException();
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = (int)EnumPurchasingOrderState.ConfirmReceipt;//isPass ? (int)PurchasingOrderStateEnum.ConfirmReceipt : (int)PurchasingOrderStateEnum.ConfirmReceipt;
            int auditType = (int)EnumPurchasingAuditType.VendorShipped;//isPass ? (int)AuditTypeEnum.VendorShipped : (int)AuditTypeEnum.Other;

            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && !c.PurchasingOrderStatusID.Equals((int)EnumPurchasingOrderState.DeparmentCheckIn)) > 0)
            {
                throw new Exception("订单状态不正确");
            }

            PurchasingOrder order = dbcontext.PurchasingOrder.FirstOrDefault(w => orderId.Equals(w.ID));
            order.PurchasingOrderStatusID = status;
            order.UpdateUserID = userID;
            order.UpdateTime = dateTimeNow;
            dbcontext.Update(order);

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dateTimeNow,
                Desc = Desc,
                Result = auditType
            };
            dbcontext.Add(pa);

            dbcontext.SaveChanges();
        }

    }
}
