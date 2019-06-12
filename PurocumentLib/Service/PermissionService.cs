using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using DevelopBase.Common;
using PurocumentLib.Dbcontext;
using System.Linq;
using Model = PurocumentLib.Model;
using PurocumentLib.Entity;
using Microsoft.EntityFrameworkCore;

namespace PurocumentLib.Service
{
    public class PermissionService : ServiceBase, IPermissionService
    {
        public PermissionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public bool CheckPermission(string StrWechatID, string StrRequestName)
        {
            bool result = false;
            if(StrWechatID == "HeYan" || StrWechatID == "LiuQingXin" || StrWechatID == "YangJian")
            {
                return true;
            }

            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            RsPermission RsPermission = null;
            if (string.IsNullOrWhiteSpace(StrRequestName))
            {
                result = true;
            }
            else
            {
                switch(StrRequestName)
                {
                    case "UpdatePurocumentPlanRequest":
                    case "CreatePurocumentPlanRequest": RsPermission = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(11)).SingleOrDefault();break;
                    case "AddQuoteRequest":
                    case "UpdateQuoteRequest": RsPermission = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(10)).SingleOrDefault(); break;
                    case "AddChargeBackRequest":
                    case "ChargeBackFinishRequest": RsPermission = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(12)).SingleOrDefault(); break;
                    case "ChargeBackAuditRequest": RsPermission = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(7)).SingleOrDefault(); break;
                }
                if(RsPermission?.Id > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
