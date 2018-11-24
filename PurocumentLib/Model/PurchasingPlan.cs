using System;
using System.Collections.Generic;
namespace PurocumentLib.Model
{
    public class PurchasingPlan
    {
        public PurchasingPlan()
        {
            CreateTime=DateTime.Now;
            Status=1;
        }
        public int ID{get;set;}
        public int DepartmentID{get;set;}
        public int BizType{get;set;}
        public string Desc{get;set;}
        public int Status{get;set;}
        public IEnumerable<PurchasingPlanDetail> Details{get;set;}
        public DateTime CreateTime{get;set;}
        public int CreateUser{get;set;}
        public DateTime UpdateTime{get;set;}
        public int UpdateUser{get;set;}
        
    }
    
}