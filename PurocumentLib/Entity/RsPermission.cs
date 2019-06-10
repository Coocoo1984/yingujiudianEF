using System;

namespace PurocumentLib.Entity
{
    public class RsPermission
    {
        public long Id { get; set; }
        public string UsrWechatId { get; set; }
        public long PermissionId { get; set; }
        public bool Disable { get; set; }

        public Permission Permission { get; set; }
    }
}
