using System;
using DevelopBase.Common;
using DevelopBase.Message;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
namespace PurocumentLib.Message.Handler
{
    public class AddAuditRecordHandler : HandlerGeneric<AddAuditRecordRequest>
    {
        public AddAuditRecordHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(AddAuditRecordRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            var service=ServiceProvider.GetService<IPurchasingAuditService>();
            
        }
    }
}