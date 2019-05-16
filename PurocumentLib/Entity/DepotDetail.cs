using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    public class DepotDetail
    {
        public virtual int ID { get; set; }
        public virtual int DepotID { get; set; }
        public virtual Depot Depot { get; set; }
        public virtual int GoodsClassID { get; set; }
        public virtual int GoodsID { get; set; }
        public virtual int Count { get; set; }
        public virtual int CreateUsrID { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual int? UpdateUsrID { get; set; }
        public virtual DateTime? UpdateTime { get; set; }
        public virtual string Remark { get; set; }
    }
}
