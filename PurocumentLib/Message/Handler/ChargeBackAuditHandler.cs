using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
using PurocumentLib.Service;
using System.Linq;
using PurocumentLib.Model;
using DevelopBase.Services;

namespace PurocumentLib.Message.Handler
{
    public class ChargeBackAuditHandler : HandlerGeneric<ChargeBackAuditRequest>
    {
        public ChargeBackAuditHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ChargeBackAuditRequest request)
        {
            try
            {
                var permissionService = ServiceProvider.GetService<IPermissionService>();
                if(!permissionService.CheckPermission(request.WechatID, "ChargeBackAuditRequest"))
                {
                    return new ResponseBase() { Result = -1, ResultInfo = ServiceBase.NoPermissionString };
                }

                var service = ServiceProvider.GetService<IChargeBackService>();
                service.Audit(request.ChargeBackID, request.UserID, request.Result, request.Desc);
                return new ResponseBase() { Result = 1, ResultInfo = "" };
            }
            catch (Exception ex)
            {
                return new ResponseBase() { Result = -1, ResultInfo = ex.Message };
            }
        }
    }
}
