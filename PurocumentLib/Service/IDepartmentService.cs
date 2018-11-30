using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using PurocumentLib.Model;
namespace PurocumentLib.Service
{
    public interface IDepartmentService:IService
    {
        bool ValidateDepartment(IEnumerable<int> departmentIDs);
        void Add(DepartmentModel model);
        void Update(DepartmentModel model);
        DepartmentModel Load(int id);
        DepartmentModel GetByName(string name);
    }
}
