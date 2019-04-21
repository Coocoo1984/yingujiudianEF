using System;
using System.Collections.Generic;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class UpdatePurocumentPlanRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
        public string Desc{get;set;}
        public int UserID{get;set;}
        public IEnumerable<Model.PurchasingPlanDetail> Details{get;set;}
        public int CancelID { get; set; }
    }
}
