using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;

namespace PurocumentLib.Message.Handler
{
    public class UpdateGoodsHandler : HandlerGeneric<UpdateGoodsRequest>
    {
        public UpdateGoodsHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateGoodsRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            //检查计量单位
            var unitService=ServiceProvider.GetService<IUnitService>();
            if(!unitService.ValidateUnitID(new int[]{request.UnitID}))
            {
                throw new Exception("计量单位无效");
            }
            var goodsClassService=ServiceProvider.GetService<IGoodsClassService>();
            if(!goodsClassService.ValidateGoodsClassID(new int[]{request.ClassID}))
            {
                throw new Exception("商品类别无效");
            }
            var model=new Model.Goods()
            {
                ID=request.ID,
                Name=request.Name,
                Specification=request.Specification,
                Desc=request.Desc,
                UnitID=request.UnitID,
                ClassID=request.ClassID
            };
            var goodsService=ServiceProvider.GetService<IGoodsService>();
            goodsService.Update(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
