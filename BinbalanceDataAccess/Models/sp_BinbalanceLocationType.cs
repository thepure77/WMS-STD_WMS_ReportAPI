using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BinBalanceDataAccess.Models
{
    public partial class  sp_BinbalanceLocationType
    {
        [Key]
        public Guid BinBalance_Index { get; set; }
        public string Movement_Status                             { get; set; }
        public int? Diff_Movement                               { get; set; }
        public Guid Product_Index                               { get; set; }
        public string Product_Id                                  { get; set; }
        public string Product_Name                                { get; set; }
        public string Product_SecondName                          { get; set; }
        public string Product_ThirdName                           { get; set; }
        public string Product_Lot                                 { get; set; }
        public Guid Location_Index                              { get; set; }
        public string Location_Id                                 { get; set; }
        public string Location_Name                               { get; set; }
        public Guid LocationType_Index                          { get; set; }
        public string LocationType_Id                             { get; set; }
        public string LocationType_Name                           { get; set; }
        public Guid Owner_Index                                 { get; set; }
        public string Owner_Id                                    { get; set; }
        public string Owner_Name                                  { get; set; }
        public Guid TagItem_Index                               { get; set; }
        public Guid Tag_Index                                   { get; set; }
        public string Tag_No                                      { get; set; }
        public DateTime? GoodsReceive_MFG_Date                       { get; set; }
        public DateTime? GoodsReceive_EXP_Date                       { get; set; }
        public Guid GoodsReceive_ProductConversion_Index        { get; set; }
        public string GoodsReceive_ProductConversion_Id           { get; set; }
        public string GoodsReceive_ProductConversion_Name         { get; set; }
        public decimal? BinBalance_Ratio                            { get; set; }
        public decimal? BinBalance_QtyBegin                         { get; set; }
        public decimal? BinBalance_QtyBal                           { get; set; }
        public decimal? BinBalance_QtyReserve                       { get; set; }
        public string ERP_Location                                { get; set; }
        public Guid ItemStatus_Index                            { get; set; }
        public string ItemStatus_Id                               { get; set; }
        public string ItemStatus_Name                             { get; set; }
        public Guid BusinessUnit_Index                          { get; set; }
        public string BusinessUnit_Id                             { get; set; }
        public string BusinessUnit_Name                           { get; set; }
    }
}
