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
    public class DepartmentController:ControllerBase
    {
        private IServiceProvider _serviceProvider=null;
        public DepartmentController(IServiceProvider serviceProvider)
        {
            _serviceProvider=serviceProvider;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddDepartmentRequest request)
        {
            try
            {
                var response=await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return new JsonResult(new ResponseBase(){Result=-1,ResultInfo=ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateDepartmentRequest request)
        {
            try
            {
                var response=await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return new JsonResult(new ResponseBase(){Result=-1,ResultInfo=ex.Message});
            }

        }
    }
}
