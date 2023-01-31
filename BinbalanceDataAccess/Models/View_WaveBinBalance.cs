using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_WaveBinBalance
    {
        [Key]
        [Column(Order = 0)]
        public Guid BinBalance_Index { get; set; }

        
        [Column(Order = 1)]
        public Guid Owner_Index { get; set; }

        
        [Column(Order = 2)]
        [StringLength(50)]
        public string Owner_Id { get; set; }

        
        [Column(Order = 3)]
        [StringLength(50)]
        public string Owner_Name { get; set; }

        
        [Column(Order = 4)]
        public Guid Location_Index { get; set; }

        [StringLength(50)]
        public string Location_Id { get; set; }

        [StringLength(200)]
        public string Location_Name { get; set; }

        
        [Column(Order = 5)]
        public Guid GoodsReceive_Index { get; set; }

        
        [Column(Order = 6)]
        [StringLength(50)]
        public string GoodsReceive_No { get; set; }

        
        [Column(Order = 7)]
        public DateTime GoodsReceive_Date { get; set; }

        
        [Column(Order = 8)]
        public Guid GoodsReceiveItem_Index { get; set; }

        
        [Column(Order = 9)]
        public Guid GoodsReceiveItemLocation_Index { get; set; }

        
        [Column(Order = 10)]
        public Guid TagItem_Index { get; set; }

        
        [Column(Order = 11)]
        public Guid Tag_Index { get; set; }

        [StringLength(50)]
        public string Tag_No { get; set; }

        
        [Column(Order = 12)]
        public Guid Product_Index { get; set; }

        
        [Column(Order = 13)]
        [StringLength(50)]
        public string Product_Id { get; set; }

        
        [Column(Order = 14)]
        [StringLength(200)]
        public string Product_Name { get; set; }

        [StringLength(200)]
        public string Product_SecondName { get; set; }

        [StringLength(200)]
        public string Product_ThirdName { get; set; }

        [StringLength(50)]
        public string Product_Lot { get; set; }

        
        [Column(Order = 15)]
        public Guid ItemStatus_Index { get; set; }

        
        [Column(Order = 16)]
        [StringLength(50)]
        public string ItemStatus_Id { get; set; }

        
        [Column(Order = 17)]
        [StringLength(200)]
        public string ItemStatus_Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GoodsReceive_MFG_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GoodsReceive_EXP_Date { get; set; }

        
        [Column(Order = 18)]
        public Guid GoodsReceive_ProductConversion_Index { get; set; }

        
        [Column(Order = 19)]
        [StringLength(50)]
        public string GoodsReceive_ProductConversion_Id { get; set; }

        
        [Column(Order = 20)]
        [StringLength(200)]
        public string GoodsReceive_ProductConversion_Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_Ratio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_QtyBegin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_WeightBegin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_VolumeBegin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_QtyBal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_WeightBal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_VolumeBal { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_QtyReserve { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_WeightReserve { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BinBalance_VolumeReserve { get; set; }

        public Guid? ProductConversion_Index { get; set; }

        
        [Column(Order = 21)]
        [StringLength(50)]
        public string ProductConversion_Id { get; set; }

        
        [Column(Order = 22)]
        [StringLength(200)]
        public string ProductConversion_Name { get; set; }

        [StringLength(200)]
        public string UDF_1 { get; set; }

        [StringLength(200)]
        public string UDF_2 { get; set; }

        [StringLength(200)]
        public string UDF_3 { get; set; }

        [StringLength(200)]
        public string UDF_4 { get; set; }

        [StringLength(200)]
        public string UDF_5 { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        [StringLength(200)]
        public string IsUse { get; set; }

        public int? BinBalance_Status { get; set; }
        public int? AgeRemain { get; set; }
    }
}
