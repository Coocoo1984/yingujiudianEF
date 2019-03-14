using System;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using DevelopBase.Common;
using PurocumentLib.Dbcontext;
using PurocumentLib.Model;
namespace PurocumentLib.Message.Handler
{
    public class GetUsrHandler : HandlerGeneric<GetUsrRequest>
    {
        public GetUsrHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(GetUsrRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var service = ServiceProvider.GetService<IUsrService>();
            UsrModel model = null;
            if (request.ID != 0)
            {
                model = service.Load(request.ID);
            }
            else if (!string.IsNullOrWhiteSpace(request.WechatID))
            {
                model = service.Load(request.WechatID);
            }
            return new ResponseGeneric<UsrModel>() { Result = 1, ResultInfo = "", Data = model };
        }
    }
}
