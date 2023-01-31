using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class View_GetProductOwner
    {
        [Key]
        public Guid Product_Index { get; set; }

        [StringLength(50)]
        public string Product_Id { get; set; }

        [StringLength(200)]
        public string Product_Name { get; set; }

        public Guid ProductConversion_Index { get; set; }
        [StringLength(50)]
        public string ProductConversion_Id { get; set; }

        [StringLength(200)]
        public string ProductConversion_Name { get; set; }

        [StringLength(200)]
        public string Product_SecondName { get; set; }

        [StringLength(200)]
        public string Product_ThirdName { get; set; }

        public Guid Owner_Index { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Ratio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Weight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Width { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Length { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Height { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_VolumeRatio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Volume { get; set; }
    }
}
