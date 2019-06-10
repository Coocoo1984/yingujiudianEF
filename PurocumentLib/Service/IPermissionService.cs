using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
using PurocumentLib.Model;
namespace PurocumentLib.Service
{
    public interface IPermissionService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StrWechatID">用户企业微信ID</param>
        /// <param name="StrRequestName">Request类名</param>
        /// <returns></returns>
        bool CheckPermission(string StrUsrWechatID,string StrRequestName);

    }
}