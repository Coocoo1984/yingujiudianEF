using System;
using DevelopBase.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using PurocumentLib.Entity;
namespace PurocumentLib.Dbcontext
{
    public interface IPurocumentDbcontext:IDbcontext
    {
        //采购业务类型
        IQueryable<BizType> BizTypes{get;}
        //计量单位
        IQueryable<Unit> Units{get;}
        //商品类别
        IQueryable<GoodsClass> GoodsClass{get;}
        //商品
        IQueryable<Goods> Goods{get;}
        //部门
        IQueryable<Department> Department{get;}
        //供应商
        IQueryable<Vendor> Vendor{get;}
        //采购计划
        IQueryable<PurchasingPlan> PurchasingPlan{get;}
        //采购计划明细
        IQueryable<PurchasingPlanDetail> PurchasingPlanDetail{get;}
    }
}
