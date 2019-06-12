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
        //角色
        IQueryable<Role> Role { get; }
        //用户
        IQueryable<Usr> Usr { get; }
        //供应商
        IQueryable<Vendor> Vendor{get;}
        //供应商关系
        IQueryable<RsVendor> RsVendor { get; }
        //采购计划
        IQueryable<PurchasingPlan> PurchasingPlan{get;}
        //采购计划明细
        IQueryable<PurchasingPlanDetail> PurchasingPlanDetail{get;}
        //审核记录
        IQueryable<PurchasingAudit> PurchasingAudits{get;}
        //供应商报价单
        IQueryable<Quote> Quotes{get;}
        //采购中心报价单审核
        IQueryable<QuoteAudit> QuoteAudits { get; }
        //报价单明细
        IQueryable<QuoteDetail> QuoteDetails{get;}
        //订单
        IQueryable<PurchasingOrder> PurchasingOrder { get; }
        //订单明细
        IQueryable<PurchasingOrderDetail> PurchasingOrderDetail { get; }
        //库存盘点
        IQueryable<Depot> Depot { get; }
        //库存盘点明细
        IQueryable<DepotDetail> DepotDetail { get; }
        //权限定义
        IQueryable<Permission> Permission { get; }
        //用户权限
        IQueryable<RsPermission> RsPermission { get; }
        //退货
        IQueryable<ChargeBack> ChargeBack { get; }
        //退货明细
        IQueryable<ChargeBackDetail> ChargeBackDetail { get; }



    }
}
