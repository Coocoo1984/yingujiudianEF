using System;
using System.Collections.Generic;
using System.Text;

namespace PurocumentLib.Entity
{
    public class QuoteAudit
    {
        public int ID { get; set; }
        public int QuoteID { get; set; }
        public int UserID { get; set; }
        public int Result { get; set; }
        public string Desc { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
