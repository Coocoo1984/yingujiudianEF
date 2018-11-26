using System;
using PurocumentLib.Service;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;

namespace PurocumentLib.Message.Handler
{
    public class ComfirmStockInHandler : HandlerGeneric<ComfirmStockInRequest>
    {
        public ComfirmStockInHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ComfirmStockInRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IPurchasingOrderService>();
            service.FinishOrder(request.OrderID, request.UserID, request.Result, request.Desc);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}