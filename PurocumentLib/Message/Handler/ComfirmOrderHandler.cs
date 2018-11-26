using System;
using PurocumentLib.Service;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;

namespace PurocumentLib.Message.Handler
{
    public class ComfirmOrderHandler: HandlerGeneric<ComfirmOrderRequest>
    {
        public ComfirmOrderHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ComfirmOrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IPurchasingOrderService>();
            service.ComfirmOrder(request.OrderID, request.UserID, request.Result, request.Desc);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
