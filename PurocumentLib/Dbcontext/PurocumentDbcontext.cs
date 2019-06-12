using System;
using DevelopBase.Data;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using PurocumentLib.Entity;
using System.Linq;

namespace PurocumentLib.Dbcontext
{
    public class PurocumentDbcontext : DbcontextBase, IPurocumentDbcontext
    {
        public IQueryable<BizType> BizTypes => Set<BizType>().AsNoTracking();

        public IQueryable<Unit> Units => Set<Unit>().AsNoTracking();

        public IQueryable<GoodsClass> GoodsClass => Set<GoodsClass>().AsNoTracking();

        public IQueryable<Goods> Goods => Set<Goods>().AsNoTracking();

        public IQueryable<Department> Department => Set<Department>().AsNoTracking();

        public IQueryable<Role> Role => Set<Role>().AsNoTracking();

        public IQueryable<Usr> Usr => Set<Usr>().AsNoTracking();

        public IQueryable<Vendor> Vendor => Set<Vendor>().AsNoTracking();

        public IQueryable<RsVendor> RsVendor => Set<RsVendor>().AsNoTracking();

        public IQueryable<PurchasingPlan> PurchasingPlan => Set<PurchasingPlan>().AsNoTracking();

        public IQueryable<PurchasingPlanDetail> PurchasingPlanDetail => Set<PurchasingPlanDetail>().AsNoTracking();

        public IQueryable<PurchasingAudit> PurchasingAudits => Set<PurchasingAudit>().AsNoTracking();

        public IQueryable<Quote> Quotes => Set<Quote>().AsNoTracking();

        public IQueryable<QuoteDetail> QuoteDetails => Set<QuoteDetail>().AsNoTracking();

        public IQueryable<QuoteAudit> QuoteAudits => Set<QuoteAudit>().AsNoTracking();

        public IQueryable<PurchasingOrder> PurchasingOrder => Set<PurchasingOrder>().AsNoTracking();

        public IQueryable<PurchasingOrderDetail> PurchasingOrderDetail => Set<PurchasingOrderDetail>().AsNoTracking();

        public IQueryable<Depot> Depot => Set<Depot>().AsNoTracking();

        public IQueryable<DepotDetail> DepotDetail => Set<DepotDetail>().AsNoTracking();

        public IQueryable<Permission> Permission => Set<Permission>().AsNoTracking();

        public IQueryable<RsPermission> RsPermission => Set<RsPermission>().AsNoTracking();

        public IQueryable<ChargeBack> ChargeBack => Set<ChargeBack>().AsNoTracking();

        public IQueryable<ChargeBackDetail> ChargeBackDetail => Set<ChargeBackDetail>().AsNoTracking();

        public PurocumentDbcontext(string connectionString) : base(connectionString)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BizType>(builder =>
            {
                builder.ToTable("biz_type").HasKey(k => k.ID); ;
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.Disable).HasColumnName("disable");
            });
            modelBuilder.Entity<Unit>(builder =>
            {
                builder.ToTable("goods_unit").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.Remark).HasColumnName("remark");
            });
            modelBuilder.Entity<GoodsClass>(builder =>
            {
                builder.ToTable("goods_class").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.Remark).HasColumnName("remark");
                builder.Property(p => p.Specification).HasColumnName("specification");
                builder.Property(p => p.BizTypeID).HasColumnName("biz_type_id");
                builder.Property(p => p.Disable).HasColumnName("disable");
            });
            modelBuilder.Entity<Goods>(builder =>
            {
                builder.ToTable("goods").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Specification).HasColumnName("specification");
                builder.Property(p => p.ClassID).HasColumnName("goods_class_id");
                builder.Property(p => p.UnitID).HasColumnName("goods_unit_id");
                builder.Property(p => p.Disable).HasColumnName("disable");
                builder.Property(p => p.Desc).HasColumnName("desc");
            });
            modelBuilder.Entity<Department>(builder =>
            {
                builder.ToTable("department").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.WechatID).HasColumnName("wechat_id");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.Address).HasColumnName("addr");
                builder.Property(p => p.Address1).HasColumnName("addr1");
                builder.Property(p => p.Tel).HasColumnName("tel");
                builder.Property(p => p.Tel1).HasColumnName("tel1");
                builder.Property(p => p.Mobile).HasColumnName("mobile");
                builder.Property(p => p.Mobile1).HasColumnName("mobile1");
                builder.Property(p => p.Disable).HasColumnName("disable");
            });
            modelBuilder.Entity<Role>(builder =>
            {
                builder.ToTable("role").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.WechatGroupID).HasColumnName("wechat_group_id");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Code).HasColumnName("code");
            });
            modelBuilder.Entity<Usr>(builder =>
            {
                builder.ToTable("usr").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.WechatID).HasColumnName("wechat_id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.Tel).HasColumnName("tel");
                builder.Property(p => p.Tel1).HasColumnName("tel1");
                builder.Property(p => p.Mobile).HasColumnName("mobile");
                builder.Property(p => p.Mobile1).HasColumnName("mobile1");
                builder.Property(p => p.Addr).HasColumnName("addr");
                builder.Property(p => p.Addr1).HasColumnName("addr1");
                builder.Property(p => p.DepartmentID).HasColumnName("department_id");
                builder.Property(p => p.VendorID).HasColumnName("vendor_id");
                builder.Property(p => p.RoleID).HasColumnName("role_id");
                builder.Property(p => p.Disable).HasColumnName("disable");

            });
            modelBuilder.Entity<Vendor>(builder =>
            {
                builder.ToTable("vendor").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.Address).HasColumnName("addr");
                builder.Property(p => p.Address1).HasColumnName("addr1");
                builder.Property(p => p.Tel).HasColumnName("tel");
                builder.Property(p => p.Tel1).HasColumnName("tel1");
                builder.Property(p => p.Mobile).HasColumnName("mobile");
                builder.Property(p => p.Mobile1).HasColumnName("mobile1");
                builder.Property(p => p.Disable).HasColumnName("disable");
                builder.HasMany(p => p.RsVendors).WithOne(p => p.Vendor).HasForeignKey(p => p.VendorID);
            });
            modelBuilder.Entity<RsVendor>(builder =>
            {
                builder.ToTable("rs_vendor").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.VendorID).HasColumnName("vendor_id");
                builder.Property(p => p.BizTypeID).HasColumnName("biz_type_id");
                builder.Property(p => p.GoodsClassID).HasColumnName("goods_class_id");
                builder.Property(p => p.GoodsID).HasColumnName("goods_id");
            });
            modelBuilder.Entity<PurchasingPlan>(builder =>
            {
                builder.ToTable("purchasing_plan").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.BizTypeID).HasColumnName("biz_type_id");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.DepartmentID).HasColumnName("department_id");
                builder.Property(p => p.CreateUserID).HasColumnName("create_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateTime).HasColumnName("update_time");
                builder.Property(p => p.UpdateUserID).HasColumnName("update_usr_id");
                builder.Property(p => p.ItemCount).HasColumnName("item_count");
                builder.Property(p => p.Status).HasColumnName("purchasing_state_id");
                builder.HasMany(p => p.Details).WithOne(p => p.PurchasingPlan).HasForeignKey(p => p.PurchasingPlanID);
            });
            modelBuilder.Entity<PurchasingPlanDetail>(builder =>
            {
                builder.ToTable("purchasing_plan_detail").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.GoodsClassID).HasColumnName("goods_class_id");
                builder.Property(p => p.GoodsID).HasColumnName("goods_id");
                builder.Property(p => p.PurchasingCount).HasColumnName("count");
                builder.Property(p => p.VendorID).HasColumnName("vendor_id");
                builder.Property(p => p.QuoteDetailID).HasColumnName("quote_detail_id");
                builder.Property(p => p.Price).HasColumnName("unit_price");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateTime).HasColumnName("update_time");
                builder.Property(p => p.UpdateUsrID).HasColumnName("update_usr_id");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.Status).HasColumnName("purchasing_state_id");
                builder.Property(p => p.PurchasingPlanID).HasColumnName("purchasing_plan_id");
            });
            modelBuilder.Entity<PurchasingAudit>(builder =>
            {
                builder.ToTable("purchasing_audit").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.PlanID).HasColumnName("purchasing_plan_id");
                builder.Property(p => p.Result).HasColumnName("audit_type");
                builder.Property(p => p.UserID).HasColumnName("audit_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("audit_time");
                builder.Property(p => p.Desc).HasColumnName("audit_desc");
            });
            modelBuilder.Entity<Quote>(builder =>
            {
                builder.ToTable("quote").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.VendorID).HasColumnName("vendor_id");
                builder.Property(p => p.BizTypeID).HasColumnName("biz_type_id");
                builder.Property(p => p.CreateUserID).HasColumnName("create_usr_id");
                builder.Property(p => p.CreatDateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateUserID).HasColumnName("update_usr_id");
                builder.Property(p => p.UpdateDateTime).HasColumnName("update_time");
                builder.Property(p => p.ItemCount).HasColumnName("item_count");
                builder.Property(p => p.Disable).HasColumnName("disable");
                builder.Property(p => p.Status).HasColumnName("quote_state_id");
                builder.HasMany(p => p.Details).WithOne(p => p.Quote).HasForeignKey(f => f.QuoteID);
            });
            modelBuilder.Entity<QuoteDetail>(builder =>
            {
                builder.ToTable("quote_detail").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.QuoteID).HasColumnName("quote_id");
                builder.Property(p => p.GoodsID).HasColumnName("goods_id");
                builder.Property(p => p.Price).HasColumnName("unit_price");
                builder.Property(p => p.GoodsClassID).HasColumnName("goods_class_id");
                builder.Property(p => p.Disable).HasColumnName("disable");
            });
            modelBuilder.Entity<QuoteAudit>(builder =>
            {
                builder.ToTable("quote_audit").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.QuoteID).HasColumnName("quote_id");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.CreateUsrWechatID).HasColumnName("create_usr_wechat_id");
                builder.Property(p => p.AuditTime).HasColumnName("audit_time");
                builder.Property(p => p.Result).HasColumnName("audit_type");
                builder.Property(p => p.Desc).HasColumnName("audit_desc");
            });
            modelBuilder.Entity<PurchasingOrder>(builder =>
            {
                builder.ToTable("purchasing_order").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.Desc).HasColumnName("desc");
                builder.Property(p => p.PurchasingPlanID).HasColumnName("purchasing_plan_id");
                builder.Property(p => p.VendorID).HasColumnName("vendor_id");
                builder.Property(p => p.DepartmentID).HasColumnName("department_id");
                builder.Property(p => p.Tel).HasColumnName("tel");
                builder.Property(p => p.Addr).HasColumnName("addr");
                builder.Property(p => p.BizTypeID).HasColumnName("biz_type_id");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateUserID).HasColumnName("update_usr_id");
                builder.Property(p => p.UpdateTime).HasColumnName("update_time");
                builder.Property(p => p.ItemCount).HasColumnName("item_count");
                builder.Property(p => p.Total).HasColumnName("total");
                builder.Property(p => p.PurchasingOrderStatusID).HasColumnName("purchasing_order_state_id");
                builder.HasMany(p => p.Details).WithOne(p => p.PurchasingOrder).HasForeignKey(p => p.PurchasingOrderID);
            });
            modelBuilder.Entity<PurchasingOrderDetail>(builder =>
            {
                builder.ToTable("purchasing_order_detail").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id").ValueGeneratedOnAdd();
                builder.Property(p => p.GoodsClassID).HasColumnName("goods_class_id");
                builder.Property(p => p.GoodsID).HasColumnName("goods_id");
                builder.Property(p => p.Count).HasColumnName("count");
                builder.Property(p => p.Price).HasColumnName("unit_price");
                builder.Property(p => p.Subtotal).HasColumnName("subtotal");
                builder.Property(p => p.ActualCount).HasColumnName("actual_count");
                builder.Property(p => p.ActualSubtotal).HasColumnName("actual_subtotal");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.UpdateUsrID).HasColumnName("update_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateTime).HasColumnName("update_time");
                builder.Property(p => p.PurchasingStateID).HasColumnName("purchasing_state_id");
                builder.Property(p => p.AuditUsrID).HasColumnName("audit_usr_id");
                builder.Property(p => p.AuditTime).HasColumnName("audit_time");
                builder.Property(p => p.Audit2UsrID).HasColumnName("audit2_usr_id");
                builder.Property(p => p.Audit2Time).HasColumnName("audit2_time");
                builder.Property(p => p.PurchasingOrderID).HasColumnName("purchasing_order_id");
                builder.Property(p => p.PurchasingOrderStateID).HasColumnName("purchasing_order_state_id");
                builder.Property(p => p.PurchasingPlanDetailID).HasColumnName("purchasing_plan_detail_id");
            });
            modelBuilder.Entity<Depot>(builder =>
            {
                builder.ToTable("depot").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.Name).HasColumnName("name");
                builder.Property(p => p.DepartmentID).HasColumnName("department_id");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.Remark).HasColumnName("remark");
                builder.Property(p => p.Disable).HasColumnName("disable");
                builder.HasMany(p => p.Details).WithOne(p => p.Depot).HasForeignKey(p => p.DepotID);
            });
            modelBuilder.Entity<DepotDetail>(builder =>
            {
                builder.ToTable("depot_detail").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id").ValueGeneratedOnAdd();
                builder.Property(p => p.GoodsClassID).HasColumnName("goods_class_id");
                builder.Property(p => p.GoodsID).HasColumnName("goods_id");
                builder.Property(p => p.Count).HasColumnName("count");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.UpdateUsrID).HasColumnName("update_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateTime).HasColumnName("update_time");
            });
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission").HasKey(k => k.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("text(24)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Desc)
                    .HasColumnName("desc")
                    .HasColumnType("text(48)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("text(48)")
                    .HasDefaultValueSql("''");

                entity.HasMany(e => e.RsPermission).WithOne(e => e.Permission).HasForeignKey(e => e.PermissionId);
            });

            modelBuilder.Entity<RsPermission>(entity =>
            {
                entity.ToTable("rs_permission").HasKey(k => k.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UsrWechatId)
                    .HasColumnName("usr_wechat_id")
                    .HasColumnType("text(64)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Disable)
                    .HasColumnName("disable")
                    .HasColumnType("INT(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            });

            modelBuilder.Entity<ChargeBack>(builder =>
            {
                builder.ToTable("charge_back").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id");
                builder.Property(p => p.Code).HasColumnName("code");
                builder.Property(p => p.PurchasingOrderID).HasColumnName("purchasing_order_id");
                builder.Property(p => p.CreateUsrID).HasColumnName("create_usr_id");
                builder.Property(p => p.CreateTime).HasColumnName("create_time");
                builder.Property(p => p.UpdateUserID).HasColumnName("update_usr_id");
                builder.Property(p => p.UpdateTime).HasColumnName("update_time");
                builder.Property(p => p.ItemCount).HasColumnName("item_count");
                builder.Property(p => p.Total).HasColumnName("total");
                builder.Property(p => p.PurchasingOrderStatusID).HasColumnName("purchasing_order_state_id");
                builder.HasMany(p => p.Details).WithOne(p => p.ChargeBack).HasForeignKey(p => p.ChargeBackID);
            });
            modelBuilder.Entity<ChargeBackDetail>(builder =>
            {
                builder.ToTable("charge_back_detail").HasKey(k => k.ID);
                builder.Property(p => p.ID).HasColumnName("id").ValueGeneratedOnAdd();
                builder.Property(p => p.Count).HasColumnName("count");
                builder.Property(p => p.Price).HasColumnName("unit_price");
                builder.Property(p => p.Subtotal).HasColumnName("subtotal");
                builder.Property(p => p.ChargeBackID).HasColumnName("charge_back_id");
                builder.Property(p => p.PurchasingOrderDetailID).HasColumnName("purchasing_order_detail_id");
            });


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //使用数据库
            optionsBuilder.UseSqlite(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
