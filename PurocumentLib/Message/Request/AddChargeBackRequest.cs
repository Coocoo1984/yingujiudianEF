using System;
using System.Collections.Generic;
using System.Text;
using DevelopBase.Message;
namespace PurocumentLib.Message.Request
{
    public class AddChargeBackRequest: RequestBase
    {
        public string WechatID { get; set; }
        public int DepartmentID { get; set; }
        //订单ID
        public int OrderID { get; set; }
        public List<CBDetail> Details { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int CreateUserID { get; set; }
    }

    public class CBDetail
    {
        //必传 后台自动提取订单明细的数量、单价、并计算小计金额
        public int PurchasingOrderDetailId { get; set; }
        //预留 预防部分退货 非订单明细商品数量、单价退货
        //public int GoodsID { get; set; }
        //预留 预防部分退货 非订单明细商品数量、单价退货
        public decimal Count { get; set; }
        //预留 预防部分退货 非订单明细商品数量、单价退货
        //public decimal UnitPrice { get; set; }
    }
}
