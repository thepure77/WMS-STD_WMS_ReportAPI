using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransferDataAccess.Models
{
    public class View_GT
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsTransfer_Index { get; set; }
        public string GoodsTransfer_No { get; set; }
        public Guid? TagItem_Index { get; set; }
        public Guid? Tag_Index { get; set; }
        public string Tag_No { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public Guid? Location_Index_To { get; set; }
        public string Location_Id_To { get; set; }
        public string Location_Name_To { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Binbalance_Qty { get; set; }
        public decimal? Qty { get; set; }
        public Guid? ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public DateTime? GoodsTransfer_Date { get; set; }
        public string Update_By { get; set; }
    }
}
