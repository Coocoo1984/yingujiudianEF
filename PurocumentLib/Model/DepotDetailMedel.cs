using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Model
{
    public class DepotDetailMedel
    {
        public int ID { get; set; }
        public int DepotID { get; set; }
        public DepotModel Depot { get; set; }
        public int GoodsID { get; set; }
        public int GoodsClassID { get; set; }
        public decimal Count { get; set; }
        public int? CreateUsrID { get; set; }
        public int? UpdateUsrID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int? PurchasingPlanDetailID { get; set; }
    }
}
