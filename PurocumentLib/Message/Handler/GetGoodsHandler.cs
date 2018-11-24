using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Model;
using PurocumentLib.Service;
using DevelopBase.Common;
namespace PurocumentLib.Message.Handler
{
    public class GetGoodsHandler : HandlerGeneric<GetGoodsRequest>
    {
        public GetGoodsHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetGoodsRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IGoodsService>();
            var goodsModel=service.Load(request.ID);
            return new ResponseGeneric<Model.Goods>(){Result=1,ResultInfo="",Data=goodsModel};
        }
    }
}
