using System;

namespace PurocumentLib.Entity
{
    public class QuoteDetail
    {
        public int ID{get;set;}
        public int QuoteID{get;set;}
        public Quote Quote{get;set;}
        public int GoodsID{get;set;}
        public decimal Price{get;set;}
        public int GoodsClassID{get;set;}
        public bool Disable { get; set; }
    }
}
