using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class AddGoodsClassRequest:RequestBase
    {
        public string WechatID { get; set; }
        public string Code{get;set;}
        public string Name
        {
            get;
            set;
        }
        public string Desc
        {
            get;
            set;
        }
        public int BizTypeID{get;set;}
    }
}
