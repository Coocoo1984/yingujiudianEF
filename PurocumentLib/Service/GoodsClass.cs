using System;
using System.Collections.Generic;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Model;
using PurocumentLib.Dbcontext;
using System.Linq;
namespace PurocumentLib.Service
{
    public class GoodsClass : ServiceBase, IGoodsClass
    {
        public GoodsClass(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public bool ValidateGoodsClassID(IEnumerable<int> classIDs)
        {
            if(classIDs==null)
            {
                return false;
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(dbcontext.GoodsClass.Count(c=>classIDs.Contains(c.ID) && c.Disable==false)==classIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
