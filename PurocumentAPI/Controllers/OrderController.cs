using System;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Message;
using DevelopBase.Common;
using System.Threading.Tasks;
using PurocumentLib.Message.Request;

namespace PurocumentAPI.Controllers
{
    public class OrderController : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;
        public OrderController(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider = serviceProvider;
        }
        //供应商确认订单
        public async Task<IActionResult> VendorComfirm([FromBody]ComfirmOrderRequest request)
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
        //供应商发货
        public async Task<IActionResult> ComfirmDelivery([FromBody]ComfirmDeliveryRequest request)
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
        //收货
        /// <summary>
        /// CheckInRequest 中传入待更新的List<PurchasingOrderDetail>对象包含ID和ActualCount值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> CheckIn([FromBody]CheckInRequest request)
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

        //订单收货完成
        public async Task<IActionResult> StockIn([FromBody]ComfirmStockInRequest request)
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