using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;

namespace PurocumentLib.Message.Handler
{
    public class UpdateRoleHandler : HandlerGeneric<UpdateRoleRequest>
    {
        public UpdateRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateRoleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var model = new RoleModel()
            {
                ID = request.ID,
                WechatGroupID = request.WechatGroupID,
                Code = request.Code,
                Name = request.Name
            };
            var service = ServiceProvider.GetService<IRoleService>();
            service.Update(model);
            return new ResponseBase() { Result = 1, ResultInfo = "" };
        }
    }
}
