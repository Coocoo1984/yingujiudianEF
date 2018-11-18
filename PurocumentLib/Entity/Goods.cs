using System;

namespace PurocumentLib.Entity
{
    public class Goods
    {
        public int ID{get;set;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string Specification{get;set;}
        public string Desc{get;set;}
        public int UnitID{get;set;}
        public int ClassID{get;set;}
        public bool Disable{get;set;}
    }
}
