using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class StockCheckHandler:HandlerGeneric<StockCheckRequest>
    {
        public StockCheckHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(StockCheckRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            if (request.ListDepotDetails?.Count <= 0)
            {
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }

            var service = ServiceProvider.GetService<IDepotService>();
            service.StockCheck(request.DepartmentID, request.UserID, request.ListDepotDetails);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
