using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using System;

namespace PurocumentLib.Message.Handler
{
    public class PlanAudit3Handler : HandlerGeneric<PlanAudit3Request>
    {
        public PlanAudit3Handler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(PlanAudit3Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IPurchasingAuditService>();
            service.ComfirmPlanAndSubmitOrder(request.PlanID, request.UserID, request.Result, request.Desc);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
