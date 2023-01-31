using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPicking
{
    public class ReportPickingPrintViewModel
    {
        public int? rowNum { get; set; }
        public string businessUnit_Name     {get;set;}
        public string goodsIssue_No         {get;set;}
        public string truckLoad_No          {get;set;}
        public string truckLoad_Date        {get;set;}
        public string planGoodsIssue_No     {get;set;}
        public string chute_No              {get;set;}
        public string tag_No                {get;set;}
        public string locationType_Name     {get;set;}
        public string location_Name         {get;set;}
        public string tagOut_No             {get;set;}
        public string product_Index         {get;set;}
        public string product_Id            {get;set;}
        public string product_Name          {get;set;}
        public string qty_SaleUnit          {get;set;}
        public string CBM                   {get;set;}
        public string tote { get; set; }
        public string report_date { get; set; }
        public string report_date_to { get; set; }
        public string ambientRoom { get; set; }
        public string status_Item { get; set; }
    
    }
}
