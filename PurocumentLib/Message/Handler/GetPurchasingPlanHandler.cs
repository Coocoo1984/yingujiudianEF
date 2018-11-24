using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Model;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class GetPurchasingPlanHandler : HandlerGeneric<GetPurchasingPlanRequest>
    {
        public GetPurchasingPlanHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetPurchasingPlanRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IPurchasingplanService>();
            var response=service.Load(request.ID);
            return new ResponseGeneric<Model.PurchasingPlan>(){Result=1,ResultInfo="",Data=response};
        }
    }
}
