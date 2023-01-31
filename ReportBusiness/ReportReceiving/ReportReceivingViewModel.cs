using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportReceiving
{
    public class ReportReceivingViewModel
    {
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string ambientRoom { get; set; }
        public string goodsReceive_No { get; set; }
        //public string planGoodsReceive_No { get; set; }
        public string report_date { get; set; }
        public string report_date_to { get; set; }

        public string businessUnit_Index     { get; set; }
        public string goodsReceive_Index     { get; set; }
        public string gR_Date_From           { get; set; }
        public string gR_Date_To             { get; set; }
        public string purchaseOrder_No    { get; set; }
        public string documentType_Index     { get; set; }
        public string vendor_Index           { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string matdoc                    { get; set; }
    }
}
