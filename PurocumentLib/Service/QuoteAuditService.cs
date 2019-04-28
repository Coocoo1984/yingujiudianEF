using System;
using DevelopBase.Common;
using DevelopBase.Services;
using PurocumentLib.Dbcontext;
using PurocumentLib.Entity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Model;

namespace PurocumentLib.Service
{
    public class QuoteAuditService: ServiceBase, IQuoteAuditService
    {
        public QuoteAuditService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void QuoteAudit(int quoteId, int userID, bool isPass, string Desc)
        {
            throw new NotImplementedException();
        }
    }
}
