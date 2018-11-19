using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;

namespace PurocumentLib.Service
{
    public interface IDepartmentService:IService
    {
        bool ValidateDepartment(IEnumerable<int> departmentIDs);
    }
}
