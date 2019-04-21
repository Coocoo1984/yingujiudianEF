using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class GetUnitRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
    }
}
