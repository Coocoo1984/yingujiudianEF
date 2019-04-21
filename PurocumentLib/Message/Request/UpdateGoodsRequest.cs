using System;
using DevelopBase.Common;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class UpdateGoodsRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Specification{get;set;}
        public string Desc{get;set;}
        public int UnitID{get;set;}
        public int ClassID{get;set;}

    }
}
