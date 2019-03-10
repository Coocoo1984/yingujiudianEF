using System;

namespace DevelopBase.Services
{
    public abstract class ServiceBase
    {
        public static readonly string StrPOPrefix = "PO";
        public static readonly string StrPOSuffixFormat = "yyyyMMddHHmmss";//���뼸��������
        public static readonly string StrPPPrefix = "PP";
        public static readonly string StrPPSuffixFormat = "yyyyMMddHHmmss";//���뼸��������
        public static readonly string StrDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public static readonly string StrQPrefix = "Q";
        public static readonly string StrQSuffixFormat = "yyyyMMddHHmmss";//���뼸��������

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
            /// ����
            /// </summary>
            OrderAwaitVendorConfirm = 7,

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
            /// ����
            /// </summary>
            Other = 7,
        }

        /// <summary>
        /// �ɹ��Ļ��ڲ�������
        /// </summary>
        public enum EnumAuditType
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
            /// ��������Ӧ�̷�
            /// </summary>
            VendorNegative = 5,

            /// <summary>
            /// ��������Ӧ����ȷ��
            /// </summary>
            VendorConfirmed = 6,

            /// <summary>
            /// ��Ӧ���ѷ���
            /// </summary>
            VendorShipped = 7,

            /// <summary>
            /// �������ջ���
            /// </summary>
            DeparmentCheckIn = 8,

            /// <summary>
            /// �������������ջ�
            /// </summary>
            ConfirmReceipt = 9,

            /// <summary>
            /// ����
            /// </summary>
            Other = 10,
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