using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Model;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class UpdateUsrHandler : HandlerGeneric<UpdateUsrRequest>
    {
        public UpdateUsrHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateUsrRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new Exception("用户名称无效");
            }
            var model = new UsrModel()
            {
                ID = request.ID,
                WechatID = request.WechatID,
                Code = request.Code,
                Name = request.Name,
                Addr = request.Addr,
                Addr1 = request.Addr1,
                Tel = request.Tel,
                Tel1 = request.Tel1,
                Mobile = request.Mobile,
                Mobile1 = request.Mobile1,
                DepartmentID = request.DepartmentID,
                VendorID = request.VendorID,
                RoleID = request.RoleID
            };
            var service = ServiceProvider.GetService<IUsrService>();
            service.Update(model);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
