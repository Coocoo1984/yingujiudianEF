using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using PurocumentLib.Model;
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
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        void AddGoods(Goods goods);
        void Update(Goods goods);
        Goods Load(int id);
        void Disable(IEnumerable<int> ids);
    }
}
