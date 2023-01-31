using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportFinishGoods
{
    public class ReportFinishGoodsViewModel
    {
       
        public string GoodsReceive_No { get; set; }

        public string GoodsReceive_Date { get; set; }
        public string GoodsReceive_Date_Start { get; set; }
        public string GoodsReceive_Date_End { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string BOM_Document { get; set; }

        public string ProductConversionBarcode { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public decimal? Qty_Bom { get; set; }

        public string ProductConversion_Name { get; set; }

        public string PutawayLocation_Id { get; set; }
        public int Rownum { get; set; }

        
    }


}
