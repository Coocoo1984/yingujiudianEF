using System;
using System.Collections.Generic;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    //采购计划提交
    public class SumbitPlanRequest:RequestBase
    {
        public IEnumerable<int> Ids{get;set;}
        public int UserID{get;set;}
        public string Desc{get;set;}
    }
}
