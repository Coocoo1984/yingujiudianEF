using System;
using System.Collections.Generic;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IUnitService:IService
    {
        bool ValidateUnitID(IEnumerable<int> unitIDs);
    }
}
