using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{
    public partial class wm_CycleCountDetail
    {
       [Key]
       public Guid CycleCountDetail_Index { get; set; }
       public Guid CycleCountItem_Index    { get; set; }
       public Guid CycleCount_Index        { get; set; }
       public string CycleCount_No           { get; set; }
       public Guid Location_Index          { get; set; }
       public string Location_Id             { get; set; }
       public string Location_Name           { get; set; }
       public Guid LocationType_Index      { get; set; }
       public string LocationType_Id         { get; set; }
       public string LocationType_Name       { get; set; }
       public Guid Product_Index           { get; set; }
       public string Product_Id              { get; set; }
       public string Product_Name            { get; set; }
       public string Product_SecondName      { get; set; }
       public string Product_ThirdName       { get; set; }
       public string Product_Lot             { get; set; }
       public decimal? Qty_Bal                 { get; set; }
       public decimal? Qty_Count               { get; set; }
       public decimal? Qty_Diff                { get; set; }
       public Guid Tag_Index               { get; set; }
       public Guid TagItem_Index           { get; set; }
       public string Tag_No                  { get; set; }
       public string DocumentRef_No1         { get; set; }
       public string DocumentRef_No2         { get; set; }
       public string DocumentRef_No3         { get; set; }
       public string DocumentRef_No4         { get; set; }
       public string DocumentRef_No5         { get; set; }
       public int? Document_Status         { get; set; }
       public string UDF_1                   { get; set; }
       public string UDF_2                   { get; set; }
       public string UDF_3                   { get; set; }
       public string UDF_4                   { get; set; }
       public string UDF_5                   { get; set; }
       public Guid ItemStatus_Index        { get; set; }
       public string ItemStatus_Id           { get; set; }
       public string ItemStatus_Name         { get; set; }
       public int? Attibute_Count          { get; set; }
       public string Create_By               { get; set; }
       public DateTime? Create_Date             { get; set; }
       public string Update_By               { get; set; }
       public DateTime? Update_Date             { get; set; }
       public string Cancel_By               { get; set; }
       public DateTime? Cancel_Date             { get; set; }
       public DateTime? MFG_Date                { get; set; }
       public DateTime? EXP_Date { get; set; }
       public string Ref_Document_No { get; set; }
       public Guid Ref_Document_Index { get; set; }
       public Guid Ref_DocumentItem_Index  { get; set; }
       public int? Counting { get; set; }
       public string Pallet_ID { get; set; }
       public string Supplier { get; set; }
        

    }
}
