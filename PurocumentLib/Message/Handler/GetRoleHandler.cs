using System;
using DevelopBase.Common;
using PurocumentLib.Service;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class GetRoleHandler : HandlerGeneric<GetRoleRequest>
    {
        public GetRoleHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetRoleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IRoleService>();
            var model = service.Load(request.ID);
            return new ResponseGeneric<Model.RoleModel>() { Result = 1, ResultInfo = "", Data = model };
        }
    }
}
