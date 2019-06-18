using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    public class ChargeBackDetail
    {
        public virtual int ID { get; set; }
        public virtual int ChargeBackID { get; set; }
        public virtual ChargeBack ChargeBack { get; set; }
        public virtual decimal Count { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Subtotal { get; set; }
        public virtual int PurchasingOrderDetailID { get; set; }

    }
}
