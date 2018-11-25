using System;

namespace PurocumentLib.Entity
{
    //审核记录
    public class PurchasingAudit
    {
        public int ID{get;set;}
        public int PlanID{get;set;}
        public int UserID { get; set; }
        public int Result{get;set;}
        public string Desc{get;set;}
        public DateTime CreateTime{get;set;}
    }
}
