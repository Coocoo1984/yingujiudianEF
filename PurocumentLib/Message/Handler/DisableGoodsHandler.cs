using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Model;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class DisableGoodsHandler : HandlerGeneric<DisableGoodsRequest>
    {
        public DisableGoodsHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(DisableGoodsRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IGoodsService>();
            service.Disable(request.GoodsID);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
