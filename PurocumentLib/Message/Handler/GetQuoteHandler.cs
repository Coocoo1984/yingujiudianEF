using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class GetQuoteHandler : HandlerGeneric<GetQuoteRequest>
    {
        public GetQuoteHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetQuoteRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IQuoteService>();
            var model=service.Load(request.ID);
            return new ResponseGeneric<QuoteModel>(){Result=1,ResultInfo="",Data=model};
        }
    }
}
