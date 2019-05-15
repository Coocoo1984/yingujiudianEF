using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;

namespace PurocumentLib.Message.Handler
{
    public class QuoteAuditHandler : HandlerGeneric<QuoteAuditRequest>
    {
        public QuoteAuditHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(QuoteAuditRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IQuoteAuditService>();
            service.QuoteAudit(request.QuoteID, request.UserID, request.Result, request.Desc);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
