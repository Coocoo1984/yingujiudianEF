using System;
using System.Collections.Generic;
using PurocumentLib.Model;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IPurchasingOrderService : IService
    {
        //创建采购计划 目前在采购计划复审通过后自动创建
        //void CreateOrder(Model.PurchasingPlan plan);
        ////修改  
        //void UpdateOrder(Model.PurchasingOrderModel order, int userID);
        //加载
        PurchasingOrderDetailModel Load(int id);
        //确认订单
        void ComfirmOrder(IEnumerable<int> ids, int userID);
        //确认发货
        void ComfirmDelivery(IEnumerable<int> ids, int userID);
        //收货
        void CheckInOrder(Model.PurchasingOrderModel order, int userID);
        //确认收货完成
        void FinishOrder(int id, int userID);
    }
}
