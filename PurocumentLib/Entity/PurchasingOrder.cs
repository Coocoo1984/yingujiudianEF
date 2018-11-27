using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    //订单
    public class PurchasingOrder
    {
        public virtual int ID { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Desc { get; set; }
        public virtual int PurchasingPlanID { get; set; }
        public virtual int VendorID { get; set; }
        public virtual int DepartmentID { get; set; }
        public virtual string Tel { get; set; }
        public virtual string Addr { get; set; }
        public virtual int BizTypeID { get; set; }
        public virtual int CreateUsrID { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual int UpdateUserID { get; set; }
        public virtual DateTime UpdateTime { get; set; }
        public virtual int ItemCount { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual int PurchasingOrderStatusID { get; set; }
        public virtual ICollection<PurchasingOrderDetail> Details { get; set; }
    }
}
