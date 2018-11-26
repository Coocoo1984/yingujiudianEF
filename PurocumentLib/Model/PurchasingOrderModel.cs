using System;
using System.Collections.Generic;
namespace PurocumentLib.Model
{
    public class PurchasingOrderModel
    {
        public PurchasingOrderModel()
        {
            CreateTime = DateTime.Now;
            PurchasingOrderStatusID = 1;
        }

        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int PurchasingPlanID { get; set; }
        public int VendorID { get; set; }
        public int DepartmentID { get; set; }
        public string Tel { get; set; }
        public string Addr { get; set; }
        public int BizTypeID { get; set; }
        public int CreateUsrID { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateTime { get; set; }
        public int ItemCount { get; set; }
        public decimal? Total { get; set; }
        public int PurchasingOrderStatusID { get; set; }
        public IEnumerable<PurchasingOrderDetailModel> Details { get; set; }
    }

}