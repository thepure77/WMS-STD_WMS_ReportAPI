using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckOnGroundRobot
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Product_Id            {get;set;}
        public string Product_Name          {get;set;}
        public string Location_Name         {get;set;}
        public string Tag_No                {get;set;}
        public string Product_Lot           {get;set;}
        public DateTime? GoodsReceive_MFG_Date {get;set;}
        public DateTime? GoodsReceive_EXP_Date {get;set;}
        public DateTime? GoodsReceive_Date     {get;set;}
        public decimal? BU_QtyOnHand          {get;set;}
        public string BU_UNIT               {get;set;}
        public decimal? SU_QtyOnHand          {get;set;}
        public string SU_UNIT               {get;set;}
        public string DocumentRef_No        {get;set;}
        public int? RobotGroup            {get;set;}
        public string ERP_Location          {get;set;}
        public Guid? BinBalance_Index { get; set; }
    }
}
