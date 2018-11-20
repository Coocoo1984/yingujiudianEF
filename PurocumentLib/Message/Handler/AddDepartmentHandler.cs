using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Model;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class AddDepartmentHandler : HandlerGeneric<AddDepartmentRequest>
    {
        public AddDepartmentHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddDepartmentRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var model=new DepartmentModel()
            {
                WechatID=request.WechatID,
                Code=request.Code,
                Name=request.Name,
                Address=request.Address,
                Address1=request.Address1,
                Tel=request.Tel,
                Tel1=request.Tel1,
                Mobile=request.Mobile,
                Mobile1=request.Mobile1
            };
            var service=ServiceProvider.GetService<IDepartmentService>();
            service.Add(model);
            return new ResponseBase(){Result=1,ResultInfo=""};
        }
    }
}
