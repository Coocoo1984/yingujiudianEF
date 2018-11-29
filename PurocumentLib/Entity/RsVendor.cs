using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    public class RsVendor
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public int BizTypeID { get; set; }
        public int GoodsClassID { get; set; }
        public int? GoodsID { get; set; }

        public Vendor Vendor { get; set; }
    }
}
