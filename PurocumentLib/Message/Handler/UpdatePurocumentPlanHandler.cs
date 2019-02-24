using System;
using DevelopBase.Message;
using PurocumentLib.Model;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
using static DevelopBase.Services.ServiceBase;

namespace PurocumentLib.Message.Handler
{
    public class UpdatePurocumentPlanHandler : HandlerGeneric<UpdatePurocumentPlanRequest>
    {
        public UpdatePurocumentPlanHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdatePurocumentPlanRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }

            var model=new Model.PurchasingPlan()
            {
                ID=request.ID,
                Desc=request.Desc,
                UpdateUser=request.UserID,
                UpdateTime=DateTime.Now,
                Details=request.Details
            };
            if (request.CancelID > 0)
            {
                //草稿状态下采购计划的删除操作
                model.Status = (int)EnumPurchasingPlanState.Cancelled;
                model.ID = request.CancelID;
            }
            var service=ServiceProvider.GetService<IPurchasingplanService>();
            service.UpdatePlan(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
