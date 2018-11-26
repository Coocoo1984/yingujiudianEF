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
        /// 供应商发货确认
        /// </summary>

        public void ComfirmDelivery(int orderId, int userID, bool isPass, string Desc)
        {

            if (orderId == 0)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && c.PurchasingOrderStatusID != 2) > 0)
            {
                throw new Exception("订单状态不正确");
            }
            PurchasingOrder order = dbcontext.PurchasingOrder.Single<PurchasingOrder>(w => orderId.Equals(w.ID));

            int status = 0;
            int auditStatus = 0;
            var statusID = new int[] { 2 };
            if (statusID.Contains(order.PurchasingOrderStatusID))
            {
                status = isPass ? 2 : 1;//待调整
                auditStatus = isPass ? 8 : 7;
            }

            DateTime dtNow = DateTime.Now;

            order.PurchasingOrderStatusID = status;  // 2 -> 3
            order.UpdateUserID = userID;
            order.UpdateTime = dtNow;

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dtNow,
                Result = auditStatus  // audit_type = 7 供应商确认发货
            };

            dbcontext.Update(order);
            dbcontext.Add(pa);
            dbcontext.SaveChanges();
        }

        /// <summary>
        /// 供应商确认订单
        /// </summary>
        public void ComfirmOrder(int orderId, int userID, bool isPass, string Desc)
        {
            if (orderId == 0)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && c.PurchasingOrderStatusID != 1) > 0)
            {
                throw new Exception("订单状态不正确");
            }
            PurchasingOrder order = dbcontext.PurchasingOrder.Single<PurchasingOrder>(w => orderId.Equals(w.ID));

            int status = 0;
            int auditStatus = 0;
            var statusID = new int[] { 1 };
            if (statusID.Contains(order.PurchasingOrderStatusID))
            {
                status = isPass ? 2 : 1;//待调整
                auditStatus = isPass ? 6 : 5;
            }

            DateTime dtNow = DateTime.Now;

            order.PurchasingOrderStatusID = status;  // 1 -> 2
            order.UpdateUserID = userID;
            order.UpdateTime = dtNow;

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dtNow,
                Result = auditStatus  // audit_type = 8 供应商确认订单
            };

            dbcontext.Update(order);
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
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && (c.PurchasingOrderStatusID == 3 || c.PurchasingOrderStatusID == 4)) > 0)
            {
                throw new Exception("订单状态不正确");
            }

            if (ListOrderDetailIDAndActualCount.Count < 1) //待更新的明细为0
            {
                return;
            }

            PurchasingOrder order = dbcontext.PurchasingOrder.Single<PurchasingOrder>(w => orderId.Equals(w.ID));

            DateTime dtNow = DateTime.Now;
            var updatePPDIDs = ListOrderDetailIDAndActualCount.Select(s => s.ID);

            var details = dbcontext.PurchasingOrder.Include(r => r.Details); 

            var pods = from f in ListOrderDetailIDAndActualCount
                       join d in details
                       on f.ID equals d.ID
                       select new PurchasingOrderDetailModel();
            foreach(var item in pods)
            {
                item.ActualCount = item.ActualCount;
                item.ActualSubtotal = item.Price * item.ActualCount;
                item.PurchasingOrderStateID = 4; //收货中(每次可能是部分货 也可能是全部)
                item.UpdateUsrID = userID;
                item.UpdateTime = dtNow;
            }
            dbcontext.UpdateRange(pods);


            order.PurchasingOrderStatusID = 4;  // 4 收货中
            order.UpdateUserID = userID;
            order.UpdateTime = dtNow;

            dbcontext.Update(order);

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
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => orderId.Equals(c.ID) && c.PurchasingOrderStatusID != 4) > 0)
            {
                throw new Exception("订单状态不正确");
            }
            PurchasingOrder order = dbcontext.PurchasingOrder.FirstOrDefault(w => orderId.Equals(w.ID));
            int status = 0;
            int auditStatus = 0;
            var statusID = new int[] { 4 };
            if (statusID.Contains(order.PurchasingOrderStatusID))
            {
                status = isPass ? 2 : 1;//待调整
                auditStatus = isPass ? 8 : 7;
            }
            
            DateTime dtNow = DateTime.Now;

            order.PurchasingOrderStatusID = status; 
            order.UpdateUserID = userID;
            order.UpdateTime = dtNow;

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dtNow,
                Result = auditStatus  // audit_type = 8 供应商订单确认
            };

            dbcontext.Update(order);
            dbcontext.Add(pa);

            dbcontext.SaveChanges();

        }

    }
}
