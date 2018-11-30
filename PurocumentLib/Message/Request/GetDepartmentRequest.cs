using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class GetDepartmentRequest:RequestBase
    {
        public int ID{get;set;}
        public string Name{get;set;}
    }
}
