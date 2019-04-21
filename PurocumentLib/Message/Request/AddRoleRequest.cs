using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class AddRoleRequest : RequestBase
    {
        public string WechatID { get; set; }
        public string WechatGroupID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
