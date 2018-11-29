using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;
using System.Collections.Generic;

namespace PurocumentLib.Message.Handler
{
    public class AddVendorHandler : HandlerGeneric<AddVendorRequest>
    {
        public AddVendorHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddVendorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new Exception("供应商名称无效");
            }
            ////var bizService = ServiceProvider.GetService<IBizTypeService>();
            ////if (!bizService.ValidateBizTypeID(request.BizTypIDs))
            ////{
            ////    throw new Exception("业务类型无效");
            ////}
            var goodsClassService = ServiceProvider.GetService<IGoodsClassService>();
            if (!goodsClassService.ValidateGoodsClassID(request.GoodsClassIDs))
            {
                throw new Exception("商品信息无效");
            }

            var rsVendors = new List<RsVendorModel>();
            foreach (int goodsClassID in request.GoodsClassIDs)
            {
                rsVendors.Add(new RsVendorModel
                {
                    GoodsClassID = goodsClassID,
                    BizTypeID = 1,
                });
            }

            var model = new VendorModel()
            {
                RsVendors = rsVendors,
                Code = request.Code,
                Name = request.Name,
                Tel = request.Tel,
                Tel1 = request.Tel,
                Mobile = request.Mobile,
                Mobile1 = request.Mobile1,
                Address = request.Address,
                Address1 = request.Address1,
                Desc = request.Desc,
                Remark = request.Remark,
                Disable = false

            };
            var service = ServiceProvider.GetService<IVendorService>();
            service.Add(model);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
