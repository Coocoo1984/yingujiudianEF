using System;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using System.Collections.Generic;
using System.Linq;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public class UnitService : ServiceBase, IUnitService
    {
        public UnitService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(UnitModel unit)
        {
            if(unit==null)
            {
                throw new ArgumentNullException();
            }
            if(string.IsNullOrEmpty(unit.Name))
            {
                throw new Exception("计量单位无效");
            }
            var entity=new Unit()
            {
                Code=unit.Code,
                Name=unit.Name,
                Desc=unit.Desc,
                Remark=unit.Remark
            };
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            dbcontext.SaveChanges();
        }

        public UnitModel Load(int id)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity= dbcontext.Units.SingleOrDefault(s=>s.ID==id);
            if(entity==null)
            {
                return null;
            }
            return new UnitModel()
            {
                ID=entity.ID,
                Code=entity.Code,
                Name=entity.Name,
                Desc=entity.Desc,
                Remark=entity.Remark
            };
        }

        public void Update(UnitModel unit)
        {
            if(unit==null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.Units.SingleOrDefault(s=>s.ID==unit.ID);
            if(entity==null)
            {
                throw new Exception("计量单位信息无效");
            }
            entity.Code=unit.Code;
            entity.Name=unit.Name;
            entity.Desc=unit.Desc;
            entity.Remark=unit.Remark;
            dbcontext.Update(entity);
            dbcontext.SaveChanges();
        }

        public bool ValidateUnitID(IEnumerable<int> unitIDs)
        {
            if(unitIDs==null)
            {
                return  false;
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(dbcontext.Units.Count(c=>unitIDs.Contains(c.ID))==unitIDs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
