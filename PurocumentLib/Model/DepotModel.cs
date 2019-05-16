using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Model
{
    public class DepotModel
    {
        public DepotModel()
        {
            CreateTime = DateTime.Now;
        }

        public int ID { get; set; }
        public int DeptID { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Disable { get; set; }
    }
}
