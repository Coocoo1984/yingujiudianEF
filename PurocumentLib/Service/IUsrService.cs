using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public interface IUsrService: IService
    {
        /// <summary>
        /// 校验用户ID
        /// </summary>
        /// <param name="usrID"></param>
        /// <returns></returns>
        bool ValidateUsrID(int[] usrID);
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="usr">用户对象</param>
        /// <returns></returns>
        void AddUsr(Usr usr);
        void Update(Usr usr);
        Goods Load(int id);
        void Disable(IEnumerable<int> ids);
    }
}
