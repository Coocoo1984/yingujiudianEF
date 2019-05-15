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
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var quote = dbcontext.Quotes.Single(s => s.ID == quoteId);
            if (quote == null)
            {
                throw new Exception("报价不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)QuoteState.QuoteAudit1Pass : (int)QuoteState.QuoteAudit1Rejected;
            int auditType = isPass ? (int)QuoteAuditType.PlanAudit1Pass : (int)QuoteAuditType.PlanAudit1Rejected;

            //保存审核结果和修改计划状态
            quote.Status = status;
            quote.UpdateDateTime = dateTimeNow;
            quote.UpdateUserID = userID;

            dbcontext.Update(quote);

            var record = new QuoteAudit()
            {
                QuoteID = quoteId,
                Result = auditType,
                CreateUsrID = userID,
                AuditTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }

        public void QuoteAudit2(int quoteId, int userID, bool isPass, string Desc)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var quote = dbcontext.Quotes.Single(s => s.ID == quoteId);
            if (quote == null)
            {
                throw new Exception("报价不存在");
            }

            DateTime dateTimeNow = DateTime.Now;
            int status = isPass ? (int)QuoteState.QuoteAudit2Pass : (int)QuoteState.QuoteAudit2Rejected;
            int auditType = isPass ? (int)QuoteAuditType.PlanAudit2Pass : (int)QuoteAuditType.PlanAudit2Rejected;

            //保存审核结果和修改计划状态
            quote.Status = status;
            quote.UpdateDateTime = dateTimeNow;
            quote.UpdateUserID = userID;

            dbcontext.Update(quote);

            var record = new QuoteAudit()
            {
                QuoteID = quoteId,
                Result = auditType,
                CreateUsrID = userID,
                AuditTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();
        }
    }
}
