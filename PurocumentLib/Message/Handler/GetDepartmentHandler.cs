using System;
using DevelopBase.Common;
using PurocumentLib.Service;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class GetDepartmentHandler : HandlerGeneric<GetDepartmentRequest>
    {
        public GetDepartmentHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetDepartmentRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IDepartmentService>();
            var model= service.Load(request.ID);
            return new ResponseGeneric<Model.DepartmentModel>(){Result=1,ResultInfo="",Data=model};
        }
    }
}
