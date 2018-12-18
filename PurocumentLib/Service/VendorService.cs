using System;
using DevelopBase.Services;
using PurocumentLib.Model;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using DevelopBase.Common;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PurocumentLib.Service
{
    public class VendorService : ServiceBase, IVendorService
    {
        public VendorService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(VendorModel vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(vendor.Name))
            {
                throw new Exception("供应商名称无效");
            }
            var entity = new Vendor()
            {
                //RsVendors = model.RsVendors,
                Code = vendor.Code,
                Name = vendor.Name,
                Address = vendor.Address,
                Address1 = vendor.Address1,
                Mobile = vendor.Mobile,
                Mobile1 = vendor.Mobile1,
                Tel = vendor.Tel,
                Tel1 = vendor.Tel1,
                Desc = vendor.Desc,
                Disable = vendor.Disable,
                Remark = vendor.Remark
            };
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            var rsvendors = from a in vendor.RsVendors
                            join b in dbcontext.GoodsClass on a.GoodsClassID equals b.ID
                            select new Entity.RsVendor()
                            {
                                BizTypeID = b.BizTypeID,
                                GoodsClassID = a.GoodsClassID,
                                Vendor = entity
                            };
            dbcontext.AddRange(rsvendors);
            dbcontext.SaveChanges();
        }

        public void Disable(IEnumerable<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException();
            }
            if (ids.Count() == 0)
            {
                return;
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entities = dbcontext.Vendor.Where(w => ids.Contains(w.ID)).ToList();
            foreach (var entity in entities)
            {
                entity.Disable = true;
            }
            dbcontext.UpdateRange(entities);
            dbcontext.SaveChanges();
        }

        public VendorModel Load(int id)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbcontext.Vendor.Include(i => i.RsVendors).SingleOrDefault(s => s.ID == id);
            //dbcontext.Vendor.Include(e => e.RsVendors).SingleOrDefault(s => s.Code == id);
            if (entity == null)
            {
                return null;
            }
            return new VendorModel()
            {
                ID = entity.ID,
                Code = entity.Code,
                Name = entity.Name,
                Tel = entity.Tel,
                Tel1 = entity.Tel1,
                Mobile = entity.Mobile,
                Mobile1 = entity.Mobile1,
                Address = entity.Address,
                Address1 = entity.Address1,
                RsVendors = entity.RsVendors.Select(en => new RsVendorModel
                {
                    ID = en.ID,
                    VendorID = en.VendorID,
                    BizTypeID = en.BizTypeID,
                    GoodsClassID = en.GoodsClassID,
                    GoodsID = en.GoodsID ?? 0
                })
            };
        }

        public VendorModel GetByName(string name)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbcontext.Vendor.Include(e => e.RsVendors).FirstOrDefault(s => s.Name == name);
            if (entity == null)
            {
                return null;
            }
            return new VendorModel()
            {
                ID = entity.ID,
                Code = entity.Code,
                Name = entity.Name,
                Tel = entity.Tel,
                Tel1 = entity.Tel1,
                Mobile = entity.Mobile,
                Mobile1 = entity.Mobile1,
                Address = entity.Address,
                Address1 = entity.Address1,
                RsVendors = entity.RsVendors.Select(en => new RsVendorModel
                {
                    ID = en.ID,
                    VendorID = en.VendorID,
                    BizTypeID = en.BizTypeID,
                    GoodsClassID = en.GoodsClassID,
                    GoodsID = en.GoodsID ?? 0
                })
            };
        }

        public void Update(VendorModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity = dbcontext.Vendor.Include(i => i.RsVendors).SingleOrDefault(s => s.ID == model.ID);
            if (entity == null)
            {
                throw new Exception("供应商信息无效");
            }
            if (entity.Disable == true)
            {
                throw new Exception("供应商已禁用不能修改");
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("供应商名称无效");
            }
            entity.Name = model.Name;
            entity.Mobile = model.Mobile;
            entity.Mobile1 = model.Mobile1;
            entity.Tel = model.Tel;
            entity.Tel1 = model.Tel1;
            entity.Address = model.Address;
            entity.Address1 = model.Address1;
            entity.Desc = model.Desc;
            entity.Remark = model.Remark;
            entity.Code = model.Code;

            dbcontext.Update(entity);

            //数据库已经存在的当前供应商的商品类目关联记录(ID、VendorID、GoodsClassID)
            var savedRsVendors = entity.RsVendors;
            //需要新保存的的关联记录(有VendorID、GoodsClassID)
            var newRsVendors = model.RsVendors;

            //只需要删除的货物类型关联
            var forDeleteRsVendors = from a in savedRsVendors
                                     where !(newRsVendors.Select(s => s.GoodsClassID).Contains(a.GoodsClassID))
                                     select a;
            dbcontext.RemoveRange(forDeleteRsVendors);

            //只需要新增的货物类型关联
            var forAddRsVendors = from b in newRsVendors
                                  where !(forDeleteRsVendors.Select(s => s.GoodsClassID).Contains(b.GoodsClassID))
                                  select new RsVendor
                                  {
                                      VendorID = entity.ID,
                                      BizTypeID = b.BizTypeID,
                                      GoodsClassID = b.GoodsClassID
                                  };
            dbcontext.AddRange(forAddRsVendors);

            dbcontext.SaveChanges();
        }
    }
}
/* 
public int VendorID { get; set; }
        public int BizTypeID { get; set; }
        public int GoodsClassID { get; set; }
        public int? GoodsID { get; set; }

        public Vendor Vendor { get; set; }
    } */
