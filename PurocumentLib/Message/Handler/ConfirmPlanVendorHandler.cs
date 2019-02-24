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
            //1对1选择供应商
            service.ConfirmVendor(request.PlanID, request.VendorID);

            ////if (request.GoodsClassID ==0)
            ////{
            ////    //1对1选择供应商
            ////    service.ConfirmVendor(request.PlanID, request.VendorID);
            ////}
            ////else
            ////{ 
            ////    //原有按照商品小类选择供应商
            ////    service.ConfirmVendor(request.PlanID, request.VendorID, request.GoodsClassID);
            ////}
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
