using System;
using System.Collections.Generic;
namespace PurocumentLib.Model
{
    public class QuoteModel
    {
        public QuoteModel()
        {
            CreatDateTime = System.DateTime.Now;
            Status = 2;//´ýÉóºË
        }

        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public int VendorID{get;set;}
        public String VendorName{get;set;}
        public int BizTypeID{get;set;}
        public string BizTypeName{get;set;}
        public IEnumerable<QuoteDetailModel> Details{get;set;}
        public int CreateUserID{get;set;}
        public DateTime CreatDateTime{get;set;}
        public int UpdateUserID{get;set;}
        public DateTime UpdateTime{get;set;}
        public int ItemCount { get; set; }
        public bool Disable{get;set;}
        public int Status { get; set; }

    }
}
