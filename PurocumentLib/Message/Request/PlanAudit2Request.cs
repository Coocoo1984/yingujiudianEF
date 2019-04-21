using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class PlanAudit2Request : RequestBase
    {
        public string WechatID { get; set; }
        public int PlanID { get; set; }
        public bool Result { get; set; }
        public int UserID { get; set; }
        public string Desc { get; set; }
    }
}
