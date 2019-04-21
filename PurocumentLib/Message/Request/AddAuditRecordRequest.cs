using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    ///审核记录
    public class AddAuditRecordRequest : RequestBase
    {
        public string WechatID { get; set; }
        //采购计划ID
        public int PlanID { get; set; }
        //审核人
        public int UserID { get; set; }
        //审核结果
        public bool IsPass { get; set; }
        //审核意见
        public string Desc { get; set; }
    }
}
