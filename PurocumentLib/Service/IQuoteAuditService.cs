using System;
using DevelopBase.Services;

namespace PurocumentLib.Service
{
    public interface IQuoteAuditService : IService
    {
        //报价初审
        void QuoteAudit(int quoteId, int userID, bool isPass, string Desc);
        //报价复审
        void QuoteAudit2(int quoteId, int userID, bool isPass, string Desc);
    }
}
