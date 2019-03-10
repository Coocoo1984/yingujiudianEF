using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Model;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class UpdateQuoteHandler : HandlerGeneric<UpdateQuoteRequest>
    {
        public UpdateQuoteHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateQuoteRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var model = new QuoteModel()
            {
                ID = request.ID,
                Code = request.Code,
                Name = request.Name,
                Desc = request.Desc,
                Details = request.Details,
                VendorID = request.VendorID,
                UpdateUserID = request.UpdateUserID
            };
            var service=ServiceProvider.GetService<IQuoteService>();
            service.Update(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
