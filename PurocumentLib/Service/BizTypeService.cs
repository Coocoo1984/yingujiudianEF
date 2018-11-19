using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using System.Linq;
namespace PurocumentLib.Service
{
    public class BizTypeService : ServiceBase, IBizTypeService
    {
        public BizTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public bool ValidateBizTypeID(IEnumerable<int> bizTypeIDs)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.BizTypes.Count(c => c.Disable == false && bizTypeIDs.Contains(c.ID)) == bizTypeIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
