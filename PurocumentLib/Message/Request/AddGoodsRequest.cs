using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class AddGoodsRequest:RequestBase
    {
        public string Name{get;set;}
        public int ClassID{get;set;}
        public int UnitID{get;set;}
    }
}
