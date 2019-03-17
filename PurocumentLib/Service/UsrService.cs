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
    public class UsrService : ServiceBase, IUsrService
    {
        public UsrService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(UsrModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("用户名称无效");
            }
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("用户编码无效");
            }
            if (dbContext.Usr.Count(c => c.Code == model.Code) > 0)
            {
                throw new Exception("用户编码无效");
            }
            var entity = new Usr()
            {
                WechatID = model.WechatID,
                Code = model.Code,
                Name = model.Name,
                Desc = model.Desc,
                Tel = model.Tel,
                Tel1 = model.Tel1,
                Mobile = model.Mobile,
                Mobile1 = model.Mobile1,
                Addr = model.Addr,
                Addr1 = model.Addr1,
                DepartmentID = model.DepartmentID,
                VendorID = model.VendorID,
                RoleID = model.RoleID,
                Disable = model.Disable
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public UsrModel Load(int id)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbContext.Usr.SingleOrDefault(s => s.ID == id);
            if (entity == null)
            {
                return null;
            }
            var model = new UsrModel()
            {
                ID = entity.ID,
                WechatID = entity.WechatID,
                Code = entity.Code,
                Name = entity.Name,
                Desc = entity.Desc,
                Tel = entity.Tel,
                Tel1 = entity.Tel1,
                Mobile = entity.Mobile,
                Mobile1 = entity.Mobile1,
                Addr = entity.Addr,
                Addr1 = entity.Addr1,
                DepartmentID = entity.DepartmentID,
                VendorID = entity.VendorID,
                RoleID = entity.RoleID,
                Disable = entity.Disable
            };
            return model;
        }

        public UsrModel Load(string wecharid)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbContext.Usr.SingleOrDefault(s => s.WechatID == wecharid);
            if (entity == null)
            {
                return null;
            }
            var model = new UsrModel()
            {
                ID = entity.ID,
                WechatID = entity.WechatID,
                Code = entity.Code,
                Name = entity.Name,
                Desc = entity.Desc,
                Tel = entity.Tel,
                Tel1 = entity.Tel1,
                Mobile = entity.Mobile,
                Mobile1 = entity.Mobile1,
                Addr = entity.Addr,
                Addr1 = entity.Addr1,
                DepartmentID = entity.DepartmentID,
                VendorID = entity.VendorID,
                RoleID = entity.RoleID,
                Disable = entity.Disable
            };
            return model;
        }

        public void Update(UsrModel model)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbContext.Usr.SingleOrDefault(s => s.ID == model.ID);
            if (entity == null)
            {
                throw new Exception("用户信息不存在");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("用户名称无效");
            }

            if (string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("用户编码无效");
            }
            if (dbContext.Usr.Count(c => c.Code == model.Code && c.ID != model.ID) > 0)
            {
                throw new Exception("用户编码无效");
            }
            entity.Code = model.Code;
            entity.WechatID = model.WechatID;
            entity.Name = model.Name;
            entity.Tel = model.Tel;
            entity.Tel1 = model.Tel1;
            entity.Mobile = model.Mobile;
            entity.Mobile1 = model.Mobile1;
            entity.RoleID = model.RoleID;
            entity.DepartmentID = model.DepartmentID;
            entity.Desc = model.Desc;
            entity.VendorID = model.VendorID;
            entity.Disable = model.Disable;
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }

        public void Disable(IEnumerable<int> ids)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (ids.Count() == 0)
            {
                return;
            }
            var usrs = dbContext.Usr.Where(w => ids.Contains(w.ID)).ToList();
            foreach (var item in usrs)
            {
                item.Disable = true;
            }
            dbContext.UpdateRange(usrs);
            dbContext.SaveChanges();
        }

        public bool ValidateUsrID(IEnumerable<int> usrIDs)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbContext.Usr.Count(c => usrIDs.Contains(c.ID)) == usrIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
