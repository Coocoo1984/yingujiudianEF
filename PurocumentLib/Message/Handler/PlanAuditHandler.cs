using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;

namespace PurocumentLib.Message.Handler
{
    public class PlanAuditHandler : HandlerGeneric<PlanAuditRequest>
    {
        public PlanAuditHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(PlanAuditRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IPurchasingAuditService>();
            service.PlanAudit(request.PlanID,request.UserID,request.Result,request.Desc);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
