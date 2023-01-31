
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{

    public partial class View_ProductDetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid ProductConversionBarcode_Index { get; set; }

       
        [Column(Order = 1)]
        public Guid Product_Index { get; set; }

       
        [Column(Order = 2)]
        [StringLength(50)]
        public string Product_Id { get; set; }

       
        [Column(Order = 3)]
        [StringLength(200)]
        public string Product_Name { get; set; }

        [StringLength(200)]
        public string Product_SecondName { get; set; }

        [StringLength(200)]
        public string Product_ThirdName { get; set; }

       
        [Column(Order = 4)]
        public Guid ProductConversion_Index { get; set; }

       
        [Column(Order = 5)]
        [StringLength(50)]
        public string ProductConversion_Id { get; set; }

       
        [Column(Order = 6)]
        [StringLength(200)]
        public string ProductConversion_Name { get; set; }

       
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ProductConversion_Ratio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Weight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Width { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Length { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Height { get; set; }

       
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ProductConversion_VolumeRatio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductConversion_Volume { get; set; }

       
        [Column(Order = 9)]
        [StringLength(50)]
        public string ProductConversionBarcode_Id { get; set; }

       
        [Column(Order = 10)]
        [StringLength(200)]
        public string ProductConversionBarcode { get; set; }

       
        [Column(Order = 11)]
        public Guid Owner_Index { get; set; }

        [StringLength(50)]
        public string Owner_Id { get; set; }

        [StringLength(200)]
        public string Owner_Name { get; set; }

       
        [Column(Order = 12)]
        public Guid ProductType_Index { get; set; }

       
        [Column(Order = 13)]
        [StringLength(50)]
        public string ProductType_Id { get; set; }

       
        [Column(Order = 14)]
        [StringLength(200)]
        public string ProductType_Name { get; set; }

       
        [Column(Order = 15)]
        public Guid ProductSubType_Index { get; set; }

       
        [Column(Order = 16)]
        [StringLength(50)]
        public string ProductSubType_Id { get; set; }

       
        [Column(Order = 17)]
        [StringLength(200)]
        public string ProductSubType_Name { get; set; }

       
        [Column(Order = 18)]
        public Guid ProductCategory_Index { get; set; }

       
        [Column(Order = 19)]
        [StringLength(50)]
        public string ProductCategory_Id { get; set; }

       
        [Column(Order = 20)]
        [StringLength(200)]
        public string ProductCategory_Name { get; set; }


        public int? IsExpDate { get; set; }

        public int? IsLot { get; set; }

        public int? ProductItemLife_Y { get; set; }

        public int? ProductItemLife_M { get; set; }

        public int? ProductItemLife_D { get; set; }

        public string BaseProductConversion { get; set; }

        public int? IsMfgDate { get; set; }

        public int? IsCatchWeight { get; set; }
    }
}
