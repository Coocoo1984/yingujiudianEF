using System;
using System.Collections.Generic;
using DevelopBase.Services;
using PurocumentLib.Model;
namespace PurocumentLib.Service
{
    public interface IUnitService:IService
    {
        void Add(UnitModel unit);
        void Update(UnitModel unit);
        UnitModel Load(int id);
        bool ValidateUnitID(IEnumerable<int> unitIDs);
    }
}
