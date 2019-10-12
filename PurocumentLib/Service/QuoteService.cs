using System;
using DevelopBase.Message;
using PurocumentLib.Dbcontext;
using DevelopBase.Common;
using PurocumentLib.Model;
using PurocumentLib.Entity;
using DevelopBase.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PurocumentLib.Service
{
    public class QuoteService : ServiceBase, IQuoteService
    {
        public QuoteService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public void Add(QuoteModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();

            //失效上一条历史报价 业务类型一致、供应商一致、报价状态为 有效
            var originalQuote = dbcontext.Quotes.Include(i => i.Details).SingleOrDefault(
                                    s => s.BizTypeID == model.BizTypeID
                                    && s.VendorID == model.VendorID
                                    && s.Disable == false);
            
            if (originalQuote != null)
            {
                //失效上一条有效记录
                originalQuote.Disable = true;
                dbcontext.Update(originalQuote);
            }

            DateTime dtNow = DateTime.Now;
            string strDataTimeNow = dtNow.ToString(StrQSuffixFormat);

            var entity = new Quote()
            {
                Code = StrQPrefix + strDataTimeNow,
                Name = model.Name,
                VendorID = model.VendorID,
                BizTypeID = model.BizTypeID,
                CreatDateTime = dtNow,
                CreateUserID = model.CreateUserID,
                UpdateDateTime = dtNow,
                Desc = model.Desc,
                ItemCount = model.Details.Count(),
                Status  = (int)QuoteState.QuoteAwaitAudit1,//增加状态字段 初始值为待审核
                Disable = false
            };
            var detials = from a in model.Details
                          join b in dbcontext.Goods on a.GoodsID equals b.ID
                          select new QuoteDetail()
                          {
                              GoodsID = a.GoodsID,
                              GoodsClassID = b.ClassID,
                              Price = a.Price,
                              Quote = entity
                          };

            int intVendorID = entity.VendorID;
            string strCode = entity.Code;
            string strDateTime = strDataTimeNow;
            //string result = isPass ? "通过" : $"未通过:{Desc}";
            string title = string.Empty;
            string content = string.Empty;
            string toUsrID = string.Empty;

            dbcontext.Add(entity);
            dbcontext.AddRange(detials);
            dbcontext.SaveChanges();

            var toUsrs = dbcontext.Usr.Where(w =>
                w.RoleID == (int)EnumRole.测试
                || w.RoleID == (int)EnumRole.采购员
            ).ToList();
            Vendor vendor = dbcontext.Vendor.SingleOrDefault(s => s.ID == intVendorID);
            var toUsrIDs = toUsrs.Select(s => s.WechatID);
            var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
            toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

            title = "待初审报价";
            content = $"报价编号:{strCode} 供应商:{vendor?.Name}";
            MessageService.Post(
                toUsrID,
                title,
                strDateTime,
                content
            );

        }

        public QuoteModel Load(int id)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            //获取主表
            var result = (from a in dbcontext.Quotes.Where(w => w.ID == id)
                          join b in dbcontext.Vendor on a.VendorID equals b.ID
                          join c in dbcontext.BizTypes on a.BizTypeID equals c.ID
                          select new QuoteModel()
                          {
                              ID = a.ID,
                              Code = a.Code,
                              Name = a.Name,
                              Desc = a.Desc,
                              VendorID = a.VendorID,
                              VendorName = b.Name,
                              BizTypeID = a.BizTypeID,
                              BizTypeName = c.Name,
                              ItemCount = a.ItemCount,
                              Disable = a.Disable,
                          }).FirstOrDefault();
            if(result == null)
            {
                throw new Exception("报价单不存在");
            }
            var details=from a in dbcontext.QuoteDetails.Where(w=>w.QuoteID==id)
                       join b in dbcontext.Goods on a.GoodsID equals b.ID into leftTemp
                       from c in leftTemp.DefaultIfEmpty()
                       select new QuoteDetailModel()
                       {
                           GoodsID=a.ID,
                           GoodsName=c==null?"":c.Name
                       };
            result.Details=details;
            return result;
        }


        public void Update(QuoteModel model)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var master = dbcontext.Quotes.Include(i => i.Details).SingleOrDefault(s => s.ID == model.ID);
            if (master == null)
            {
                throw new Exception("报价单不存在");
            }
            var entity = new Quote()
            {
                Code = model.Code,
                Name = model.Name,
                CreatDateTime = master.CreatDateTime,
                CreateUserID = master.CreateUserID,
                Desc = model.Desc,
                UpdateDateTime = DateTime.Now,
                UpdateUserID = model.UpdateUserID,
                ItemCount = model.Details.Count()
            };
            dbcontext.Update(entity);///
            var modelGoods = model.Details.Select(s => s.GoodsID).ToList();
            var entityGoods = entity.Details.Select(s => s.GoodsID).ToList();
            //删除商品
            var removeGoods = entity.Details.Where(w => !modelGoods.Contains(w.GoodsID)).ToList();
            dbcontext.RemoveRange(removeGoods);///
            //新增
            var addGoods = from a in model.Details.Where(w => !entityGoods.Contains(w.GoodsID))
                           join b in dbcontext.Goods on a.GoodsID equals b.ID
                           select new QuoteDetail()
                           {
                               GoodsID = a.GoodsID,
                               GoodsClassID = b.ClassID,
                               Disable = false,
                               Price = a.Price
                           };
            dbcontext.AddRange(addGoods);///
            //修改
            var updateGoods = addGoods.Select(s => s.GoodsID).Concat(removeGoods.Select(s => s.GoodsID));
            var updateDetails = from a in model.Details.Where(w => updateGoods.Contains(w.GoodsID))
                                join b in dbcontext.Goods on a.GoodsID equals b.ID
                                select new QuoteDetail()
                                {
                                    GoodsID = a.GoodsID,
                                    GoodsClassID = b.ClassID,
                                    Disable = false,
                                    Price = a.Price
                                };
            dbcontext.UpdateRange(updateDetails);///
            dbcontext.SaveChanges();
        }
    }
}
