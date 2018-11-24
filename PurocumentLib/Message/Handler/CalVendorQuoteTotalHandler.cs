using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class CalVendorQuoteTotalHandler : HandlerGeneric<CalVendorQuoteTotalRequest>
    {
        public CalVendorQuoteTotalHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(CalVendorQuoteTotalRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var servcie=ServiceProvider.GetService<IPurchasingplanService>();
            var total=servcie.CalPlanPriceTotal(request.PlanID,request.VendorID,request.GoodsClassID);
            return new ResponseGeneric<decimal>(){Result=1,ResultInfo="",Data=total};
        }
    }
}
