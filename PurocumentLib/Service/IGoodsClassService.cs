using System;
using System.Collections.Generic;
using DevelopBase.Services;
using PurocumentLib.Model;
namespace PurocumentLib.Service
{
    public interface IGoodsClassService:IService
    {
        bool ValidateGoodsClassID(IEnumerable<int> classIDs);
        void AddGoodsClass(GoodsClassModel model);
        //检查编码是否存在
        bool CodeExists(string code);
        void Update(GoodsClassModel model);
        GoodsClassModel Load(int id);
        void Disable(int id);
    }
}
