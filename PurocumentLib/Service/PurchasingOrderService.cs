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
        /// 部门收货确认
        /// </summary>
        /// <param name="po"></param>
        /// <param name="userID"></param>
        public void CheckInOrder(PurchasingOrderModel po, int userID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 供应商发货确认
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userID"></param>
        public void ComfirmDelivery(IEnumerable<int> ids, int userID)
        {
            if (ids == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => ids.Contains(c.ID) && c.PurchasingOrderStatusID != 2) > 0)
            {
                throw new Exception("订单状态不正确");
            }
            var orders = dbcontext.PurchasingOrder.Where(w => ids.Contains(w.ID)).ToList();
            var listPAs = new List<PurchasingAudit>();
            DateTime dtNow = DateTime.Now;
            foreach (var item in orders)
            {
                item.PurchasingOrderStatusID = item.PurchasingOrderStatusID + 1;//
                item.UpdateUserID = userID;
                item.UpdateTime = dtNow;

                var oPA = new PurchasingAudit()
                {
                    PlanID = item.PurchasingPlanID,
                    UserID = userID,
                    CreateTime = dtNow,
                    Result = 5  // audit_type = 5 供应商确认发货
                };
                listPAs.Add(oPA);

                dbcontext.UpdateRange(orders);
                dbcontext.AddRange(listPAs);
                dbcontext.SaveChanges();
            }
        }

        /// <summary>
        /// 供应商确认订单
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userID"></param>
        public void ComfirmOrder(IEnumerable<int> ids, int userID)
        {
            if (ids == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => ids.Contains(c.ID) && c.PurchasingOrderStatusID != 1) > 0)
            {
                throw new Exception("订单状态不正确");
            }
            var orders = dbcontext.PurchasingOrder.Where(w => ids.Contains(w.ID)).ToList();
            var listPAs = new List<PurchasingAudit>();
            DateTime dtNow = DateTime.Now;
            foreach (var item in orders)
            {
                item.PurchasingOrderStatusID = item.PurchasingOrderStatusID + 1;//
                item.UpdateUserID = userID;
                item.UpdateTime = dtNow;

                var oPA = new PurchasingAudit()
                {
                    PlanID = item.PurchasingPlanID,
                    UserID = userID,
                    CreateTime = dtNow,
                    Result = 4  // audit_type = 4 供应商订单确认
                };
                listPAs.Add(oPA);

                dbcontext.UpdateRange(orders);
                dbcontext.AddRange(listPAs);
                dbcontext.SaveChanges();
            }
        }

        /// <summary>
        /// 部门全部收货完毕
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userID"></param>
        public void FinishOrder(int id, int userID)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.PurchasingOrder.Count(c => id.Equals(c.ID) && c.PurchasingOrderStatusID != 4) > 0)
            {
                throw new Exception("订单状态不正确");
            }
            PurchasingOrder order = dbcontext.PurchasingOrder.FirstOrDefault(w => id.Equals(w.ID));
            DateTime dtNow = DateTime.Now;

            order.PurchasingOrderStatusID = order.PurchasingOrderStatusID + 1;//
            order.UpdateUserID = userID;
            order.UpdateTime = dtNow;

            PurchasingAudit pa = new PurchasingAudit()
            {
                PlanID = order.PurchasingPlanID,
                UserID = userID,
                CreateTime = dtNow,
                Result = 7  // audit_type = 7 供应商订单确认
            };

            dbcontext.Update(order);
            dbcontext.Add(pa);
            dbcontext.SaveChanges();

        }

        public PurchasingOrderDetailModel Load(int id)
        {
            throw new NotImplementedException();
        }
    }
}
