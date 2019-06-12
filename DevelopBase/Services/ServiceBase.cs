using System;

namespace DevelopBase.Services
{
    public abstract class ServiceBase
    {
        public static string NoPermissionString = "没有访问权限";

        public static readonly string StrPOPrefix = "PO";
        public static readonly string StrPOSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议
        public static readonly string StrPPPrefix = "PP";
        public static readonly string StrPPSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议
        public static readonly string StrDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static readonly string StrQPrefix = "Q";
        public static readonly string StrQSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议

        /// <summary>
        /// 采购计划阶段状态定义
        /// </summary>
        public enum EnumPurchasingPlanState
        {
            /// <summary>
            /// 需求草稿
            /// </summary>
            PlanDraft = 1,

            /// <summary>
            /// 等待初审
            /// </summary>
            PlanAwaitAudit1 = 2,

            /// <summary>
            /// 初审驳回
            /// </summary>
            PlanAudit1Rejected = 3,

            /// <summary>
            /// 初审通过
            /// </summary>
            PlanAudit1Pass = 4,

            /// <summary>
            /// 复审驳回
            /// </summary>
            PlanAudit2Rejected = 5,

            /// <summary>
            /// 复审通过
            /// </summary>
            PlanAudit2Pass = 6,

            /// <summary>
            /// 三审驳回
            /// </summary>
            PlanAudit3Rejected = 7,

            /// <summary>
            /// 三审通过
            /// </summary>
            PlanAudit3Pass = 8,

            /// <summary>
            /// 供应商驳回
            /// </summary>
            OrderVendorRejected = 9,

            /// <summary>
            /// 供应商确认
            /// </summary>
            OrderVendorConfirm = 10,

            /// <summary>
            /// 废除
            /// </summary>
            Cancelled = 10,

        }

        /// <summary>
        /// 订单阶段状态定义
        /// </summary>
        public enum EnumPurchasingOrderState
        {
            /// <summary>
            /// 订单等待供应商确认
            /// </summary>
            AwaitVendorConfirm = 1,

            /// <summary>
            /// 订单被供应商否定
            /// </summary>
            VendorNegative = 2,

            /// <summary>
            /// 订单被供应商已确认
            /// </summary>
            VendorConfirmed = 3,

            /// <summary>
            /// 供应商已发货
            /// </summary>
            VendorShipped = 4,

            /// <summary>
            /// 需求部门收货中
            /// </summary>
            DeparmentCheckIn = 5,

            /// <summary>
            /// 需求部门已完整收货
            /// </summary>
            ConfirmReceipt = 6,

            /// <summary>
            /// 需求部门发起退货
            /// </summary>
            ChargeBack = 7,

            /// <summary>
            /// 需求部分发起退货
            /// </summary>
            //ChargeBack = 7,
        }

        /// <summary>
        /// 采购的环节操作定义
        /// </summary>
        public enum EnumPurchasingAuditType
        {
            /// <summary>
            /// 初审驳回
            /// </summary>
            PlanAudit1Rejected = 1,

            /// <summary>
            /// 初审通过
            /// </summary>
            PlanAudit1Pass = 2,

            /// <summary>
            /// 复审驳回
            /// </summary>
            PlanAudit2Rejected = 3,

            /// <summary>
            /// 复审通过
            /// </summary>
            PlanAudit2Pass = 4,

            /// <summary>
            /// 三审驳回
            /// </summary>
            PlanAudit3Rejected = 5,

            /// <summary>
            /// 三审通过
            /// </summary>
            PlanAudit3Pass = 6,

            /// <summary>
            /// 订单被供应商否定
            /// </summary>
            VendorNegative = 7,

            /// <summary>
            /// 订单被供应商已确认
            /// </summary>
            VendorConfirmed = 8,

            /// <summary>
            /// 供应商已发货
            /// </summary>
            VendorShipped = 9,

            /// <summary>
            /// 需求部门收货中
            /// </summary>
            DeparmentCheckIn = 10,

            /// <summary>
            /// 需求部门已完整收货
            /// </summary>
            ConfirmReceipt = 11,

            /// <summary>
            /// 需求部门发起退货
            /// </summary>
            DepartmentChargeBack = 12,

            /// <summary>
            /// 采购部门发起退货审核(一次审核)
            /// </summary>
            ChargeBackAudit = 13,

            /// <summary>
            /// 供应商确认退货
            /// </summary>
            VendorChargeBackComfirm = 14,
        }


        /// <summary>
        /// 报价状态定义
        /// </summary>
        public enum QuoteState
        {
            //草稿
            QuoteDraft = 1,
            //待初审
            QuoteAwaitAudit1 = 2,
            //初审驳回
            QuoteAudit1Rejected = 3,
            //初审通过
            QuoteAudit1Pass = 4,
            //复审驳回
            QuoteAudit2Rejected = 5,
            //复审通过
            QuoteAudit2Pass = 6
        }

        public enum QuoteAuditType
        {
            /// <summary>
            /// 初审驳回
            /// </summary>
            Audit1Rejected = 1,

            /// <summary>
            /// 初审通过
            /// </summary>
            Audit1Pass = 2,

            /// <summary>
            /// 复审驳回
            /// </summary>
            Audit2Rejected = 3,

            /// <summary>
            /// 复审通过，报价生效
            /// </summary>
            Audit2Pass = 4,

        }


        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider{get=>_serviceProvider;}
        public ServiceBase(IServiceProvider serviceProvider)
        {
            if(serviceProvider==null)
            {
                throw new ArgumentNullException();
            }
            _serviceProvider=serviceProvider;
        }
    }
}