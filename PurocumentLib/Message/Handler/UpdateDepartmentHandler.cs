using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class UpdateDepartmentHandler : HandlerGeneric<UpdateDepartmentRequest>
    {
        public UpdateDepartmentHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(UpdateDepartmentRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var model=new DepartmentModel()
            {
                ID=request.ID,
                WechatID=request.WechatID,
                Code=request.Code,
                Name=request.Name,
                Address=request.Address,
                Address1=request.Address1,
                Mobile=request.Mobile,
                Mobile1=request.Mobile1,
                Tel=request.Tel,
                Tel1=request.Tel1
            };
            var service=ServiceProvider.GetService<IDepartmentService>();
            service.Update(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
