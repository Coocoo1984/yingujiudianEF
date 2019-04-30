using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;

namespace PurocumentLib.Message.Handler
{
    public class PlanAudit2Handler : HandlerGeneric<PlanAudit2Request>
    {
        public PlanAudit2Handler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(PlanAudit2Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IPurchasingAuditService>();
            service.PlanAudit2(request.PlanID, request.UserID, request.Result, request.Desc);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
