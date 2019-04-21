using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class UpdateGoodsClassRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public int BizTypeID{get;set;}
    }
}
