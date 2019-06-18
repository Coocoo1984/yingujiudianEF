using System;
using System.Collections.Generic;
using PurocumentLib.Model;
using DevelopBase.Services;

namespace PurocumentLib.Service
{
    public interface IChargeBackService : IService
    {
        //创建退货计划
        void Add(Model.ChargeBackModel chargeBack);
        //修改  
        void Update(Model.ChargeBackDetailModel chargeBackDetail);
        //加载
        ChargeBackModel Load(int id);
        //采购中心审核退货计划
        void Audit(int planId, int userID, bool isPass, string Desc);
        //供应商确认退货
        void VendorComfirm(int planId, int userID, bool isPass, string Desc);
        //需求部门确认退货完成
        void Finish(int orderId, int userID, bool isPass, string Desc);
    }
}
