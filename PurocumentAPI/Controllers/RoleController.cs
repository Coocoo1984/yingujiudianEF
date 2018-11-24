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
    public class RoleController : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;
        public RoleController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        //角色新增
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddRoleRequest request)
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
        //角色修改
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateRoleRequest request)
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
        //获取角色信息
        public async Task<IActionResult> Get(int id)
        {
            var request = new GetRoleRequest()
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
    }
}