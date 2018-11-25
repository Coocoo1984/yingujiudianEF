using System;

namespace PurocumentLib.Entity
{
    public class PurchasingOrderDetail
    {
        public int ID { get; set; }
        public int PurchasingOrderID { get; set; }
        public PurchasingOrder PurchasingOrder { get; set; }
        public int GoodsID { get; set; }
        public int? GoodsClassID { get; set; }
        public int Count { get; set; }
        public decimal? Price { get; set; }
        public decimal? Subtotal { get; set; }
        public int ActualCount { get; set; }
        public decimal? ActualSubtotal { get; set; }
        public decimal CreateUsrID { get; set; }
        public decimal UpdateUsrID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int PurchasingStateID { get; set; }//没用
        public decimal AuditUsrID { get; set; }
        public DateTime AuditTime { get; set; }
        public decimal Audit2UsrID { get; set; }
        public DateTime Audit2Time { get; set; }
        public int PurchasingOrderStateID { get; set; }
        public int PurchasingPlanDetailID { get; set; }
    }
}
