using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Model;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class GetPurchasingPlanHandler : HandlerGeneric<GetPurchasingPlanRequest>
    {
        public GetPurchasingPlanHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetPurchasingPlanRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
