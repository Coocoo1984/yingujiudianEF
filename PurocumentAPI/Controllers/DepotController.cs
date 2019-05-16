using System;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Message;
using DevelopBase.Common;
using System.Threading.Tasks;
using PurocumentLib.Message.Request;

namespace PurocumentAPI.Controllers
{
    public class DepotController : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;
        public DepotController(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider = serviceProvider;
        }

        //需求部门库管提交在库数量
        public async Task<IActionResult> DeptStockCheck([FromBody]StockCheckRequest request)
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