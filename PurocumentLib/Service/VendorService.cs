using System;
using DevelopBase.Services;
using PurocumentLib.Model;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using DevelopBase.Common;
using System.Linq;
using System.Collections.Generic;

namespace PurocumentLib.Service
{
    public class VendorService : ServiceBase, IVendorService
    {
        public VendorService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(VendorModel model)
        {
            if(model==null)
            {
                throw new ArgumentNullException();
            }
            if(string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("供应商名称无效");
            }
            var entity=new Vendor()
            {
                Code=model.Code,
                Name=model.Name,
                Address=model.Address,
                Address1=model.Address1,
                Mobile=model.Mobile,
                Mobile1=model.Mobile1,
                Tel=model.Tel,
                Tel1=model.Tel1,
                Desc=model.Desc,
                Disable=model.Disable,
                Remark=model.Remark
            };
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            dbcontext.SaveChanges();
        }

        public void Disable(IEnumerable<int> ids)
        {
            if(ids==null)
            {
                throw new ArgumentNullException();
            }
            if(ids.Count()==0)
            {
                return;
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entities=dbcontext.Vendor.Where(w=>ids.Contains(w.ID)).ToList();
            foreach(var entity in entities)
            {
                entity.Disable=true;
            }
            dbcontext.UpdateRange(entities);
            dbcontext.SaveChanges();
        }

        public void Update(VendorModel model)
        {
            if(model==null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.Vendor.SingleOrDefault(s=>s.ID==model.ID);
            if(entity==null)
            {
                throw new Exception("供应商信息无效");
            }
            if(entity.Disable==true)
            {
                throw new Exception("供应商已禁用不能修改");
            }
            if(string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("供应商名称无效");
            }
            entity.Name=model.Name;
            entity.Mobile=model.Mobile;
            entity.Mobile1=model.Mobile1;
            entity.Tel=model.Tel;
            entity.Tel1=model.Tel1;
            entity.Address=model.Address;
            entity.Address1=model.Address1;
            entity.Desc=model.Desc;
            entity.Remark=model.Remark;
            entity.Code=model.Code;
            
            dbcontext.Update(entity);
            dbcontext.SaveChanges();
        }
    }
}
