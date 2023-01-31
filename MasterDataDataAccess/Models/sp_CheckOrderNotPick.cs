using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckOrderNotPick
    {
        [Key]
        public long? RowIndex { get; set; }
        public string TruckLoad_No          {get;set;}
        public string Appointment_Id        {get;set;}
        public string Dock_Name             {get;set;}
        public DateTime? Appointment_Date      {get;set;}
        public string Appointment_Time      {get;set;}
        public string PlanGoodsIssue_No     {get;set;}
        public string ShipTo_Id             {get;set;}
        public string ShipTo_Name           {get;set;}
        public string BranchCode            {get;set;}
        public string Product_Id            {get;set;}
        public string Product_Name          {get;set;}
        public decimal? Order_Qty             {get;set;}
        public string Order_Unit { get; set; }
    }
}
