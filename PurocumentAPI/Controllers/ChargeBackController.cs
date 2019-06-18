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
    public class ChargeBackController : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;

        public ChargeBackController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //采购需求部门发起退货
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddChargeBackRequest request)
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

        //退货审核
        public async Task<IActionResult> Audit([FromBody]ChargeBackAuditRequest request)
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

        //供应商确认退货
        public async Task<IActionResult> VendorComfirm([FromBody]ChargeBackVerdorComfirmRequest request)
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

        //退货完成
        public async Task<IActionResult> Finish([FromBody]ChargeBackFinishRequest request)
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