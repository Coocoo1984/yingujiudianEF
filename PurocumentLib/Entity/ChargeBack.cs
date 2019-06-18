using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    //退货
    public class ChargeBack
    {
        public virtual int ID { get; set; }
        public virtual string Code { get; set; }
        public virtual int PurchasingOrderID { get; set; }
        public virtual PurchasingOrder PurchasingOrder { get; set; }
        public virtual int ItemCount { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int CreateUsrID { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual int UpdateUserID { get; set; }
        public virtual DateTime UpdateTime { get; set; }
        public virtual int PurchasingOrderStatusID { get; set; }

        public virtual ICollection<ChargeBackDetail> Details { get; set; }
    }
}
