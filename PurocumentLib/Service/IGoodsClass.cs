using System;
using System.Collections.Generic;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IGoodsClass:IService
    {
        bool ValidateGoodsClassID(IEnumerable<int> classIDs);
    }
}
