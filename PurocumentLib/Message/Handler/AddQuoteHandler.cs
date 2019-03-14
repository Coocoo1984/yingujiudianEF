using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class AddQuoteHandler : HandlerGeneric<AddQuoteRequest>
    {
        public AddQuoteHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddQuoteRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var model = new QuoteModel()
            {
                BizTypeID = request.BizTypeID,
                Code = request.Code,
                Name = request.Name,
                Desc = request.Desc,
                Details = request.Details,
                VendorID = request.VendorID,
                CreateUserID = request.CreateUserID,
                Disable = false
            };
            var service=ServiceProvider.GetService<IQuoteService>();
            service.Add(model);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
