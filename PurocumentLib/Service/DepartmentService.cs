using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
namespace PurocumentLib.Service
{
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        public DepartmentService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
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
