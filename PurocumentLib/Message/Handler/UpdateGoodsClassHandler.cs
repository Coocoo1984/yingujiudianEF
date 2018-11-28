using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using DevelopBase.Common;
namespace PurocumentLib.Message.Handler
{
    public class UpdateGoodsClassHandler : HandlerGeneric<UpdateGoodsClassRequest>
    {
        public UpdateGoodsClassHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateGoodsClassRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            //验证业务类型
            var bizTypeService=ServiceProvider.GetService<IBizTypeService>();
            if(!bizTypeService.ValidateBizTypeID(new int[]{request.BizTypeID}))
            {
                throw new Exception("业务类型无效");
            }
            var goodsClassService=ServiceProvider.GetService<IGoodsClassService>();
            var model=new Model.GoodsClassModel()
            {
                ID=request.ID,
                Code=request.Code,
                Name=request.Name,
                Desc=request.Desc,
                BizTypeID=request.BizTypeID
            };
            goodsClassService.Update(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
