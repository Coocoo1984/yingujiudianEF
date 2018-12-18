using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopBase.Message;
using Microsoft.AspNetCore.Mvc;
using PurocumentLib.Message.Request;
using DevelopBase.Common;
namespace PurocumentAPI.Controllers
{
    public class VendorController : ControllerBase
    {
        private IServiceProvider _serviceProvider = null;
        public VendorController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<IActionResult> Add([FromBody]AddVendorRequest request)
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
        public async Task<IActionResult> Update([FromBody]UpdateVendorRequest request)
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
        public async Task<IActionResult> Disable([FromBody]DisableVendorsRequest request)
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
        public async Task<IActionResult> Get(string name)
        {
            var request = new GetVendorRequest()
            {
                Name = name
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
