﻿using System;
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
        void Add(Usr usr);
        void Update(Usr usr);
        Usr Load(int id);
        void Disable(IEnumerable<int> ids);
    }
}
