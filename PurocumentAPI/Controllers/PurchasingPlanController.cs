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
    public class PurchasingPlanController:ControllerBase
    {
        private IServiceProvider _serviceProvider=null;
        public PurchasingPlanController(IServiceProvider serviceProvider)
        {
            _serviceProvider=serviceProvider;
        }
        //新增采购计划
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreatePurocumentPlanRequest request)
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
        //修改采购计划
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdatePurocumentPlanRequest request)
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

        //修改采购计划
        public async Task<IActionResult> Cancel([FromBody]UpdatePurocumentPlanRequest request)
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
        //获取商品信息
        public async Task<IActionResult> Get(int id)
        {
            var request=new GetPurchasingPlanRequest()
            {
                ID=id
            };
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
        //提交初审
        public async Task<IActionResult> SubmitFirst([FromBody]SubmitPlanRequest request)
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
        //计算采购计划商品类别金额
        public async Task<IActionResult> CalPlanTotalByGoodsClass([FromBody]CalVendorQuoteTotalRequest request)
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
        //确认采购计划供应商
        public async Task<IActionResult> ConfirmVendor([FromBody]ConfirmPlanVendorRequest request)
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