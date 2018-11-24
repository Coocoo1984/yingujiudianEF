using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using DevelopBase.Common;
using PurocumentLib.Dbcontext;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class GetVendorHandler : HandlerGeneric<GetVendorRequest>
    {
        public GetVendorHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetVendorRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IVendorService>();
            var model= service.Load(request.ID);
            return new ResponseGeneric<VendorModel>(){Result=1,ResultInfo="",Data=model};
        }
    }
}
