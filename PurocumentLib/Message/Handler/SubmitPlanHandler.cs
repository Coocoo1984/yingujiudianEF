using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class SubmitPlanHandler : HandlerGeneric<SubmitPlanRequest>
    {
        public SubmitPlanHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(SubmitPlanRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            ServiceProvider.GetService<IPurchasingplanService>()?.SubmitPlan(request.IDs,request.UserID);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
