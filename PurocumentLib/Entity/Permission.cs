using System;
using System.Collections.Generic;

namespace PurocumentLib.Entity
{
    public partial class Permission
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public ICollection<RsPermission> RsPermission { get; set; }
    }
}
