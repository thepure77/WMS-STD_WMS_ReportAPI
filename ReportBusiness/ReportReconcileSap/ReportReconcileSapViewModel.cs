using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportReconcileSap
{
    public class ReportReconcileSapViewModel
    {
        public Guid RowIndex { get; set; }
        public int Row_No { get; set; }
        public string Warehouse_Type { get; set; }
        public string BusinessUnit_Index { get; set; }
        public string BusinessUnit_Name { get; set; }
        public string Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Bu_Qty { get; set; }
        public string Bu_Unit { get; set; }
        public string Su_Qty { get; set; }
        public string Su_Ratio { get; set; }
        public string Su_Unit { get; set; }
        public string QTY { get; set; }
        public string Plant { get; set; }
        public string Sap_Sloc { get; set; }

        public string ambientRoom { get; set; }
        public string ambientRoom_name { get; set; }
        public string Date { get; set; }
        public int rowNum { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }

    }
}
