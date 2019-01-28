using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using System.Linq;
using System.Threading.Tasks;
using DevelopBase.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PurocumentAPI.Controllers
{
    //供应商报价
    public class QuoteImportController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private IServiceProvider _serviceProvider = null;
        protected IServiceProvider ServiceProvider => _serviceProvider;

        public IHostingEnvironment HostingEnvironment => _hostingEnvironment;

        public QuoteImportController(IServiceProvider serviceProvider, IHostingEnvironment hostingEnvironment)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider = serviceProvider;
            _hostingEnvironment = hostingEnvironment;

        }

        //供应商上传报价文件/导入报价
        public async Task<IActionResult> UploadImport([FromBody]ImportQuotesRequest request, IFormFileCollection files)
        {
            try
            {
                long size = files.Sum(f => f.Length);
                var uploadFolder = Path.Combine(HostingEnvironment.WebRootPath, "upload");

                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                IList<string> LocalFileURIs = new List<string>();

                //当前逻辑每此报价仅上传一个单文件 多文件为今后批量报价预留
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Convert.ToString(request.VendorID) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);//[VendorID]-[yyyyMMddHHmmss].xlsx
                        var filePath = Path.Combine(uploadFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            LocalFileURIs.Add(filePath + fileName);
                        }
                    }
                }
                request.localFileURIs = LocalFileURIs;

                var response = await ServiceProvider.HandlerAsync(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase() { Result = -1, ResultInfo = ex.Message });
            }
        }
    }
}
