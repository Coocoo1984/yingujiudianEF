using System;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using System.Collections.Generic;
using System.Linq;
namespace PurocumentLib.Service
{
    public class UnitService : ServiceBase, IUnitService
    {
        public UnitService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public bool ValidateUnitID(IEnumerable<int> unitIDs)
        {
            if(unitIDs==null)
            {
                return  false;
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(dbcontext.Units.Count(c=>unitIDs.Contains(c.ID))==unitIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
