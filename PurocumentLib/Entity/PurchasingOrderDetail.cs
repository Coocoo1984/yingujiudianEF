using System;

namespace PurocumentLib.Entity
{
    public class PurchasingOrderDetail
    {
        public virtual int ID { get; set; }
        public virtual int PurchasingOrderID { get; set; }
        public virtual PurchasingOrder PurchasingOrder { get; set; }
        public virtual int GoodsID { get; set; }
        public virtual int? GoodsClassID { get; set; }
        public virtual decimal Count { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal? Subtotal { get; set; }
        public virtual decimal? ActualCount { get; set; }
        public virtual decimal? ActualSubtotal { get; set; }
        public virtual int? CreateUsrID { get; set; }
        public virtual int? UpdateUsrID { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime UpdateTime { get; set; }
        public virtual int? PurchasingStateID { get; set; }//没用
        public virtual int? AuditUsrID { get; set; }
        public virtual DateTime? AuditTime { get; set; }
        public virtual int? Audit2UsrID { get; set; }
        public virtual DateTime? Audit2Time { get; set; }
        public virtual int? PurchasingOrderStateID { get; set; }
        public virtual int? PurchasingPlanDetailID { get; set; }
        //public virtual int? PurchasiongOrderStateID { get; internal set; }
    }
}
