using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_CheckPickTime
    {
        [Key]
        public long? RowIndex { get; set; }
        public string GoodsIssue_No     {get;set;}
        public DateTime? GoodsIssue_Date   {get;set;}
        public string RoundWave         {get;set;}
        public string PICKTYPE          {get;set;}
        public DateTime? Min_Date          {get;set;}
        public TimeSpan? Min_Time          {get;set;}
        public DateTime? Max_Date          {get;set;}
        public TimeSpan? Max_Time { get; set; }
    }
}
