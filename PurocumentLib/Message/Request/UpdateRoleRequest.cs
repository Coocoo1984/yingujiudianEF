using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class UpdateRoleRequest : RequestBase
    {
        public string WechatID { get; set; }
        public int ID { get; set; }
        public string WechatGroupID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
