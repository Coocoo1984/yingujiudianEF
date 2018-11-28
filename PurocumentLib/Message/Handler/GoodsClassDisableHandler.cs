using System;
using DevelopBase.Message;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
namespace PurocumentLib.Message.Handler
{
    public class GoodsClassDisableHandler : HandlerGeneric<GoodsClassDisableRequest>
    {
        public GoodsClassDisableHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GoodsClassDisableRequest request)
        {
            var service=ServiceProvider.GetService<IGoodsClassService>();
            service.Disable(request.ID);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
