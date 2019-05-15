using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;

namespace PurocumentLib.Message.Handler
{
    public class QuoteAudit2Handler : HandlerGeneric<QuoteAudit2Request>
    {
        public QuoteAudit2Handler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(QuoteAudit2Request request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IQuoteAuditService>();
            service.QuoteAudit2(request.QuoteID, request.UserID, request.Result, request.Desc);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
