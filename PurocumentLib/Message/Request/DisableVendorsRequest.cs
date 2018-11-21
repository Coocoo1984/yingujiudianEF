using System;
using DevelopBase.Message;
using System.Collections.Generic;
namespace PurocumentLib.Message.Request
{
    public class DisableVendorsRequest:RequestBase
    {
        public IEnumerable<int> VendorIDs;
    }
}
