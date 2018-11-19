using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IGoodsService:IService
    {
        /// <summary>
        /// 校验GoodsID
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        bool ValidateGoodsID(int[] goodsID);
    }
}
