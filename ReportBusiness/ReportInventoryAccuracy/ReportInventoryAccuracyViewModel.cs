using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportInventoryAccuracy
{
    public class ReportInventoryAccuracyViewModel
    {

        public string Sloc { get; set; }
        public string ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }


        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Lot { get; set; }
        public decimal? CBM { get; set; }
        public decimal? SU_QtyBal { get; set; }
        public decimal? SU_QtyReserve { get; set; }
        public decimal? SU_QtyOnHand { get; set; }
        public decimal? Per_SU_QtyReserve { get; set; }
        public decimal? Per_SU_QtyOnHand { get; set; }
        public string SU_UNIT { get; set; }
        public string ERP_Location { get; set; }
    }
}
