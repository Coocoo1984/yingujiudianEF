using System;
using System.Collections.Generic;

namespace PurocumentLib.Model
{
    public class ChargeBackModel
    {
        public ChargeBackModel()
        {
            CreateTime = DateTime.Now;
            PurchasingOrderStatusID = 1;
        }

        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int PurchasingOrderID { get; set; }
        public int CreateUsrID { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateTime { get; set; }
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
        public int PurchasingOrderStatusID { get; set; }
        public IEnumerable<ChargeBackDetailModel> Details { get; set; }
    }
}
