using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class CreatePurocumentPlanRequest:RequestBase
    {
        public string WechatID { get; set; }
        public int DepartmentID { get; set; }
        public int BizTypeID { get; set; }
        public IEnumerable<PurocumentPlanDetail> Details { get; set; }
        public DateTime CreateTime { get; set; }=DateTime.Now;
        public int CreateUserID { get; set; }

    }
    public class PurocumentPlanDetail
    {
        public string WechatID { get; set; }
        public int GoodsID { get; set; }
        public int PurocumentCount { get; set; }
    }
}
