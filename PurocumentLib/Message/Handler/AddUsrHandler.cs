using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Model;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class AddUsrHandler : HandlerGeneric<AddUsrRequest>
    {
        public AddUsrHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddUsrRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var model = new UsrModel()
            {
                WechatID = request.WechatID,
                Code = request.Code,
                Name = request.Name,
                Desc = request.Desc,
                Tel = request.Tel,
                Tel1 = request.Tel1,
                Mobile = request.Mobile,
                Mobile1 = request.Mobile1,
                Addr = request.Addr,
                Addr1 = request.Addr1,
                DepartmentID = request.DepartmentID,
                VendorID = request.VendorID,
                RoleID = request.RoleID,
                Disable = request.Disable
            };
            var service = ServiceProvider.GetService<IUsrService>();
            service.Add(model);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
