using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckReplenishByOrder
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Product_Id         {get;set;}
        public string Product_Name       {get;set;}
        public decimal? BU_QTY             {get;set;}
        public decimal? Order_QTY          {get;set;}
        public string Order_Unit         {get;set;}
        public decimal? SU_QTY             {get;set;}
        public string SU_UNIT            {get;set;}
        public decimal? SU_Weight          {get;set;}
        public decimal? SU_GrsWeight       {get;set;}
        public decimal? SU_W               {get;set;}
        public decimal? SU_L               {get;set;}
        public decimal? SU_H               {get;set;}
        public string IsPeicePick        {get;set;}
        public decimal? QtyInPiecePick { get; set; }
        public decimal? diff { get; set; }
    }
}
