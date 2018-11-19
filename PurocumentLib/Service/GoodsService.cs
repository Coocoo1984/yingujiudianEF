using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using DevelopBase.Common;
using PurocumentLib.Dbcontext;
using System.Linq;
using Model=PurocumentLib.Model;
using PurocumentLib.Entity;

namespace PurocumentLib.Service
{
    public class GoodsService : ServiceBase, IGoodsService
    {
        public GoodsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void AddGoods(Model.Goods goods)
        {
            if(goods==null)
            {
                throw new Exception("商品信息无效");
            }
            if(string.IsNullOrEmpty(goods.Name))
            {
                throw new Exception("商品名称无效");
            }
            var entity=new Entity.Goods()
            {
                Name=goods.Name,
                ClassID=goods.ClassID,
                Disable=false,
                UnitID=goods.UnitID
            };
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbContext.Add(entity);
            dbContext.SaveChanges();
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
