using System;
using System.Collections.Generic;
using DevelopBase.Message;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Request
{
    public class UpdateQuoteRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public int VendorID{get;set;}
        public int BizTypeID{get;set;}
        public IEnumerable<QuoteDetailModel> Details{get;set;}
        public int UpdateUserID{get;set;}

    }
}
