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
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            IEnumerable<RsPermission> RsPermissionList = null;
            if (string.IsNullOrWhiteSpace(StrRequestName))
            {
                RsPermissionList = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID)).ToList();
            }
            else
            {
                switch(StrRequestName)
                {
                    case "UpdatePurocumentPlanRequest":
                    case "CreatePurocumentPlanRequest": RsPermissionList = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(11)).ToList();break;
                    case "AddQuoteRequest":
                    case "UpdateQuoteRequest": RsPermissionList = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(10)).ToList(); break;
                    case "AddChargeBackRequest":
                    case "UpdateChargeBackRequest": RsPermissionList = dbContext.RsPermission.Include(i => i.Permission).Where(w => w.UsrWechatId.Equals(StrWechatID) && w.PermissionId.Equals(12)).ToList(); break;
                }
                
            }
            

            foreach(var item in RsPermissionList)
            {

            }

            return result;
        }
    }
}
