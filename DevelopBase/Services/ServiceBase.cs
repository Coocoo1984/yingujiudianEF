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
        public static string NoPermissionString = "û�з���Ȩ��";
        //���� Purchase Order
        public static readonly string StrPOPrefix = "PO";
        public static readonly string StrPOSuffixFormat = "yyyyMMddHHmmss";//���뼸��������
        //�ɹ��ƻ� Purchase Plan
        public static readonly string StrPPPrefix = "PP";
        public static readonly string StrPPSuffixFormat = "yyyyMMddHHmmss";//���뼸��������
        public static readonly string StrDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        //����  Quote
        public static readonly string StrQPrefix = "Q";
        public static readonly string StrQSuffixFormat = "yyyyMMddHHmmss";//���뼸��������
        //�˻� Charge Back
        public static readonly string StrCBPrefix = "CB";
        public static readonly string StrCBSuffixFormat = "yyyyMMddHHmmss";//���뼸��������

        public static IHttpClientFactory IHttpClientFactory = new ServiceCollection().AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();

        public static readonly string MessageUri = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=";//��Ϣ���͵�ַ����
        public static readonly string GetTockenUri = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=ww3589f3907e9ad0e5&corpsecret=zVNtrajjOJgi0C7PC7Xzw7mpvJI3340j-LZhsE9bx2s";//


        public enum EnumRole
        {
            �ɹ�Ա = 3,
            �ɹ����� = 4,
            �ɹ��ܼ� = 5,
            �����Ųɹ������� = 1,
            �����ſ�� = 2,
            ��Ӧ�� = 6,
            ϵͳ����Ա = 7,
            �ܾ��� = 8,
            ���� = 9,
            δ���� = 10,
        }


        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider { get => _serviceProvider; }
        public ServiceBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException();
        }


        /// <summary>
        /// �ɹ��ƻ��׶�״̬����
        /// </summary>
        public enum EnumPurchasingPlanState
        {
            /// <summary>
            /// ����ݸ�
            /// </summary>
            PlanDraft = 1,

            /// <summary>
            /// �ȴ�����
            /// </summary>
            PlanAwaitAudit1 = 2,

            /// <summary>
            /// ���󲵻�
            /// </summary>
            PlanAudit1Rejected = 3,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            PlanAudit1Pass = 4,

            /// <summary>
            /// ���󲵻�
            /// </summary>
            PlanAudit2Rejected = 5,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            PlanAudit2Pass = 6,

            /// <summary>
            /// ���󲵻�
            /// </summary>
            PlanAudit3Rejected = 7,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            PlanAudit3Pass = 8,

            /// <summary>
            /// ��Ӧ�̲���
            /// </summary>
            OrderVendorRejected = 9,

            /// <summary>
            /// ��Ӧ��ȷ��
            /// </summary>
            OrderVendorConfirm = 10,

            /// <summary>
            /// �ϳ�
            /// </summary>
            Cancelled = 10,

        }

        /// <summary>
        /// �����׶�״̬����
        /// </summary>
        public enum EnumPurchasingOrderState
        {
            /// <summary>
            /// �����ȴ���Ӧ��ȷ��
            /// </summary>
            AwaitVendorConfirm = 1,

            /// <summary>
            /// ��������Ӧ�̷�
            /// </summary>
            VendorNegative = 2,

            /// <summary>
            /// ��������Ӧ����ȷ��
            /// </summary>
            VendorConfirmed = 3,

            /// <summary>
            /// ��Ӧ���ѷ���
            /// </summary>
            VendorShipped = 4,

            /// <summary>
            /// �������ջ���
            /// </summary>
            DeparmentCheckIn = 5,

            /// <summary>
            /// �������������ջ�
            /// </summary>
            ConfirmReceipt = 6,

            /// <summary>
            /// �����ŷ����˻�
            /// </summary>
            DepartmentChargeBack = 7,

            /// <summary>
            /// �ɹ����Ĳ����˻�
            /// </summary>
            ChargeBackAuditRejected = 8,

            /// <summary>
            /// �ɹ������˻����ͨ��
            /// </summary>
            ChargeBackAudit = 9,

            /// <summary>
            /// ��Ӧ��ȷ���˻�
            /// </summary>
            VendorChargeBackComfirm = 10,

            /// <summary>
            /// ������ȷ���˻����
            /// </summary>
            ChargeBackFinish = 11,
        }

        /// <summary>
        /// �ɹ��Ļ��ڲ�������
        /// </summary>
        public enum EnumPurchasingAuditType
        {
            /// <summary>
            /// ���󲵻�
            /// </summary>
            PlanAudit1Rejected = 1,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            PlanAudit1Pass = 2,

            /// <summary>
            /// ���󲵻�
            /// </summary>
            PlanAudit2Rejected = 3,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            PlanAudit2Pass = 4,

            /// <summary>
            /// ���󲵻�
            /// </summary>
            PlanAudit3Rejected = 5,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            PlanAudit3Pass = 6,

            /// <summary>
            /// ��������Ӧ�̷�
            /// </summary>
            VendorNegative = 7,

            /// <summary>
            /// ��������Ӧ����ȷ��
            /// </summary>
            VendorConfirmed = 8,

            /// <summary>
            /// ��Ӧ���ѷ���
            /// </summary>
            VendorShipped = 9,

            /// <summary>
            /// �������ջ���
            /// </summary>
            DeparmentCheckIn = 10,

            /// <summary>
            /// �������������ջ�
            /// </summary>
            ConfirmReceipt = 11,

            /// <summary>
            /// �����ŷ����˻�
            /// </summary>
            DepartmentChargeBack = 12,

            /// <summary>
            /// �ɹ����Ĳ����˻�
            /// </summary>
            ChargeBackAuditRejected = 13,

            /// <summary>
            /// �ɹ������˻����ͨ��
            /// </summary>
            ChargeBackAudit = 14,

            /// <summary>
            /// ��Ӧ��ȷ���˻�
            /// </summary>
            VendorChargeBackComfirm = 15,

            /// <summary>
            /// ������ȷ���˻����
            /// </summary>
            ChargeBackFinish = 16,

        }


        /// <summary>
        /// ����״̬����
        /// </summary>
        public enum QuoteState
        {
            //�ݸ�
            QuoteDraft = 1,
            //������
            QuoteAwaitAudit1 = 2,
            //���󲵻�
            QuoteAudit1Rejected = 3,
            //����ͨ��
            QuoteAudit1Pass = 4,
            //���󲵻�
            QuoteAudit2Rejected = 5,
            //����ͨ��
            QuoteAudit2Pass = 6
        }

        public enum QuoteAuditType
        {
            /// <summary>
            /// ���󲵻�
            /// </summary>
            Audit1Rejected = 1,

            /// <summary>
            /// ����ͨ��
            /// </summary>
            Audit1Pass = 2,

            /// <summary>
            /// ���󲵻�
            /// </summary>
            Audit2Rejected = 3,

            /// <summary>
            /// ����ͨ����������Ч
            /// </summary>
            Audit2Pass = 4,

        }



    }
}