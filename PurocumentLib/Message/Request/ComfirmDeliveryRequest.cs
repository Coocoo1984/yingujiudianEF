using DevelopBase.Message;
using System;

namespace PurocumentLib.Message.Request
{
    public class ComfirmDeliveryRequest : RequestBase
    {
        public string WechatID { get; set; }
        public int OrderID { get; set; }
        public bool Result { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }
    }
}
