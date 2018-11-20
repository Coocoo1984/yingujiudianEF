using System;
using DevelopBase.Message;
using System.Collections.Generic;
namespace PurocumentLib.Message.Request
{
    public class DisableGoodsRequest:RequestBase
    {
        public IEnumerable<int> GoodsID{get;set;}
    }
}
