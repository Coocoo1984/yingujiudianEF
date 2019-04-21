using System;
using DevelopBase.Message;
using System.Collections.Generic;
namespace PurocumentLib.Message.Request
{
    public class DisableGoodsRequest:RequestBase
    {
        public string WechatID { get; set; }
        public IEnumerable<int> GoodsID{get;set;}
    }
}
