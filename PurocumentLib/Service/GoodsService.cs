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

        public void Disable(IEnumerable<int> ids)
        {
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(ids.Count()==0)
            {
                return;
            }
            var goods=dbContext.Goods.Where(w=>ids.Contains(w.ID)).ToList();
            foreach(var item in goods)
            {
                item.Disable=true;
            }
            dbContext.UpdateRange(goods);
            dbContext.SaveChanges();
        }

        public Model.Goods Load(int id)
        {
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbContext.Goods.SingleOrDefault(s=>s.ID==id);
            return new Model.Goods()
            {
                ID=entity.ID,
                Name=entity.Name,
                Desc=entity.Desc,
                Disable=entity.Disable,
                UnitID=entity.UnitID,
                Specification=entity.Specification,
                ClassID=entity.ClassID
            };
        }

        public void Update(Model.Goods goods)
        {
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbContext.Goods.SingleOrDefault(f=>f.ID==goods.ID);
            if(entity==null)
            {
                throw new Exception("商品信息不存在");
            }
            if(entity.Disable)
            {
                throw new Exception("商品禁用无法修改");
            }
            if(string.IsNullOrEmpty(goods.Name))
            {
                throw new Exception("商品名称无效");
            }
            entity.Name=goods.Name;
            entity.Desc=goods.Desc;
            entity.Specification=goods.Specification;
            entity.ClassID=goods.ClassID;
            entity.UnitID=goods.UnitID;
            dbContext.Update(entity);
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
