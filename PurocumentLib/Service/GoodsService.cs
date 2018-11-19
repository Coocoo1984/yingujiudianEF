using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using DevelopBase.Common;
using PurocumentLib.Dbcontext;
using System.Linq;
namespace PurocumentLib.Service
{
    public class GoodsService : ServiceBase, IGoodsService
    {
        public GoodsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public bool ValidateGoodsID(int[] goodsID)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbContext.Goods.Count(c => goodsID.Contains(c.ID) && c.Disable == false)==goodsID.Count())
            {
                return true;
            }
            return false;
        }
    }
}
