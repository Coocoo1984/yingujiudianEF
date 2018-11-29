using System;
using PurocumentLib.Model;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class GetVendorRequest:RequestBase
    {
        public string ID{get;set;}
    }
}
