using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_17_Not_Movement
    {   
        [Key]
        public Guid Row_Index                   {get;set;}
        public long Row_No                      {get;set;}
        public string Warehouse_Type              {get;set;}
        public Guid Tag_Index                   {get;set;}
        public string Tag_No                      {get;set;}
        public Guid Location_Index              {get;set;}
        public string Location_Id                 {get;set;}
        public string Location_Name               {get;set;}
        public Guid Product_Index               {get;set;}
        public string Product_Id                  {get;set;}
        public string Product_Name                {get;set;}
        public Guid BusinessUnit_Index          {get;set;}
        public string BusinessUnit_Name           {get;set;}
        public Guid ProductConversion_Index     {get;set;}
        public string ProductConversion_Id        {get;set;}
        public string ProductConversion_Name      {get;set;}
        public string Su_Unit { get; set; }
        public decimal? Su_Qty { get; set; }          
        public decimal? Su_Ratio                    {get;set;}
        public DateTime? Update_Date                 {get;set;}
        public int? Diff_Movement               {get;set;}
        public string WMS_Sloc                    {get;set;}
        public string SAP_Sloc                    {get;set;}
        public Guid ItemStatus_Index            {get;set;}
        public string ItemStatus_Id               {get;set;}
        public string ItemStatus_Name             {get;set;}
        public DateTime? GoodsReceive_MFG_Date       {get;set;}
        public DateTime? GoodsReceive_EXP_Date       {get;set;}
        public string Product_Lot { get; set; }

    }
}
