﻿using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
using PurocumentLib.Service;
using System.Linq;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class CreatePurocumentPlanHandler : HandlerBase<CreatePurocumentPlanRequest>
    {
        public CreatePurocumentPlanHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(CreatePurocumentPlanRequest request)
        {
            try
            {
                var departmentService = ServiceProvider.GetService<IDepartmentService>();
                if (!departmentService.ValidateDepartment(new int[] { request.DepartmentID }))
                {
                    throw new Exception("部门错误");
                }
                var bizService = ServiceProvider.GetService<IBizTypeService>();
                if (!bizService.ValidateBizTypeID(new int[] { request.BizTypeID }))
                {
                    throw new Exception("业务类型无效");
                }
                var goodsService = ServiceProvider.GetService<IGoodsService>();
                if (!goodsService.ValidateGoodsID(request.Details.Select(s => s.GoodsID).ToArray()))
                {
                    throw new Exception("商品信息无效");
                }
                var model = new PurchasingPlan()
                {
                    BizType = request.BizTypeID,
                    DepartmentID = request.DepartmentID,
                    CreateUser = request.CreateUserID,
                    Details=request.Details.Select(s=>new PurchasingPlanDetail() { GoodsID=s.GoodsID, PurchasingPlanCount=s.PurocumentCount })
                };
                var purchaasingPlanService = ServiceProvider.GetService<IPurchasingplanService>();
                purchaasingPlanService.CreatePlan(model);
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }
            catch (Exception ex)
            {
                return new ResponseBase() { Result = -1, ResultInfo = ex.Message };
            }
        }
    }
}
