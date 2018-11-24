using System;
using System.Collections.Generic;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    //采购计划提交初审
    public class SubmitPlanRequest:RequestBase
    {
        public IEnumerable<int> IDs{get;set;}
        public int UserID{get;set;}
        public string Desc{get;set;}
    }
}
