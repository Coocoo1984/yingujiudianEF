using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class QuoteAuditRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int QuoteID{get;set;}
        public bool Result{get;set;}
        public int UserID{get;set;}
        public string Desc{get;set;}
    }
}
