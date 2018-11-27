using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Model
{
    public class PurchasingOrderDetailModel
    {
        public int ID { get; set; }
        public int PurchasingOrderID { get; set; }
        //public PurchasingOrderModel PurchasingOrder { get; set; }
        public int GoodsID { get; set; }
        public int? GoodsClassID { get; set; }
        public decimal? Count { get; set; }
        public decimal? Price { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? ActualCount { get; set; }
        public decimal? ActualSubtotal { get; set; }
        public int? CreateUsrID { get; set; }
        public int? UpdateUsrID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int? PurchasingStateID { get; set; }//没用
        public int? AuditUsrID { get; set; }
        public DateTime AuditTime { get; set; }
        public int? Audit2UsrID { get; set; }
        public DateTime Audit2Time { get; set; }
        public int PurchasingOrderStateID { get; set; }
        public int? PurchasingPlanDetailID { get; set; }
    }
}
