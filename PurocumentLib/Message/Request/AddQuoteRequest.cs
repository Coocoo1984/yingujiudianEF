using System;
using DevelopBase.Message;
using System.Collections.Generic;
using PurocumentLib.Model;

namespace PurocumentLib.Message.Request
{
    public class AddQuoteRequest:RequestBase
    {
        public string WechatID { get; set; }
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public int VendorID{get;set;}
        public int BizTypeID{get;set;}
        public IEnumerable<QuoteDetailModel> Details{get;set;}
        public int CreateUserID { get;set;}
    }
}
