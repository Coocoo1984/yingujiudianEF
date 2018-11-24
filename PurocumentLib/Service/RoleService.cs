using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(Model.Role model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("角色名称无效");
            }
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("角色编码无效");
            }
            if (dbContext.Role.Count(c => c.Code == model.Code) > 0)
            {
                throw new Exception("角色编码无效");
            }
            var entity = new Entity.Role()
            {
                WechatGroupID = model.WechatGroupID,
                Code = model.Code,
                Name = model.Name
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public Model.Role Load(int id)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbContext.Department.SingleOrDefault(s => s.ID == id);
            if (entity == null)
            {
                return null;
            }
            var model = new Model.Role()
            {
                ID = entity.ID,
                WechatGroupID = entity.WechatID,
                Name = entity.Name,
                Code = entity.Code
            };
            return model;
        }

        public void Update(Model.Role model)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbContext.Role.SingleOrDefault(s => s.ID == model.ID);
            if (entity == null)
            {
                throw new Exception("角色信息不存在");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("角色名称无效");
            }

            if (string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("角色编码无效");
            }
            if (dbContext.Department.Count(c => c.Code == model.Code && c.ID != model.ID) > 0)
            {
                throw new Exception("角色编码无效");
            }
            entity.Code = model.Code;
            entity.WechatGroupID = model.WechatGroupID;
            entity.Name = model.Name;
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }

        public bool ValidateRoleID(IEnumerable<int> roleIDs)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbContext.Department.Count(c => roleIDs.Contains(c.ID)) == roleIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
