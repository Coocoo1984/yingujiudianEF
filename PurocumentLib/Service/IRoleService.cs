using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public interface IRoleService : IService
    {
        /// <summary>
        /// 校验RoleID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        bool ValidateRoleID(IEnumerable<int> departmentIDs);
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="goods">商品信息</param>
        /// <returns></returns>
        void Add(RoleModel role);
        void Update(RoleModel role);
        RoleModel Load(int id);
        //void Disable(IEnumerable<int> ids);
    }
}
