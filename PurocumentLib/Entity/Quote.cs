using System;
using System.Collections.Generic;
namespace PurocumentLib.Entity
{
    public class Quote
    {
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public int VendorID{get;set;}
        public int BizTypeID{get;set;}
        public ICollection<QuoteDetail> Details{get;set;}
        public int CreateUserID{get;set;}
        public DateTime CreatDateTime{get;set;}
        public int? UpdateUserID{get;set;}
        public DateTime? UpdateDateTime{get;set;}
        public virtual int ItemCount { get; set; }
        public bool Disable{get;set;}
        public int Status { get; set; }


    }
}
