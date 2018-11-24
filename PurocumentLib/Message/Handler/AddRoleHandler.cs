using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Model;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class AddRoleHandler : HandlerGeneric<AddRoleRequest>
    {
        public AddRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddRoleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var model = new RoleModel()
            {
                WechatGroupID = request.WechatGroupID,
                Code = request.Code,
                Name = request.Name
            };
            var service = ServiceProvider.GetService<IRoleService>();
            service.Add(model);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
