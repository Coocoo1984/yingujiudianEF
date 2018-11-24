using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using System.Linq;
using System.Threading.Tasks;
using DevelopBase.Message;
namespace PurocumentAPI.Controllers
{
    //供应商报价
    public class QuoteController:ControllerBase
    {
        private IServiceProvider _serviceProvider=null;
        protected IServiceProvider ServiceProvider=>_serviceProvider;
        public QuoteController(IServiceProvider serviceProvider)
        {
            if(serviceProvider==null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider=serviceProvider;
        }
        //获取报价信息
        public async Task<IActionResult> Get(int id)
        {
            var request=new GetQuoteRequest()
            {
                ID=id
            };
            try
            {
                var response=await ServiceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return new JsonResult(new ResponseBase(){Result=-1,ResultInfo=ex.Message});
            }
        }
        //新增报价
        public async Task<IActionResult> Add([FromBody]AddQuoteRequest request)
        {
            try
            {
                var response=await ServiceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return new JsonResult(new ResponseBase(){Result=-1,ResultInfo=ex.Message});
            }

        }
        //修改报价
        public async Task<IActionResult> Update([FromBody]UpdateQuoteRequest request)
        {
            try
            {
                var response=await ServiceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return new JsonResult(new ResponseBase(){Result=-1,ResultInfo=ex.Message});
            }
        }
    }
}
