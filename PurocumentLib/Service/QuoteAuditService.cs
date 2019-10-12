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
            int auditType = isPass ? (int)QuoteAuditType.Audit1Pass : (int)QuoteAuditType.Audit1Rejected;

            //保存审核结果和修改计划状态
            quote.Status = status;
            quote.UpdateDateTime = dateTimeNow;
            quote.UpdateUserID = userID;

            int intVendorID = quote.VendorID;
            string strCode = quote.Code;
            string strDateTime = dateTimeNow.ToString(StrDateTimeFormat);
            string result = isPass ? "通过" : $"未通过:{Desc}";
            string title = string.Empty;
            string content = string.Empty;
            string toUsrID = string.Empty;

            dbcontext.Update(quote);

            var record = new QuoteAudit()
            {
                QuoteID = quoteId,
                Result = auditType,
                CreateUsrID = userID,
                Desc = Desc,
                AuditTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();

            Usr usr = dbcontext.Usr.SingleOrDefault(s => s.ID == userID);
            Vendor vendor = dbcontext.Vendor.SingleOrDefault(s => s.ID == intVendorID);

            if (isPass)
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购总监
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "待复审报价";
                content = $"报价编号:{strCode}   供应商:{vendor?.Name}";
            }
            else
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购主管
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "报价初审驳回";
                content = $"报价编号:{strCode}    复审结果:{result}";
            }

            MessageService.Post(
                toUsrID,
                title,
                strDateTime,
                content
            );

        }

        //报价复审
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
            int auditType = isPass ? (int)QuoteAuditType.Audit2Pass : (int)QuoteAuditType.Audit2Rejected;

            //保存审核结果和修改计划状态
            quote.Status = status;
            quote.UpdateDateTime = dateTimeNow;
            quote.UpdateUserID = userID;

            int intVendorID = quote.VendorID;
            string strCode = quote.Code;
            string strDateTime = dateTimeNow.ToString(StrDateTimeFormat);
            string result = isPass ? "通过" : $"未通过:{Desc}";
            string title = string.Empty;
            string content = string.Empty;
            string toUsrID = string.Empty;

            dbcontext.Update(quote);

            var record = new QuoteAudit()
            {
                QuoteID = quoteId,
                Result = auditType,
                CreateUsrID = userID,
                Desc = Desc,
                AuditTime = DateTime.Now
            };
            dbcontext.Add(record);
            dbcontext.SaveChanges();

            Usr usr = dbcontext.Usr.SingleOrDefault(s => s.ID == userID);
            Vendor vendor = dbcontext.Vendor.SingleOrDefault(s => s.ID == intVendorID);

            if (isPass)
            {
                //////这里用户是Vendor 但node.js没有写vendor id信息 所以关联不上
                ////var toUsrs = dbcontext.Usr.Where(w =>
                ////    w.RoleID == (int)EnumRole.测试
                ////    || w.RoleID == (int)EnumRole.采购总监
                ////).ToList();

                ////var toUsrIDs = toUsrs.Select(s => s.WechatID);
                ////var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                ////toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                ////title = "复审报价";
                ////content = $"编号:{strCode}&nbsp供应商:{vendor?.Name}";
            }
            else
            {
                var toUsrs = dbcontext.Usr.Where(w =>
                    w.RoleID == (int)EnumRole.测试
                    || w.RoleID == (int)EnumRole.采购主管
                ).ToList();

                var toUsrIDs = toUsrs.Select(s => s.WechatID);
                var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
                toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

                title = "报价初审被驳回";
                content = $"报价编号:{strCode}    复审结果:{result}";
            }

            MessageService.Post(
                toUsrID,
                title,
                strDateTime,
                content
            );

        }
    }
}
