using System;
using PurocumentLib.Service;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;

namespace PurocumentLib.Message.Handler
{
    public class ComfirmDeliveryHandler : HandlerGeneric<ComfirmDeliveryRequest>
    {
        public ComfirmDeliveryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ComfirmDeliveryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IPurchasingOrderService>();
            service.ComfirmDelivery(request.OrderID, request.UserID, request.Result, request.Desc);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
