using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportSummaryStockStorage
{
    public class ReportSummaryStockStorageViewModel
    {
        public string ambientRoom { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string product_Id { get; set; }
        public string goodsReceive_No { get; set; }
        public string purchaseOrder_No { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string GR_Date_From          {get;set;}
	    public string GR_Date_To            {get;set;}
	    public string tag_No                {get;set;}
	    public string vendor_Id         {get;set;}
	    public string matdoc                {get;set;}
	    public string PutAway_Date_From     {get;set;}
	    public string PutAway_Date_To { get; set; }
        public string report_date { get; set; }
        public string report_date_to { get; set; }

        public Guid? owner_Index { get; set; }
        public string vendorId { get; set; }
    }

    public class vendor
    {
        public string vendor_Id { get; set; }
    }


}
