using System;
namespace PurocumentLib.Model
{
    public class GoodsClassModel
    {
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Desc{get;set;}
        public int BizTypeID{get;set;}
        public bool Disable{get;set;}
    }
}