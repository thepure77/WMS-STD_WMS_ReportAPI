using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_13_Picking
    {
        [Key]
        public Guid Row_Index            {get;set;}
        public long? Row_No               {get;set;}
        public string Warehouse_Type       {get;set;}
        public string BusinessUnit_Name    {get;set;}
        public string GoodsIssue_No        {get;set;}
        public string TruckLoad_No         {get;set;}
        public DateTime? TruckLoad_Date       {get;set;}
        public string PlanGoodsIssue_No    {get;set;}
        public string Chute_No             {get;set;}
        public string Tag_No               {get;set;}
        public string LocationType_Name    {get;set;}
        public string Location_Name        {get;set;}
        public string TagOut_No            {get;set;}
        public Guid? Product_Index        {get;set;}
        public string Product_Id           {get;set;}
        public string Product_Name         {get;set;}
        public decimal? Qty_SaleUnit         {get;set;}
        public decimal? CBM                  {get;set;}
        public int Tote                 { get; set; }
        public string ItemStatus_Name { get; set; }

    }
}
