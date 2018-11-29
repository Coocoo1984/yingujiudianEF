using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Model;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
using System.Linq;

namespace PurocumentLib.Message.Handler
{
    public class UpdateVendorHandler : HandlerGeneric<UpdateVendorRequest>
    {
        public UpdateVendorHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateVendorRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new Exception("供应商名称无效");
            }

            var model = new VendorModel()
            {
                RsVendors = from item in request.GoodsClassIDs
                            select new RsVendorModel
                            {
                                VendorID = request.ID,
                                GoodsClassID = item
                            },
                Code = request.Code,
                Name = request.Name,
                Address = request.Address,
                Address1 = request.Address1,
                Tel = request.Tel,
                Tel1 = request.Tel1,
                Mobile = request.Mobile,
                Mobile1 = request.Mobile1,
                ID = request.ID
            };
            var service = ServiceProvider.GetService<IVendorService>();
            service.Update(model);

            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
