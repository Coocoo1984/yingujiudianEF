using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class PlanAuditRequest:RequestBase
    {
        public int PlanID{get;set;}
        public bool Result{get;set;}
        public int UserID{get;set;}
        public string Desc{get;set;}
    }
}
