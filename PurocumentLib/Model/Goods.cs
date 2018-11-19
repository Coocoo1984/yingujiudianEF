using System;
namespace PurocumentLib.Model
{
    public class Goods
    {
        public int ID{get;set;}
        public string Name{get;set;}
        public int UnitID{get;set;}
        public int ClassID{get;set;}
        public bool Disable{get;set;}
        public string Specification{get;set;}
        public string Desc{get;set;}
    }
}