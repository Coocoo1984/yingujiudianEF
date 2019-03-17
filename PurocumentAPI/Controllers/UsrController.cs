using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using DevelopBase.Message;

namespace PurocumentAPI.Controllers
{
    public class UsrController : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;
        public UsrController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        //用户新增
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddUsrRequest request)
        {
            try
            {
                var response = await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase() { Result = -1, ResultInfo = ex.Message });
            }
        }
        //用户修改
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateUsrRequest request)
        {
            try
            {
                var response = await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase() { Result = -1, ResultInfo = ex.Message });
            }

        }
        //获取用户信息
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetUsrRequest()
            {
                ID = id
            };
            try
            {
                var response = await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase() { Result = -1, ResultInfo = ex.Message });
            }
        }

        //获取用户信息
        public async Task<IActionResult> GetByWechat(string wechatid)
        {
            var request = new GetUsrRequest()
            {
                WechatID = wechatid
            };
            try
            {
                var response = await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase() { Result = -1, ResultInfo = ex.Message });
            }
        }
    }
}