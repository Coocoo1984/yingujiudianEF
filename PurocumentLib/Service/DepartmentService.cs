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
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        public DepartmentService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(DepartmentModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("部门名称无效");
            }
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (string.IsNullOrEmpty(model.Code))
            {
                throw new Exception("部门编码无效");
            }
            if (dbContext.Department.Count(c => c.Code == model.Code) > 0)
            {
                throw new Exception("部门编码无效");
            }
            var entity = new Department()
            {
                Code = model.Code,
                Name = model.Name,
                WechatID = model.WechatID,
                Disable = false,
                Address = model.Address,
                Address1 = model.Address1,
                Tel = model.Tel,
                Tel1 = model.Tel1,
                Mobile = model.Mobile,
                Mobile1 = model.Mobile1
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }

        public DepartmentModel Load(int id)
        {
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbContext.Department.SingleOrDefault(s=>s.ID==id);
            if(entity==null)
            {
                return null;
            }
            var modle = new DepartmentModel()
            {
                ID = entity.ID,
                Name = entity.Name,
                Code = entity.Code,
                Disable = entity.Disable,
                Mobile = entity.Mobile,
                Mobile1 = entity.Mobile1,
                Tel = entity.Tel,
                Tel1 = entity.Tel1,
                Address = entity.Address,
                Address1 = entity.Address1
            };
            return modle;
        }

        public DepartmentModel GetByName(string name)
        {
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbContext.Department.SingleOrDefault(s=>s.Name==name);
            if(entity==null)
            {
                return null;
            }
            var modle=new DepartmentModel()
            {
                ID=entity.ID,
                Name=entity.Name,
                Code=entity.Code,
                Mobile=entity.Mobile,
                Mobile1=entity.Mobile1,
                Tel=entity.Tel,
                Tel1=entity.Tel1,
                Address=entity.Address,
                Address1=entity.Address1
            };
            return modle;
        }

        public void Update(DepartmentModel model)
        {
            var dbContext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbContext.Department.SingleOrDefault(s=>s.ID==model.ID);
            if(entity==null)
            {
                throw new Exception("部门信息不存在");
            }
            
            if(string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("部门名称无效");
            }
            
            if(string.IsNullOrEmpty(model.Code) )
            {
                throw new Exception("部门编码无效");
            }
            if(dbContext.Department.Count(c=>c.Code==model.Code && c.ID!=model.ID)>0)
            {
                throw new Exception("部门编码无效");
            }
            entity.Code=model.Code;
            entity.WechatID=model.WechatID;
            entity.Name=model.Name;
            entity.Tel=model.Tel;
            entity.Tel1=model.Tel1;
            entity.Mobile=model.Mobile;
            entity.Mobile1=model.Mobile1;
            entity.Desc=model.Desc;
            entity.Address=model.Address;
            entity.Address1=model.Address1;
            dbContext.Update(entity);
            dbContext.SaveChanges();
        }

        public bool ValidateDepartment(IEnumerable<int> departmentIDs)
        {
            var dbContext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if (dbContext.Department.Count(c => departmentIDs.Contains(c.ID)) == departmentIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
