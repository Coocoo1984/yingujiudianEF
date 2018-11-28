using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Model;
using DevelopBase.Common;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class AddGoodsClassHandler : HandlerGeneric<AddGoodsClassRequest>
    {
        public AddGoodsClassHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddGoodsClassRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            if(string.IsNullOrEmpty(request.Code))
            {
                throw new Exception("编码无效");
            }
            var bizTypeService=ServiceProvider.GetService<IBizTypeService>();
            if(!bizTypeService.ValidateBizTypeID(new int[]{request.BizTypeID}))
            {
                throw new Exception("业务类型无效");
            }
            var model=new GoodsClassModel()
            {
                Code=request.Code,
                Name=request.Name,
                Desc=request.Desc,
                BizTypeID=request.BizTypeID
            };
            var goodsClassService=ServiceProvider.GetService<IGoodsClassService>();
            goodsClassService.AddGoodsClass(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
