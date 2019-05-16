using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    public class Depot
    {
        public virtual int ID { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int DepartmentID { get; set; }
        public virtual int CreateUsrID { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual string Remark { get; set; }
        public virtual int ItemCount { get; set; }
        public bool Disable { get; set; }
        public virtual ICollection<DepotDetail> Details { get; set; }
    }
}
