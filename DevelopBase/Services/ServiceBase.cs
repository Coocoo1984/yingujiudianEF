using System;

namespace DevelopBase.Services
{
    public abstract class ServiceBase
    {
        public static string NoPermissionString = "û�з���Ȩ��";

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
            ChargeBack = 7,

            /// <summary>
            /// ���󲿷ַ����˻�
            /// </summary>
            //ChargeBack = 7,
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
            /// �ɹ����ŷ����˻����(һ�����)
            /// </summary>
            ChargeBackAudit = 13,

            /// <summary>
            /// ��Ӧ��ȷ���˻�
            /// </summary>
            VendorChargeBackComfirm = 14,
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