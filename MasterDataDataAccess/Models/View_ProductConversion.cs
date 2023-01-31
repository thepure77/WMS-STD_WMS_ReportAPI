using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class View_ProductConversion
    {
        [Key]
        public Guid ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? ProductConversion_Ratio { get; set; }
        public decimal? ProductConversion_Weight { get; set; }
        public decimal? ProductConversion_Width { get; set; }
        public decimal? ProductConversion_Length { get; set; }
        public decimal? ProductConversion_Height { get; set; }
        public decimal? ProductConversion_VolumeRatio { get; set; }
        public decimal? ProductConversion_Volume { get; set; }

        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        
        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

    }
}
