using System;
using System.Collections.Generic;
using PurocumentLib.Model;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IPurchasingplanService:IService
    {
        //创建采购计划
        void CreatePlan(Model.PurchasingPlan plan); 
        //修改  
        void UpdatePlan(Model.PurchasingPlan plan);
        //加载
        PurchasingPlan Load(int id);    
        //草稿提交
        void SubmitPlan(IEnumerable<int> ids,int userID,string desc);
    } 
}
