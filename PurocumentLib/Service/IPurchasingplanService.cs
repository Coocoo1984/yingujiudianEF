using System;
using PurocumentLib.Model;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IPurchasingplanService:IService
    {
        //创建采购计划
        void CreatePlan(PurchasingPlan plan);   
        void UpdatePlan(PurchasingPlan plan);
        PurchasingPlan Load(int id);    
    } 
}
