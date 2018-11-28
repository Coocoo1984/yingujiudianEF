using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using DevelopBase.Common;
namespace PurocumentLib.Message.Handler
{
    public class AddGoodsHandler : HandlerGeneric<AddGoodsRequest>
    {
        public AddGoodsHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddGoodsRequest request)
        {
            try
            {
                if(request==null)
                {
                    throw new Exception("商品信息无效");
                }
                var service=ServiceProvider.GetService<IGoodsClassService>();
                if(!service.ValidateGoodsClassID(new int[]{request.ClassID}))
                {
                    throw new Exception("商品类别无效");
                }
                var unitService=ServiceProvider.GetService<IUnitService>();
                if(!unitService.ValidateUnitID(new int[]{request.UnitID}))
                {
                    throw new Exception("计量单位无效");
                }
                var model=new Model.Goods()
                {
                    Name=request.Name,
                    UnitID=request.UnitID,
                    ClassID=request.ClassID
                };
                var goodsService=ServiceProvider.GetService<IGoodsService>();
                goodsService.AddGoods(model);
                return new ResponseBase(){Result=1,ResultInfo=""};
            }
            catch(Exception ex)
            {
                return new ResponseBase(){Result=-1,ResultInfo=ex.Message};
            }
        }
    }
}
