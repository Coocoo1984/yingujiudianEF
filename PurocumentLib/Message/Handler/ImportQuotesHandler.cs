using System;
using DevelopBase.Message;
using DevelopBase.Common;
using PurocumentLib.Message.Request;
using PurocumentLib.Service;
using PurocumentLib.Model;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;

namespace PurocumentLib.Message.Handler
{
    public class ImportQuotesHandler : HandlerGeneric<ImportQuotesRequest>
    {
        public ImportQuotesHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ResponseBase Handler(ImportQuotesRequest request)
        {
            if(request==null)
            {
                throw new ArgumentNullException();
            }
            
            //����
            var model = new QuoteModel()
            {
                Code = request.Code,
                Name = request.Name,
                Desc = request.Desc,
                VendorID = request.VendorID,
                BizTypeID = request.BizTypeID,
                Details = request.Details,
                CreateUserID = request.CreateUserID,
                Disable = false,
            };
            //������ϸ ��ȡExcel�ļ�
            foreach (var localFileURI in request.localFileURIs)
            {
                model.Details = GetSheetValues(localFileURI);
            }
            //��֤����(��)

            var service =ServiceProvider.GetService<IQuoteService>();
            //�ύ������۲�ʧЧ��ʷ����
            service.Add(model);
            
            return new ResponseBase(){Result=1,ResultInfo=""};
        }

        /// <summary>
        /// ��ȡsheet �ڵ����� ����ʵ�弯�� �����۵�Ԫ��Ϊ��������
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public IEnumerable<QuoteDetailModel> GetSheetValues(string fileFullName)
        {
            FileInfo file = new FileInfo(fileFullName);
            if (file != null)
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    //��ȡ��������������
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    var QuoteDetailModels = new List<QuoteDetailModel>();
                    for (int row = 4; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, 7].Value == null) break;
                        QuoteDetailModel detail = new QuoteDetailModel
                        {
                            GoodsName = worksheet.Cells[row, 4].Value.ToString(),
                            Price = Convert.ToDecimal(worksheet.Cells[row, 7].Value)
                    };
                        QuoteDetailModels.Add(detail);
                    }
                    return QuoteDetailModels;
                }
            }
            return null;
        }

    }
}
