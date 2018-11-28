using System;
using System.Collections.Generic;

namespace PurocumentLib.Model
{
    public class VendorModel
    {
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public string Tel{get;set;}
        public string Tel1{get;set;}
        public string Mobile{get;set;}
        public string Mobile1{get;set;}
        public string Address{get;set;}
        public string Address1{get;set;}
        public string Remark{get;set;}
        public bool Disable{get;set;}

        public IEnumerable<RsVendorModel> RsVendors { get; set; }
    }
}