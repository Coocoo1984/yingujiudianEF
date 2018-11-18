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
        public int Status{get;private set;}
        public IEnumerable<PurchasingPlanDetail> Details{get;set;}
        public DateTime CreateTime{get;private set;}
        public int CreateUser{get;set;}
        
    }
    
}