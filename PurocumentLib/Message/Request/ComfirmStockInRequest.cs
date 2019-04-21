using DevelopBase.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Message.Request
{
    public class ComfirmStockInRequest : RequestBase
    {
        public string WechatID { get; set; }
        public int OrderID { get; set; }
        public bool Result { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }
    }
}
