using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPicking
{
    public class ReportPickingViewModel
    {
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string ambientRoom { get; set; }
        public string goodsIssue_No { get; set; }
        public string truckLoad_No { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string tag_No { get; set; }
        public string product_Id { get; set; }
        public string location_Id { get; set; }
        public string chut_Id { get; set; }
        public string report_date { get; set; }
        public string report_date_to { get; set; }
        public LocationTypeViewModel locationTypeList { get; set; }
        public ItemStatus itemStatuList { get; set; }
        public string date_Main_Start { get; set; }
        public string date_Main_to { get; set; }
        public string tagOut_No { get; set; }
    }
    public class ItemStatus
    {
        public Guid? itemStatus_Index { get; set; }

        public string itemStatus_Id { get; set; }

        public string itemStatus_Name { get; set; }

    }
}
