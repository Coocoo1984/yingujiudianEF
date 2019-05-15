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

    }
}
