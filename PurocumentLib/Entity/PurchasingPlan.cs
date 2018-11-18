using System;
using System.Collections.Generic;

namespace PurocumentLib.Entity
{
    //采购计划
    public class PurchasingPlan
    {
        public virtual int ID{get;set;}
        public virtual string Code{get;set;}
        public virtual string Name{get;set;}
        public virtual string Desc{get;set;}
        public virtual int DepartmentID{get;set;}
        public virtual int BizTypeID{get;set;}
        public virtual int CreateUserID{get;set;}
        public virtual DateTime CreateTime{get;set;}
        public virtual int UpdateUserID{get;set;}
        public virtual DateTime UpdateTime{get;set;}
        public virtual int ItemCount{get;set;}
        public virtual ICollection<PurchasingPlanDetail> Details{get;set;}
        public int Status{get;set;}
    }
}
