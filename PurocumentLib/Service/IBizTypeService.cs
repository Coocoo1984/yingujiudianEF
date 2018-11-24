using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Services;
namespace PurocumentLib.Service
{
    public interface IBizTypeService:IService
    {
        /// <summary>
        /// 验证业务类型
        /// </summary>
        /// <param name="BizTypeID"></param>
        /// <returns></returns>
        bool ValidateBizTypeID(IEnumerable<int> BizTypeID);
        
    }
}
