using System;
using DevelopBase.Message;
using PurocumentLib.Model;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
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
            var service=ServiceProvider.GetService<IPurchasingplanService>();
            service.UpdatePlan(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
