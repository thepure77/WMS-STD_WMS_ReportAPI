using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_11_PutAway
    {
        [Key]
        public Guid? Row_Index                {get;set;}
        public long? Row_No                   {get;set;}
        public string Warehouse_Type           {get;set;}
        public string BusinessUnit_Name        {get;set;}
        public string GoodsReceive_No          {get;set;}
        public DateTime? GoodsReceive_Date        {get;set;}
        public string PurchaseOrder_No         {get;set;}
        public string PlanGoodsReceive_No      {get;set;}
        public string Tag_No                   {get;set;}
        public string PutawayLocation_Id       {get;set;}
        public Guid? Product_Index            {get;set;}
        public string Product_Id               {get;set;}
        public string Product_Name             {get;set;}
        public string Product_Lot              {get;set;}
        public decimal? PutAway_Qty              {get;set;}
        public string PutAway_SU               {get;set;}
        public string TixHi                    {get;set;}
        public decimal? Qty_Pallet               {get;set;}
        public decimal? PalletHeight             {get;set;}
        public string LimitHeight              {get;set;}
        public decimal? PalletWeight             {get;set;}
        public Guid? Vendor_Index             {get;set;}
        public string Vendor_Id                {get;set;}
        public string Vendor_Name              {get;set;}
        public DateTime? GR_Date        {get;set;}
        public DateTime? Putaway_Date             {get;set;}
        public string Putaway_Time             {get;set;}
        public string ItemStatus_Name          {get;set;}
        public DateTime? MFG_Date                 {get;set;}
        public DateTime? EXP_Date                 {get;set;}
        public string SAP_Sloc                 {get;set;}
        public string WMS_Sloc                 {get;set;}
        public string DocumentType_Name        {get;set;}
        public int Document_Status          {get;set;}
        public string Matdoc                   {get;set;}
        public string DocumentRef_No2          {get;set;}
        public string ProcessStatus_Name       {get;set;}
        public string Document_Remark          {get;set;}
        public string Update_By { get; set; }

    }
}
