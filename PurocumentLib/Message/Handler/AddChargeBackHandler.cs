using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
using PurocumentLib.Service;
using System.Linq;
using PurocumentLib.Model;
using DevelopBase.Services;

namespace PurocumentLib.Message.Handler
{
    public class AddChargeBackHandler: HandlerGeneric<AddChargeBackRequest>
    {
        public AddChargeBackHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddChargeBackRequest request)
        {
            try
            {
                var permissionService = ServiceProvider.GetService<IPermissionService>();
                if (!permissionService.CheckPermission(request.WechatID, "AddChargeBackRequest"))
                {
                    return new ResponseBase() { Result = -1, ResultInfo = ServiceBase.NoPermissionString };
                }


                var departmentService = ServiceProvider.GetService<IDepartmentService>();
                if (!departmentService.ValidateDepartment(new int[] { request.DepartmentID }))
                {
                    throw new Exception("部门错误");
                }
                ////var model = new ChargeBack()
                ////{
                ////    BizType = request.BizTypeID,
                ////    DepartmentID = request.DepartmentID,
                ////    CreateUser = request.CreateUserID,
                ////    Details = request.Details.GroupBy(g => g.GoodsID).Select(s => new PurchasingPlanDetail()
                ////    {
                ////        GoodsID = s.Key,
                ////        PurchasingPlanCount = s.Sum(g => g.PurocumentCount)//?
                ////    })
                ////};
                ////var purchaasingPlanService = ServiceProvider.GetService<IPurchasingplanService>();
                ////purchaasingPlanService.CreatePlan(model);
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }
            catch (Exception ex)
            {
                return new ResponseBase() { Result = -1, ResultInfo = ex.Message };
            }
        }
    }
}
