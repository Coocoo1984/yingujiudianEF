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
    public interface IUsrService: IService
    {
        /// <summary>
        /// 校验用户ID
        /// </summary>
        /// <param name="usrID"></param>
        /// <returns></returns>
        bool ValidateUsrID(IEnumerable<int> ids);
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="usr">用户对象</param>
        /// <returns></returns>
        void Add(UsrModel usr);
        void Update(UsrModel usr);
        UsrModel Load(int id);
        UsrModel Load(string wechatid);
        void Disable(IEnumerable<int> ids);
    }
}
