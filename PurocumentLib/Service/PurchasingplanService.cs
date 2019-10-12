using System;
using DevelopBase.Services;
using PurocumentLib.Model;
using System.Linq;
using System.Collections.Generic;
using Entity=PurocumentLib.Entity;
using PurocumentLib.Dbcontext;
using DevelopBase.Common;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Entity;
using System.Net.Http;

namespace PurocumentLib.Service
{
    public class PurchasingplanService :ServiceBase ,IPurchasingplanService
    {
        public PurchasingplanService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public decimal CalPlanPriceTotal(int id, int vendorID, int goodsClassID)
        {
            //按 供应商-商品分类模式 计算供应商报价
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var details=dbcontext.PurchasingPlanDetail.Where(w=>w.PurchasingPlanID==id && w.GoodsClassID==goodsClassID).ToList();
            var goodsIds=details.Select(s=>s.GoodsID);
            //查询供应商报价单
            var quoteDetails = from a in dbcontext.QuoteDetails.Include(i => i.Quote).Where(w => w.Quote.VendorID == vendorID && goodsIds.Contains(w.GoodsID))
                               group a by a.GoodsID into temp
                               select new
                               {
                                   GoodsId = temp.Key,
                                   QuoteDetailID = temp.OrderByDescending(o => o.Quote.CreatDateTime).First().ID
                               };
            //获取单价
            var quoteResult = from a in details
                              join b in quoteDetails on a.GoodsID equals b.GoodsId
                              join c in dbcontext.QuoteDetails on b.QuoteDetailID equals c.ID
                              select new
                              {
                                  GoodsID = a.GoodsID,
                                  Count = a.PurchasingCount,
                                  Price = c.Price
                              };
            return quoteResult.ToList().Sum(s=>s.Price*s.Count);
        }

        public void ConfirmVendor(int id, int vendorID, int goodsClassID)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var details=dbcontext.PurchasingPlanDetail.Where(w=>w.PurchasingPlanID==id && w.GoodsClassID==goodsClassID).ToList();
            var goodsIds=details.Select(s=>s.GoodsID);
            //查询供应商报价单 之前的代码缺少 Quote生效的条件判断
            var quoteDetailIDsByGoodsID = from a in dbcontext.QuoteDetails.Include(i => i.Quote).Where(w => w.Quote.VendorID == vendorID && w.Quote.Disable == false && goodsIds.Contains(w.GoodsID))
                               group a by a.GoodsID into temp
                               select new {
                                   GoodsId = temp.Key,
                                   QuoteDetailID = temp.OrderByDescending(o => o.Quote.CreatDateTime).First().ID
                               };
            var quoteDetails = from a in dbcontext.QuoteDetails.Include(i => i.Quote).Where(w => w.Quote.VendorID == vendorID && w.Quote.Disable == false && goodsIds.Contains(w.GoodsID))
                               select new
                               {
                                   GoodsId = a.GoodsID,
                                   QuoteDetailID = a.ID,
                                   Price = a.Price
                               };//修复缺陷
            var updateDetails = from a in details
                                join b in quoteDetailIDsByGoodsID.ToList() on a.GoodsID equals b.GoodsId
                                join c in quoteDetails.ToList() on a.GoodsID equals c.GoodsId
                                select new Entity.PurchasingPlanDetail()
                                {
                                    ID = a.ID,
                                    PurchasingPlanID = a.PurchasingPlanID,
                                    GoodsID = a.GoodsID,
                                    PurchasingCount = a.PurchasingCount,
                                    Price = c.Price,//修复缺陷
                                    GoodsClassID = a.GoodsClassID,
                                    QuoteDetailID = b.QuoteDetailID,
                                    VendorID = vendorID
                                };
            dbcontext.UpdateRange(updateDetails);
            dbcontext.SaveChanges();
        }

        public void ConfirmVendor(int id, int vendorID)
        {
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var details = dbcontext.PurchasingPlanDetail.Where(w => w.PurchasingPlanID == id).ToList();
            var goodsIds = details.Select(s => s.GoodsID);
            //查询供应商报价单
            var quoteDetails = from a in dbcontext.QuoteDetails.Include(i => i.Quote).Where(w => w.Quote.VendorID == vendorID && w.Quote.Disable == false && goodsIds.Contains(w.GoodsID))
                               select new
                               {
                                   GoodsId = a.GoodsID,
                                   QuoteDetailID = a.ID,
                                   Price = a.Price
                               };//修复缺陷
            var updateDetails = from a in details
                                join b in quoteDetails.ToList() on a.GoodsID equals b.GoodsId
                                select new Entity.PurchasingPlanDetail()
                                {
                                    ID = a.ID,
                                    PurchasingPlanID = a.PurchasingPlanID,
                                    GoodsID = a.GoodsID,
                                    PurchasingCount = a.PurchasingCount,
                                    Price = b.Price,//修复缺陷
                                    GoodsClassID = a.GoodsClassID,
                                    QuoteDetailID = b.QuoteDetailID,
                                    VendorID = vendorID
                                };
            dbcontext.UpdateRange(updateDetails);
            dbcontext.SaveChanges();
        }

        public void CreatePlan(Model.PurchasingPlan plan)
        {
            if(plan.Details.Count(c=>c.PurchasingPlanCount<=0)>0)
            {
                throw new Exception("商品数量无效");
            }
            //创建主表
            var entity = new Entity.PurchasingPlan()
            {
                Code = StrPPPrefix + DateTime.Now.ToString(StrPPSuffixFormat),//[2][17]
                Desc = plan.Desc,
                BizTypeID = plan.BizType,
                Status = (int)EnumPurchasingPlanState.PlanDraft,//草稿新增  之前改漏了 BUG//plan.Status,
                CreateTime = plan.CreateTime,
                UpdateTime = plan.CreateTime,
                CreateUserID = plan.CreateUser,
                UpdateUserID = plan.CreateUser,
                ItemCount = plan.Details.Count(),
                DepartmentID = plan.DepartmentID
            };
            var dbcontext= ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            var details = from a in plan.Details
                          join b in dbcontext.Goods on a.GoodsID equals b.ID
                          select new Entity.PurchasingPlanDetail()
                          {
                              GoodsID = a.GoodsID,
                              PurchasingCount = a.PurchasingPlanCount,
                              GoodsClassID = b.ClassID,
                              PurchasingPlan = entity
                          };
            dbcontext.AddRange(details);
            dbcontext.SaveChanges();

        }

        public Model.PurchasingPlan Load(int id)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.PurchasingPlan.Include(i=>i.Details).SingleOrDefault(s=>s.ID==id);
            var details=entity.Details.Select(s=>new Model.PurchasingPlanDetail()
            {
                GoodsID=s.GoodsID,
                PurchasingPlanCount=s.PurchasingCount
            });
            var master=new Model.PurchasingPlan()
            {
                ID=entity.ID,
                DepartmentID=entity.DepartmentID,
                BizType=entity.BizTypeID,
                Desc=entity.Desc,
                Status=entity.Status,
                Details=details,
                CreateTime=entity.CreateTime,
                CreateUser=entity.CreateUserID,
                UpdateUser=entity.UpdateUserID,
                UpdateTime=entity.UpdateTime

            };
            return master;
        }
        //采购需求部门确认提交计划 待初审
        public void SubmitPlan(IEnumerable<int> ids, int userID)
        {
            if (ids == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            ///之前改审核状态改漏了 值是对的 避免使用魔幻数值 if(dbcontext.PurchasingPlan.Count(c=>ids.Contains(c.ID) && c.Status!=1)>0)
            if (dbcontext.PurchasingPlan.Count(c => ids.Contains(c.ID) && c.Status != (int)EnumPurchasingPlanState.PlanDraft) > 0)
            {
                throw new Exception("采购计划状态不正确");
            }
            var plans = dbcontext.PurchasingPlan.Where(w => ids.Contains(w.ID)).ToList();

            int intDeparmentID = 0;
            string strCode = string.Empty;
            string strDateTime = string.Empty;
            //string result = isPass ? "通过" : $"未通过:Desc";
            string title = string.Empty;
            string content = string.Empty;
            string toUsrID = string.Empty;

            foreach (var item in plans)
            {
                item.Status = item.Status + 1;
                item.UpdateUserID = userID;
                item.UpdateTime = DateTime.Now;

                strCode += item.Code + ' ';//为了通知
                intDeparmentID = item.DepartmentID;//为了通知
                strDateTime = item.UpdateTime.ToString(strMessageTimeFormat);
            }
            dbcontext.UpdateRange(plans);
            dbcontext.SaveChanges();


            var toUsrs = dbcontext.Usr.Where(w => 
                w.RoleID== (int)EnumRole.测试
                || w.RoleID == (int)EnumRole.采购员
            ).ToList();
            Usr usr = dbcontext.Usr.SingleOrDefault(s => s.ID == userID);
            Department department = dbcontext.Department.SingleOrDefault(s => s.ID == intDeparmentID);
            var toUsrIDs = toUsrs.Select(s => s.WechatID);
            toUsrID = string.Empty;
            var toUserWechatIDs = toUsrs.Select(s => s.WechatID);
            toUsrID = string.Join("|", toUsrs.Select(s => s.WechatID).ToArray());

            title = "待初审采购计划";
            content = $"编号:{strCode}&nbsp部门:{department?.Name}";
            MessageService.Post(
                toUsrID,
                title,
                strDateTime,
                content
            );
        }

        public void UpdatePlan(Model.PurchasingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();

            if (plan.Status == (int)EnumPurchasingPlanState.Cancelled)
            {
                //删除草稿及明细
                var deleteEntity = dbcontext.PurchasingPlan.Include(i => i.Details).SingleOrDefault(s => s.ID == plan.ID);
                if (deleteEntity == null)
                {
                    throw new Exception("采购计划不存在");
                }
                dbcontext.Remove(deleteEntity);///
                dbcontext.SaveChanges();
            }
            else
            {

                var entity = dbcontext.PurchasingPlan.Include(i => i.Details).SingleOrDefault(s => s.ID == plan.ID);
                if (entity == null)
                {
                    throw new Exception("采购计划不存在");
                }
                //BUG 只有在草稿/初审驳回/复审驳回 3种状态下可以修改;其他（为作废预留）状态下应该复制一条原采购计划的内容进行新增（暂时未实现） //var modifyStatus=new int[]{1,3,7};
                var modifyStatus = new int[] {
                (int)EnumPurchasingPlanState.PlanDraft,
                (int)EnumPurchasingPlanState.PlanAudit1Rejected,
                (int)EnumPurchasingPlanState.PlanAudit2Rejected
            };

                if (modifyStatus.Contains(entity.Status))
                {
                    var saveGoods = entity.Details.Select(s => s.GoodsID).ToList();//数据库中的商品列表 e.g. 1 2 3 
                    var submitGoods = plan.Details.Select(s => s.GoodsID).ToList();//请求中的的商品列表 e.g.   2 3 4

                    //修改
                    var updateGoods = saveGoods.Intersect(submitGoods);//求交集 e.g. 2 3
                    if (updateGoods != null || updateGoods.Count() > 0)
                    {
                        ////var updateDetails = from a in entity.Details.Where(w => updateGoods.Contains(w.GoodsID))
                        ////                    join b in plan.Details on a.GoodsID equals b.GoodsID
                        ////                    join c in dbcontext.Goods on a.GoodsID equals c.ID
                        ////                    select new Entity.PurchasingPlanDetail()
                        ////                    {
                        ////                        PurchasingPlanID = entity.ID,
                        ////                        GoodsID = a.GoodsID,
                        ////                        GoodsClassID = c.ClassID,
                        ////                        PurchasingCount = b.PurchasingPlanCount
                        ////                    };
                        
                        var detailsList = from a in entity.Details.Where(w => updateGoods.Contains(w.GoodsID))
                                              join b in plan.Details on a.GoodsID equals b.GoodsID
                                              join c in dbcontext.Goods on a.GoodsID equals c.ID
                                              select new {
                                                  PurchasingPlanID = a.ID,
                                                  PurchasingCount = b.PurchasingPlanCount
                                              };
                        var updateDetailsIds = detailsList.Select(s => s.PurchasingPlanID);

                        var updateDetailsObjs = entity.Details.Where(w => updateDetailsIds.Contains(w.ID));
                        foreach(var obj in updateDetailsObjs)
                        {
                            obj.PurchasingCount = detailsList.Where(w => w.PurchasingPlanID.Equals(obj.ID)).Select(s=>s.PurchasingCount).Single();
                        }
                                            

                        dbcontext.UpdateRange(updateDetailsObjs);///
                    }

                    entity.Desc = plan.Desc;
                    entity.UpdateTime = DateTime.Now;
                    entity.UpdateUserID = plan.UpdateUser;
                    entity.ItemCount = plan.Details.Count();
                    switch (entity.Status)
                    {
                        case (int)EnumPurchasingPlanState.PlanDraft://草稿修改 不更改状态值
                            break;
                        case (int)EnumPurchasingPlanState.PlanAudit1Rejected://初审驳回后修改 更改为等待初审
                            entity.Status = (int)EnumPurchasingPlanState.PlanAwaitAudit1;
                            break;
                        case (int)EnumPurchasingPlanState.PlanAudit2Rejected://复审驳回后修改 更改为等待复审(初审通过)
                            entity.Status = (int)EnumPurchasingPlanState.PlanAudit1Pass;
                            break;
                    }
                    dbcontext.Update(entity);///

                    //删除
                    var removeGoods = saveGoods.Except(submitGoods);// e.g. 1
                    if (removeGoods != null || removeGoods.Count() > 0)
                    {
                        //saveGoods.Where(w => !submitGoods.Contains(w));数据库中 含有 请求不包含的商品 这种写法太绕了
                        var removeDetails = entity.Details.Where(w => removeGoods.Contains(w.GoodsID));
                        dbcontext.RemoveRange(removeDetails);///
                    }

                    //新增
                    var addGoods = submitGoods.Except(saveGoods);// e.g. 4
                    //submitGoods.Where(w => !saveGoods.Contains(w));请求中 含有 数据库中无的 明细的商品 这种写法太绕了
                    if (addGoods != null || addGoods.Count() > 0)
                    {
                        var addDetails = from a in plan.Details.Where(w => addGoods.Contains(w.GoodsID))
                                         join b in dbcontext.Goods on a.GoodsID equals b.ID
                                         select new Entity.PurchasingPlanDetail()
                                         {
                                             PurchasingPlanID = entity.ID,
                                             GoodsID = a.GoodsID,
                                             GoodsClassID = b.ClassID,
                                             PurchasingCount = a.PurchasingPlanCount
                                         };
                        dbcontext.AddRange(addDetails);///
                    }

                    dbcontext.SaveChanges();
                }
                else
                {
                    throw new Exception("采购计划不可修改");

                }
            }
        }
    }
}
