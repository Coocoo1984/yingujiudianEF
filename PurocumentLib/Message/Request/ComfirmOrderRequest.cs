using DevelopBase.Message;
using PurocumentLib.Model;
using System;
using System.Collections.Generic;

namespace PurocumentLib.Message.Request
{
    /// <summary>
    /// 供应商确认订单
    /// </summary>
    public class ComfirmOrderRequest : RequestBase
    {
        public string WechatID { get; set; }
        public int OrderID { get; set; }
        public bool Result { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }
}
}
