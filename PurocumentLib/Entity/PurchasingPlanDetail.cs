using System;

namespace PurocumentLib.Entity
{
    public class PurchasingPlanDetail
    {
        public virtual int ID{get;set;}
        public virtual int PurchasingPlanID{get;set;}
        public virtual PurchasingPlan PurchasingPlan{get;set;}
        public virtual int? GoodsClassID{get;set;}
        public virtual int GoodsID { get; set; }
        public virtual int PurchasingCount{get;set;}
        public virtual int? VendorID{get;set;}
        public virtual int? QuoteDetailID{get;set;}
        public virtual decimal Price { get; set; }
        public virtual int? CreateUsrID { get; set; }
        public virtual DateTime? CreateTime{get;set;}
        public virtual int? UpdateUsrID { get; set; }
        public virtual DateTime? UpdateTime{get;set;}
        public virtual int Status { get; set; }


    }
}
