using System;
using PurocumentLib.Model;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class GetVendorRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
        public string Name{get;set;}
    }
}
