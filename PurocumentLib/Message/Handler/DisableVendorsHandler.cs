using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class DisableVendorsHandler : HandlerGeneric<DisableVendorsRequest>
    {
        public DisableVendorsHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(DisableVendorsRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IVendorService>();
            service.Disable(request.VendorIDs);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
