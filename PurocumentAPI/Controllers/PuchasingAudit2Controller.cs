using System;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Message;
using DevelopBase.Common;
using System.Threading.Tasks;
using PurocumentLib.Message.Request;
namespace PurocumentAPI.Controllers
{
    public class PurchasingAudit2Controller : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;
        public PurchasingAudit2Controller(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider = serviceProvider;
        }
        //采购计划复审 复审成功后生成订单
        public async Task<IActionResult> PlanAudit([FromBody]PlanAudit2Request request)
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
