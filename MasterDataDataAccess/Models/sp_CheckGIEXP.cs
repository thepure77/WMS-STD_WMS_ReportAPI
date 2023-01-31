using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckGIEXP
    {   
        [Key]
        public long? RowIndex { get; set; }
        public string GoodsIssue_No         {get;set;}
        public DateTime? GoodsIssue_Date       {get;set;}
        public string PlanGoodsIssue_No     {get;set;}
        public string ShipTo_Id             {get;set;}
        public string ShipTo_Name           {get;set;}
        public string Product_Id            {get;set;}
        public string Product_Name          {get;set;}
        public string Product_Lot           {get;set;}
        public DateTime? GoodsReceive_EXP_Date {get;set;}
        public decimal? Qty                   {get;set;}
        public decimal? TotalQty              {get;set;}
        public string ERP_Location { get; set; }
    }
}
