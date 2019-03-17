using System;

namespace PurocumentLib.Model
{
    public class UsrModel
    {
        public int ID { get; set; }
        public string WechatID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Tel { get; set; }
        public string Tel1 { get; set; }
        public string Mobile { get; set; }
        public string Mobile1 { get; set; }
        public string Addr { get; set; }
        public string Addr1 { get; set; }
        public int DepartmentID { get; set; }
        public int? VendorID { get; set; }
        public int? RoleID { get; set; }
        public bool Disable { get; set; }
    }
}
