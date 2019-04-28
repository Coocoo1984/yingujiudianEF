using System;
using DevelopBase.Services;

namespace PurocumentLib.Service
{
    public interface IQuoteAuditService : IService
    {
        //采购计划审核
        void QuoteAudit(int quoteId, int userID, bool isPass, string Desc);
    }
}
