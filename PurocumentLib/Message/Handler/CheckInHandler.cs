using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;

namespace PurocumentLib.Message.Handler
{
    public class CheckInHandler : HandlerGeneric<CheckInRequest>
    {
        public CheckInHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(CheckInRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            if (request.ListOrderDetailIDAndActualCount?.Count<=0)
            {
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }
            
            var service = ServiceProvider.GetService<IPurchasingOrderService>();
            service.CheckInOrder(request.OrderID, request.UserID, request.ListOrderDetailIDAndActualCount);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
