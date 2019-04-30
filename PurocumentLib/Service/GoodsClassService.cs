using System;
using System.Collections.Generic;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Model;
using PurocumentLib.Dbcontext;
using System.Linq;
using PurocumentLib.Entity;
namespace PurocumentLib.Service
{
    public class GoodsClassService : ServiceBase, IGoodsClassService
    {
        public GoodsClassService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void AddGoodsClass(GoodsClassModel model)
        {
            if(model==null)
            {
                throw new ArgumentNullException();
            }
            if(string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("商品分类名称不能为空");
            }
            if(string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("商品编码无效");
            }
            if(CodeExists(model.Code))
            {
                throw new Exception("商品编码无效");
            }
            var entity=new GoodsClass()
            {
                Code=model.Code,
                Name=model.Name,
                Desc=model.Desc,
                Disable=false,
                BizTypeID=model.BizTypeID
            };
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            dbcontext.SaveChanges();
        }

        public bool CodeExists(string code)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(dbcontext.GoodsClass.Count(c=>c.Code==code && c.Disable==false)>0)
            {
                return true;
            }
            return false;
        }

        public void Disable(int id)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.GoodsClass.SingleOrDefault(s=>s.ID==id);
            if(entity!=null)
            {
                entity.Disable=true;
                dbcontext.Update(entity);
                dbcontext.SaveChanges();
            }
        }

        public GoodsClassModel Load(int id)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.GoodsClass.SingleOrDefault(s=>s.ID==id);
            if(entity==null)
            {
                return null;
            }
            return new GoodsClassModel()
            {
                ID=entity.ID,
                Code=entity.Code,
                Name=entity.Name,
                Disable=entity.Disable,
                Desc=entity.Desc
            };
        }

        public void Update(GoodsClassModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("编码无效");
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("类别名称无效");
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            //验证编码
            if (dbcontext.GoodsClass.Count(c => c.Code == model.Code && c.ID != model.ID) > 0)
            {
                throw new Exception("编码无效已使用");
            }
            var entity = dbcontext.GoodsClass.SingleOrDefault(s => s.ID == model.ID);
            if (entity == null)
            {
                throw new Exception("类别信息不存在");
            }
            entity.Code = model.Code;
            entity.Name = model.Name;
            entity.Desc = model.Desc;
            entity.BizTypeID = model.BizTypeID;
            dbcontext.Update(entity);
            dbcontext.SaveChanges();
        }

        public bool ValidateGoodsClassID(IEnumerable<int> classIDs)
        {
            if (classIDs == null)
            {
                return false;
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbcontext.GoodsClass.Count(c => classIDs.Contains(c.ID) && c.Disable == false) == classIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
