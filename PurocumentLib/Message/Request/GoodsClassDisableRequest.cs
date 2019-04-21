using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class GoodsClassDisableRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
    }
}