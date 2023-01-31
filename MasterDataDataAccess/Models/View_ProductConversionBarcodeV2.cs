using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ProductConversionBarcodeV2
    {
        [Key]
        public Guid ProductConversionBarcode_Index { get; set; }
        public string ProductConversionBarcode_Id { get; set; }
        public string ProductConversionBarcode { get; set; }

        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        public Guid ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? ProductConversion_Ratio { get; set; }
        
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }

     }
}
