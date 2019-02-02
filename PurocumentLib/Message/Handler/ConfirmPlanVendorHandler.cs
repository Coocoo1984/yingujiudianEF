using System;
using PurocumentLib.Service;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class ConfirmPlanVendorHandler : HandlerGeneric<ConfirmPlanVendorRequest>
    {
        public ConfirmPlanVendorHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ConfirmPlanVendorRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IPurchasingplanService>();
            if (request.GoodsClassID ==0)
            {
                service.ConfirmVendor(request.PlanID, request.VendorID, request.GoodsClassID);
            }
            else
            { 
                
                service.ConfirmVendor(request.PlanID, request.VendorID, request.GoodsClassID);
            }
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
