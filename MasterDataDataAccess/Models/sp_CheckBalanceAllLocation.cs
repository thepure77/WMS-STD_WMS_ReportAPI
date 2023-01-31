using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckBalanceAllLocation
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Location_Id   {get;set;}
        public string Location_Name    {get;set;}
        public string LocationType_Name {get;set;}
        public string Tag_No            {get;set;}
        public string Product_Id        {get;set;}
        public string Product_Name      {get;set;}
        public decimal? BU_QtyOnHand      {get;set;}
        public string BU_UNIT           {get;set;}
        public decimal? SU_QtyOnHand      {get;set;}
        public string SU_UNIT { get; set; }
    }
}
