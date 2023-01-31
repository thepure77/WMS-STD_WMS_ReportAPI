using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckGrPutaway
    {
        [Key]
        public long? RowIndex { get; set; }
        public string GoodsReceive_No       {get;set;}
        public string DocumentType_Name     {get;set;}
        public string PO_No                 {get;set;}
        public string ASN_No                {get;set;}
        public string Appointment_id        {get;set;}
        public DateTime? GoodsReceive_Date     {get;set;}
        public string Tag_No                {get;set;}
        public string Product_Id            {get;set;}
        public string Product_Name          {get;set;}
        public string Product_Lot           {get;set;}
        public DateTime? MFG_Date              {get;set;}
        public DateTime? EXP_Date              {get;set;}
        public decimal? BU_Qty                {get;set;}
        public string BU_Unit               {get;set;}
        public decimal? SU_Qty                {get;set;}
        public string SU_Unit               {get;set;}
        public string Suggest_Location_Name {get;set;} 
        public string LocationType_Name     {get;set;}
        public string Putaway_Date          {get;set;}
        public int? Tag_Status { get; set; }
    }
}
