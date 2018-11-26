using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Message.Request
{
    /// <summary>
    /// 供应商确认订单
    /// </summary>
    public class ComfirmOrderRequest
    {
        public int OrderID { get; set; }
        public bool Result { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }
    }
}
