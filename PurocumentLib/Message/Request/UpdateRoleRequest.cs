using System;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    class UpdateRoleRequest : RequestBase
    {
        public int ID { get; set; }
        public string WechatGroupID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
