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

namespace PurocumentLib.Service
{
    public class PurchasingplanService :ServiceBase ,IPurchasingplanService
    {
        public PurchasingplanService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public decimal CalPlanPriceTotal(int id, int vendorID, int goodsClassID)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var details=dbcontext.PurchasingPlanDetail.Where(w=>w.PurchasingPlanID==id && w.GoodsClassID==goodsClassID).ToList();
            var goodsIds=details.Select(s=>s.GoodsID);
            //查询供应商报价单
            var quoteDetails=from a in dbcontext.QuoteDetails.Include(i=>i.Quote).Where(w=>w.Quote.VendorID==vendorID && goodsIds.Contains(w.GoodsID))
                             group a by a.GoodsID into temp
                             select new {GoodsId=temp.Key,QuoteDetailID=temp.OrderByDescending(o=>o.Quote.CreatDateTime).First().ID};
            //获取单价
            var quoteResult=from a in details
                            join b in quoteDetails on a.GoodsID equals b.GoodsId
                            join c in dbcontext.QuoteDetails on b.QuoteDetailID equals c.ID
                            select new {GoodsID=a.GoodsID,Count=a.PurchasingCount,Price=c.Price};
            return quoteResult.ToList().Sum(s=>s.Price*s.Count);
            
        }

        public void ConfirmVendor(int id, int vendorID, int goodsClassID)
        {
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var details=dbcontext.PurchasingPlanDetail.Where(w=>w.PurchasingPlanID==id && w.GoodsClassID==goodsClassID).ToList();
            var goodsIds=details.Select(s=>s.GoodsID);
            //查询供应商报价单
            var quoteDetails=from a in dbcontext.QuoteDetails.Include(i=>i.Quote).Where(w=>w.Quote.VendorID==vendorID && goodsIds.Contains(w.GoodsID))
                             group a by a.GoodsID into temp
                             select new {GoodsId=temp.Key,QuoteDetailID=temp.OrderByDescending(o=>o.Quote.CreatDateTime).First().ID};
            var updateDetails=from a in details
                              join b in quoteDetails.ToList() on a.GoodsID equals b.GoodsId
                              select new Entity.PurchasingPlanDetail()
                              {
                                  ID=a.ID,
                                  PurchasingPlanID=a.PurchasingPlanID,
                                  GoodsID=a.GoodsID,
                                  PurchasingCount=a.PurchasingCount,
                                  GoodsClassID=a.GoodsClassID,
                                  QuoteDetailID=b.QuoteDetailID,
                                  VendorID=vendorID
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
            var entity=new Entity.PurchasingPlan()
            {
                Desc=plan.Desc,
                BizTypeID=plan.BizType,
                Status=plan.Status,
                CreateTime=plan.CreateTime,
                UpdateTime=plan.CreateTime,
                CreateUserID=plan.CreateUser,
                UpdateUserID=plan.CreateUser
            };
            var dbcontext= ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            dbcontext.Add(entity);
            var details=from a in plan.Details
                        join b in dbcontext.Goods on a.GoodsID equals b.ID
            select new Entity.PurchasingPlanDetail()
            {
                GoodsID=a.GoodsID,
                PurchasingCount=a.PurchasingPlanCount,
                GoodsClassID=b.ClassID,
                PurchasingPlan=entity
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
        //初审提交
        public void SubmitPlan(IEnumerable<int> ids,int userID)
        {
            if(ids==null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            if(dbcontext.PurchasingPlan.Count(c=>ids.Contains(c.ID) && c.Status!=1)>0)
            {
                throw new Exception("采购计划状态不正确");
            }
            var plans=dbcontext.PurchasingPlan.Where(w=>ids.Contains(w.ID)).ToList();
            foreach(var item in plans)
            {
                item.Status=item.Status+1;
                item.UpdateUserID=userID;
                item.UpdateTime=DateTime.Now;
            }
            dbcontext.UpdateRange(plans);
            dbcontext.SaveChanges();
        }

        //提交复审 生成与供应商的订单及订单明细
        public void ComfirmPlanAndSubmitOrder(Model.PurchasingPlan plan)
        {
            var insertPA = new Entity.PurchasingAudit();
            var insertPOs = new List<PurchasingOrder>();
            var insertPODs = new List<PurchasingOrderDetail>();
            var updatePP = new Entity.PurchasingPlan();
            var updatePPD = new List<Entity.PurchasingPlanDetail>();

            ///下面这段代码写得乱 性能应该也不高 EF语法糖不熟悉 后面要改

            if (plan == null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext = ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            //采购计划
            var entityPP = dbcontext.PurchasingPlan.Include(i => i.Details).SingleOrDefault(s => s.ID == plan.ID);
            //采购计划的部门
            var entityD = dbcontext.Department.SingleOrDefault(s => s.ID == plan.DepartmentID);

            if (entityPP == null)
            {
                throw new Exception("采购计划不存在");
            }
            //采购计划明细列表
            var entityPPD = entityPP.Details.ToList();
            //按供应商分组,循环操作
            var vendorIDs = entityPPD.Select(s => s.VendorID).Distinct().ToList();

            foreach (var vendorID in vendorIDs)
            {
                //按供应商的订单明细集合
                var verdorPPDs = entityPP.Details.Where(w=> vendorIDs.Contains(w.VendorID));

                int itemCount = 0;
                decimal? total = 0;

                foreach (var vendorPPD in verdorPPDs)
                {
                    //生成每个供应商分配的采购明细
                    PurchasingOrderDetail pod = new PurchasingOrderDetail
                    {
                        //PurchasingOrder = po,
                        PurchasingOrderStateID = 1,//写死
                        GoodsClassID = vendorPPD.GoodsClassID,
                        GoodsID = vendorPPD.GoodsID,
                        Count = vendorPPD.PurchasingCount,                        
                        Price = vendorPPD.Price,
                        Subtotal = vendorPPD.PurchasingCount*vendorPPD.Price,
                        ActualCount = 0,
                        ActualSubtotal = 0,
                        //CreateUsrID =  //没得用户
                        CreateTime = DateTime.Now,
                        ////PurchasiongOrderStateID//冗余的字段
                        PurchasingPlanDetailID = vendorPPD.ID
                    };
                    insertPODs.Add(pod);    /// 生成订单明细

                    total += pod.Subtotal;//累计每种商品的小计金额
                    itemCount++;//明细数量

                    //更新采购计划、采购计划明细状态
                    vendorPPD.Status = 9;//复审通过

                    updatePPD.Add(vendorPPD);   /// 更新PPD

                }

                PurchasingOrder po = new PurchasingOrder
                {
                    Code = $"PO{DateTime.Now.ToString("yyyyMMddHHmmssfff")}",//[2][17]
                    PurchasingPlanID = plan.ID,
                    PurchasingOrderStatusID = 1,//写死
                    VendorID = vendorID.Value,
                    DepartmentID = plan.DepartmentID,
                    Tel = entityD.Tel,
                    Addr = entityD.Address,
                    BizTypeID = plan.BizType,
                    //CreateUsrID =  //没得用户
                    CreateTime = DateTime.Now,
                    Total = total,
                    ItemCount = itemCount
                };
                insertPOs.Add(po);  // 生成PO

            }
            
            updatePP = entityPP;
            updatePP.Status = 9; //复审确认

            insertPA = new PurchasingAudit
            {
                PlanID = plan.ID,
                //UserID = //没得用户
                CreateTime = DateTime.Now,
                Result = 3,
                //Desc = //没得
            };


            dbcontext.UpdateRange(updatePPD);
            dbcontext.Update(updatePP);
            dbcontext.AddRange(insertPOs);
            dbcontext.AddRange(insertPODs);
            dbcontext.SaveChanges();
        }


        public void UpdatePlan(Model.PurchasingPlan plan)
        {
            if(plan==null)
            {
                throw new ArgumentNullException();
            }
            var dbcontext=ServiceProvider.GetDbcontext<IPurocumentDbcontext>();
            var entity=dbcontext.PurchasingPlan.Include(i=>i.Details).SingleOrDefault(s=>s.ID==plan.ID);
            if(entity==null)
            {
                throw new Exception("采购计划不存在");
            }
            var modifyStatus=new int[]{1,3,7};
            if(entity.Status!=1)
            {
                throw new Exception("采购计划不可修改");
            }
            entity.Desc=plan.Desc;
            entity.UpdateTime=DateTime.Now;
            entity.UpdateUserID=plan.UpdateUser;
            //修改商品信息
            var saveGoods=entity.Details.Select(s=>s.GoodsID).ToList();
            var submitGoods=plan.Details.Select(s=>s.GoodsID).ToList();
            //新增
            var addGoods =submitGoods.Where(w=>!saveGoods.Contains(w));
            var addDetails=from a in  plan.Details.Where(w=>addGoods.Contains(w.GoodsID)) 
                           join b in dbcontext.Goods on a.GoodsID equals b.ID
                            select new Entity.PurchasingPlanDetail()
                            {
                                PurchasingPlanID=entity.ID,
                                GoodsID=a.GoodsID,
                                GoodsClassID=b.ClassID,
                                PurchasingCount=a.PurchasingPlanCount
                            };
            dbcontext.AddRange(addDetails);
            //删除
            var removeGoods=saveGoods.Where(w=>!submitGoods.Contains(w));
            var removeDetails=entity.Details.Where(w=>removeGoods.Contains(w.GoodsID));
            dbcontext.RemoveRange(removeDetails);
            //修改
            var combinGoods=addGoods.Concat(removeGoods);
            var updateDetails=from a in plan.Details.Where(w=>combinGoods.Contains(w.GoodsID))
                              join b in dbcontext.Goods on a.GoodsID equals b.ID
                              select new Entity.PurchasingPlanDetail()
                            {
                                PurchasingPlanID=entity.ID,
                                GoodsID=a.GoodsID,
                                GoodsClassID=b.ClassID,
                                PurchasingCount=a.PurchasingPlanCount
                            };
            dbcontext.UpdateRange(updateDetails);
            dbcontext.SaveChanges();
        }
    }
}
