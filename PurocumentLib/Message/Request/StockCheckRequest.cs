using DevelopBase.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Message.Request
{
    public class StockCheckRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }
        public string Remark { get; set; }
        public List<Model.DepotDetailMedel> ListDepotDetails { get; set; }
    }
}
