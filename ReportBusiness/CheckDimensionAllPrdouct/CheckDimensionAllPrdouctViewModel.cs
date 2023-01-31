using System;
using System.Collections.Generic;

using System.Text;

namespace ReportBusiness.CheckDimensionAllPrdouct
{
    public class CheckDimensionAllPrdouctViewModel
    {
        public int rowNo { get; set; }
        public string product_ID { get; set; }
        public string product_No { get; set; }
        public string product_Name { get; set; }
        public string bu_UNIT { get; set; }
        public string sale_UNIT { get; set; }
        public string in_UNIT { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? productConversion_Ratio { get; set; }
        public decimal? productConversion_Weight { get; set; }
        public decimal? productConversion_GrsWeight { get; set; }
        public decimal? productConversion_Width { get; set; }
        public decimal? productConversion_Length { get; set; }
        public decimal? productConversion_Height { get; set; }
        public string ti { get; set; }
        public string hi { get; set; }
        public string productShelfLifeGR_D { get; set; }
        public string isPiecePcik { get; set; }
        public int? productItemLife_D { get; set; }
        public int? productShelfLifeGI_D { get; set; }
        public string productConversionBarcode { get; set; }
        public string sup { get; set; }
        public int? isMfgDate { get; set; }
        public int? isExpDate { get; set; }
        public int? isLot { get; set; }
        public int? isSerial { get; set; }
        public decimal? bu_qty_Per_Tag { get; set; }
        public decimal? qty_Per_Tag { get; set; }
        public string create_By { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }

    }
}
