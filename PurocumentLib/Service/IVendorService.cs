using System;
using DevelopBase.Message;
using DevelopBase.Services;
using PurocumentLib.Model;
using System.Collections.Generic;
namespace PurocumentLib.Service
{
    public interface IVendorService:IService
    {
        void Add(VendorModel model);
        void Update(VendorModel model);
        VendorModel Load(int id);
        VendorModel GetByName(string name);
        void Disable(IEnumerable<int> ids);
    }
}
