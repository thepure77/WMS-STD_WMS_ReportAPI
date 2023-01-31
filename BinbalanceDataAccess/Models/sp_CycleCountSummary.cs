using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BinbalanceDataAccess.Models
{
    public partial class sp_CycleCountSummary
    {
       [Key]
       public Guid Row_Index { get; set; }
       public Guid CycleCount_Index { get; set; }
       public string CycleCount_No { get; set; }
       public string Create_By { get; set; }
       public DateTime? Create_Date { get; set; }
       public Guid LocationType_Index { get; set; }
       public string LocationType_Id { get; set; }
       public string LocationType_Name                          { get; set; }
       public Guid Location_Index                             { get; set; }
       public string Location_Id                                { get; set; }
       public string Location_Name                              { get; set; }
       public Guid TagItem_Index                              { get; set; }
       public Guid Tag_Index                                  { get; set; }
       public string Tag_No                                     { get; set; }
       public Guid Product_Index                              { get; set; }
       public string Product_Id                                 { get; set; }
       public string Product_Name                               { get; set; }
       public Guid Owner_Index                                { get; set; }
       public string Owner_Id                                   { get; set; }
       public string Owner_Name                                 { get; set; }
       public string ERP_Location                               { get; set; }
       public Guid ItemStatus_Index                           { get; set; }
       public string ItemStatus_Id                              { get; set; }
       public string ItemStatus_Name                            { get; set; }
       public decimal? BinBalance_QtyBal                          { get; set; }
       public Guid GoodsReceive_ProductConversion_Index       { get; set; }
       public string GoodsReceive_ProductConversion_Id          { get; set; }
       public string GoodsReceive_ProductConversion_Name        { get; set; }
       public decimal? Sale_Unit                                  { get; set; }
       public decimal? SALE_ProductConversion_Ratio               { get; set; }
       public string SALE_ProductConversion_Name                { get; set; }
       public decimal? Qty_Count                                  { get; set; }
       public decimal? Qty_Diff                                   { get; set; }
       public string Status_Diff_Count                          { get; set; }
       public string Status_Diff_Count_Check                    { get; set; }
       public string Count_by                                   { get; set; }
       public DateTime? Count_Date                                 { get; set; }
       public string Product_Lot                                { get; set; }
       public DateTime? GoodsReceive_MFG_Date                      { get; set; }
       public DateTime? GoodsReceive_EXP_Date                      { get; set; }
       public DateTime? GoodsReceive_Date                          { get; set; }
       public string GoodsReceive_No { get; set; }
       public string BusinessUnit_Name { get; set; }
       public string RollCage_Index { get; set; }
       public string RollCage_Id { get; set; }
       public string RollCage_Name { get; set; }

    }
}
