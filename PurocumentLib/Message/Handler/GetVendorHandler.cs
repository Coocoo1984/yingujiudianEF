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
            VendorModel model = null;
            if(request.ID !=0)
            {
                model = service.Load(request.ID);
            }
            else if(!string.IsNullOrWhiteSpace(request.Name))
            {
                model = service.GetByName(request.Name);
            }
            return new ResponseGeneric<VendorModel>(){Result=1,ResultInfo="",Data=model};
        }
    }
}
