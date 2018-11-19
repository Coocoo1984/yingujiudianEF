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
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasingPlanController:ControllerBase
    {
        private IServiceProvider _serviceProvider=null;
        public PurchasingPlanController(IServiceProvider serviceProvider)
        {
            _serviceProvider=serviceProvider;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody]CreatePurocumentPlanRequest request)
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