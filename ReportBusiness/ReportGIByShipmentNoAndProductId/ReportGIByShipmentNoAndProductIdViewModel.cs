using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportGIByShipmentNoAndProductId
{
    public class ReportGIByShipmentNoAndProductIdViewModel
    {
        public int? rowNum { get; set; }
        public string shipment_date { get; set; }
        public string shipment_time { get; set; }
        public string shipment_no { get; set; }
        public string business_unit { get; set; }
        public string product_Name { get; set; }
        public string product_Id { get; set; }
        //public string product_Index { get; set; }
        public int? su_Order { get; set; }
        public int? su_WhGi_Qty { get; set; }
        public int? su_TrGi_Qty { get; set; }
        public string su_UNIT { get; set; }
        public decimal? su_CBM { get; set; }
        public decimal? su_Volume { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public Guid? product_Index { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string ambientRoom { get; set; }
    }
}
