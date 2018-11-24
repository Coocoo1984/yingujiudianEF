using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Service;
using PurocumentLib.Message.Request;
namespace PurocumentLib.Message.Handler
{
    public class GetUnitHandler : HandlerGeneric<GetUnitRequest>
    {
        public GetUnitHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetUnitRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IUnitService>();
            var model=service.Load(request.ID);
            return new ResponseGeneric<Model.UnitModel>(){Result=1,ResultInfo="",Data=model};
        }
    }
}
