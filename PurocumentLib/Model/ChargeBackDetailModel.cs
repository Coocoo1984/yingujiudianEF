using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Model
{
    public class ChargeBackDetailModel
    {
        public int ID { get; set; }
        public int ChargeBackID { get; set; }
        public int PurchasingOrderDetailID { get; set; }
        public decimal Count { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}
