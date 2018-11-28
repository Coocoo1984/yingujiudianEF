using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
using PurocumentLib.Service;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class GetGoodsClassHandler : HandlerGeneric<GetGoodsClassRequest>
    {
        public GetGoodsClassHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetGoodsClassRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IGoodsClassService>();
            var response=service.Load(request.ID);
            return new ResponseGeneric<GoodsClassModel>(){Result=1,ResultInfo="",Data=response};
        }
    }
}
