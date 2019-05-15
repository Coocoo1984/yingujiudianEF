using System;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Message;
using DevelopBase.Common;
using System.Threading.Tasks;
using PurocumentLib.Message.Request;
namespace PurocumentAPI.Controllers
{
    public class PurchasingAuditController:ControllerBase
    {
        private IServiceProvider _serviceProvider=null;
        public PurchasingAuditController(IServiceProvider serviceProvider)
        {
            if(serviceProvider==null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider=serviceProvider;
        }
        //采购计划审核(初审)
        public async Task<IActionResult> PlanAudit([FromBody]PlanAuditRequest request)
        {
            try
            {
                if(request==null)
                {
                    throw new ArgumentNullException();
                }
                var response=await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return new JsonResult(new ResponseBase(){Result=-1,ResultInfo=ex.Message});
            }
        }

        //采购计划复审
        public async Task<IActionResult> PlanAudit2([FromBody]PlanAudit2Request request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                var response = await _serviceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase() { Result = -1, ResultInfo = ex.Message });
            }
        }

        //采购计划三审
        public async Task<IActionResult> PlanAudit3([FromBody]PlanAudit3Request request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
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
