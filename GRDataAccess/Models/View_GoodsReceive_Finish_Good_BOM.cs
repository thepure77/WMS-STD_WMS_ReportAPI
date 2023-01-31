using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class View_GoodsReceive_Finish_Good_BOM
    {
        
        [Key]
        public Int64 Rownum { get; set; }

        public string GoodsReceive_No { get; set; }

        public DateTime GoodsReceive_Date { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string BOM_Document { get; set; }

        public string ProductConversionBarcode { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public decimal? Qty_Bom { get; set; }

        public string ProductConversion_Name { get; set; }

        public string PutawayLocation_Id { get; set; }
    }
}

