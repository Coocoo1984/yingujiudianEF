using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace DevelopBase.Services
{
    public class TextMesage : StringContent
    {
        public TextMesage(object obj) :
                    base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }

    public class token
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public abstract class ServiceBase
    {
        public static readonly string strMessageTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static string NoPermissionString = "没有访问权限";
        //订单 Purchase Order
        public static readonly string StrPOPrefix = "PO";
        public static readonly string StrPOSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议
        //采购计划 Purchase Plan
        public static readonly string StrPPPrefix = "PP";
        public static readonly string StrPPSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议
        public static readonly string StrDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        //报价  Quote
        public static readonly string StrQPrefix = "Q";
        public static readonly string StrQSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议
        //退货 Charge Back
        public static readonly string StrCBPrefix = "CB";
        public static readonly string StrCBSuffixFormat = "yyyyMMddHHmmss";//毫秒几乎无异议

        public static IHttpClientFactory IHttpClientFactory = new ServiceCollection().AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();

        public static readonly string MessageUri = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=";//消息推送地址带参
        public static readonly string GetTockenUri = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=ww3589f3907e9ad0e5&corpsecret=zVNtrajjOJgi0C7PC7Xzw7mpvJI3340j-LZhsE9bx2s";//


        public enum EnumRole
        {
            采购员 = 3,
            采购主管 = 4,
            采购总监 = 5,
            需求部门采购负责人 = 1,
            需求部门库管 = 2,
            供应商 = 6,
            系统管理员 = 7,
            总经理 = 8,
            测试 = 9,
            未设置 = 10,
        }


        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider { get => _serviceProvider; }
        public ServiceBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException();
        }


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
            DepartmentChargeBack = 7,

            /// <summary>
            /// 采购中心驳回退货
            /// </summary>
            ChargeBackAuditRejected = 8,

            /// <summary>
            /// 采购中心退货审核通过
            /// </summary>
            ChargeBackAudit = 9,

            /// <summary>
            /// 供应商确认退货
            /// </summary>
            VendorChargeBackComfirm = 10,

            /// <summary>
            /// 需求部门确认退货完成
            /// </summary>
            ChargeBackFinish = 11,
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
            /// 采购中心驳回退货
            /// </summary>
            ChargeBackAuditRejected = 13,

            /// <summary>
            /// 采购中心退货审核通过
            /// </summary>
            ChargeBackAudit = 14,

            /// <summary>
            /// 供应商确认退货
            /// </summary>
            VendorChargeBackComfirm = 15,

            /// <summary>
            /// 需求部门确认退货完成
            /// </summary>
            ChargeBackFinish = 16,

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



    }
}