using DevelopBase.Message;
using PurocumentLib.Model;
using System;
using System.Collections.Generic;

namespace PurocumentLib.Message.Request
{
    public class CheckInRequest: RequestBase
    {
        public string WechatID { get; set; }
        public int OrderID { get; set; }
        public bool Result { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }
        public List<Model.PurchasingOrderDetailModel> ListOrderDetailIDAndActualCount { get; set; }
    }
}
