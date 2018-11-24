using System;
using System.Collections.Generic;
using DevelopBase.Services;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public interface IRole
    {
        /// <summary>
        /// 校验RoleID
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        bool ValidateGoodsID(int[] roleID);
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        void AddGoods(Role goods);
        void Update(Role goods);
        Role Load(int id);
        void Disable(IEnumerable<int> ids);
    }
}
