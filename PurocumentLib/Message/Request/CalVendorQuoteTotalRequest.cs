using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class CalVendorQuoteTotalRequest:RequestBase
    {
        public string WechatID { get; set; }
        //采购计划ID
        public int PlanID{get;set;}
        //商品类别ID
        public int GoodsClassID{get;set;}
        //供应商ID
        public int VendorID{get;set;}
    }
}