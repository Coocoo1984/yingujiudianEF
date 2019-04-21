using System;
using System.Collections.Generic;
using DevelopBase.Message;
using PurocumentLib.Model;

namespace PurocumentLib.Message.Request
{
    public class UpdateVendorRequest:RequestBase
    {
        public string WechatID { get; set; }
        //public IEnumerable<int> BizTypIDs { get; set; }
        public IEnumerable<int> GoodsClassIDs { get; set; }

        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public string Tel{get;set;}
        public string Tel1{get;set;}
        public string Mobile{get;set;}
        public string Mobile1{get;set;}
        public string Address{get;set;}
        public string Address1{get;set;}
        public string Remark{get;set;}
    }
}
