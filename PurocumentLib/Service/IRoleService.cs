using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
//using PurocumentLib.Entity;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public interface IRoleService
    {
        /// <summary>
        /// 校验RoleID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        bool ValidateRoleID(int[] roleID);
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        void Add(Role role);
        void Update(Role role);
        Role Load(int id);
        void Disable(IEnumerable<int> ids);
    }
}
