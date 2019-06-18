using System;
using PurocumentLib.Service;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using DevelopBase.Services;

namespace PurocumentLib.Message.Handler
{
    public class ChargeBackVerdorComfirmHandler : HandlerGeneric<ChargeBackVerdorComfirmRequest>
    {
        public ChargeBackVerdorComfirmHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ChargeBackVerdorComfirmRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }

                var permissionService = ServiceProvider.GetService<IPermissionService>();
                if (!permissionService.CheckPermission(request.WechatID, "ChargeBackVerdorComfirmRequest"))
                {
                    return new ResponseBase() { Result = -1, ResultInfo = ServiceBase.NoPermissionString };
                }

                var service = ServiceProvider.GetService<IChargeBackService>();
                service.VendorComfirm(request.ChargeBackID, request.UserID, request.Result, request.Desc);
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }
            catch (Exception ex)
            {
                return new ResponseBase() { Result = -1, ResultInfo = ex.Message };
            }
        }
    }
}