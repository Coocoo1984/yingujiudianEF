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
                var model = new Model.ChargeBackModel()
                {
                    PurchasingOrderID = request.OrderID,
                    CreateUsrID = request.CreateUserID,
                    CreateTime = request.CreateTime,
                    UpdateUserID = request.CreateUserID,
                    UpdateTime = request.CreateTime,
                    Details = request.Details.Where(w => w.PurchasingOrderDetailId > 0 && w.Count > 0).Select(s => new ChargeBackDetailModel()
                    {
                        PurchasingOrderDetailID = s.PurchasingOrderDetailId,
                        Count = s.Count
                    })
                };
                var purchaasingPlanService = ServiceProvider.GetService<IChargeBackService>();
                purchaasingPlanService.Add(model);
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }
            catch (Exception ex)
            {
                return new ResponseBase() { Result = -1, ResultInfo = ex.Message };
            }
        }
    }
}
